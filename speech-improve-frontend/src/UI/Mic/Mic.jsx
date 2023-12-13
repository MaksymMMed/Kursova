import MicRecorder from 'mic-recorder-to-mp3';
import React, { useState } from "react";
import { useEffect } from "react";
import axios from "axios";
import SmallButton from '../button/smallButton/SmallButton';

const Mic = ({path,HandleResult,IdSent}) => {

    const config = {
        headers: {
          'Access-Control-Allow-Origin': "*",
          'Authorization': `Bearer ${localStorage.getItem("Token")}`
        },
      };

const [Mp3Recorder, setMp3Recorder] = useState(new MicRecorder({bitRate: 128 }))


const [isRecording, setIsRecording] = useState(false);
  const [blobURL, setBlobURL] = useState('');
  const [isBlocked, setIsBlocked] = useState(false);

  const startRecording = () => {
    if (isBlocked) {
      console.log('Permission Denied');
    } else {
      Mp3Recorder.start()
        .then(() => setIsRecording(true))
        .catch((e) => console.error(e));
    }
  };

  const stopRecording = () => {
    Mp3Recorder.stop()
      .getMp3()
      .then(([buffer, blob]) => {
        const audioFile = new File([blob], 'audio.mp3', { type: 'audio/mp3' });

        const formData = new FormData();
        formData.append('file', audioFile);

        formData.append('file_path', path);
        axios.post('http://127.0.0.1:5000/Recognize', formData, {headers:{'Access-Control-Allow-Origin': "*"}})
          .then(response => {
            HandleResult(response.data)
            console.log(JSON.stringify({
              "Id": 0,
              "DateTime": new Date().toISOString(),
              "Accuracy": response.data,
              "UserId": JSON.parse(localStorage.getItem("UserData")).Id,
              "SentenceId":IdSent }))

              axios.post('https://localhost:7000/AttempController/AddAttemp',JSON.stringify({
              "Id": 0,
              "DateTime": new Date().toISOString(),
              "Accuracy": response.data,
              "UserId": JSON.parse(localStorage.getItem("UserData")).Id,
              "SentenceId":IdSent 
          }),{headers: {
            'Access-Control-Allow-Origin': "*",
            'Content-Type':'application/json',
            'Authorization' : `Bearer ${localStorage.getItem("Token")}`
          }})
          })
          .catch(error => {
            console.error('Помилка відправлення аудіо:', error);
          });
        
        const url = URL.createObjectURL(blob);
        setBlobURL(url);
        setIsRecording(false);
      })
      .catch((e) => console.log(e));
  };

  useEffect(() => {
    navigator.getUserMedia({ audio: true },
      () => setIsBlocked(false),
      () => setIsBlocked(true)
    );
}, []);


    return(
            <div style ={{display: "flex", flexDirection: "column", alignItems: "center" }}>
                <h1>Ваш голос</h1>
                <audio src={blobURL} controls="controls" />
                <div style={{display:"flex",justifyContent:"space-between",width:"250px",marginTop:"15px"}}>
                    <SmallButton onClick={startRecording} disabled={isRecording}>Записати</SmallButton>
                    <SmallButton onClick={stopRecording} disabled={!isRecording}>Надіслати</SmallButton>
                </div>
            </div>
  )
}

export default Mic