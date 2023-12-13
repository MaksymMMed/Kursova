import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Speaker = ({id}) => {

    const [audioBlob, setAudioBlob] = useState(null);
  const [audioURL, setAudioURL] = useState('');

  useEffect(() => {

    axios.get(`https://localhost:7000/SentenceController/GetSentence/?id=${id}`, { headers:{'Authorization': `Bearer ${localStorage.getItem("Token")}`}, responseType: 'blob' })
      .then(response => {
        console.log(response)
        setAudioBlob(response.data);
        setAudioURL(URL.createObjectURL(response.data));
      })
      .catch(error => {
        console.error('Помилка отримання аудіозапису:', error);
      });
  }, [id]);

  
  return (
    <div style ={{display: "flex", flexDirection: "column", alignItems: "center" }}>
      <h1>Цільовий голос</h1>
      {audioBlob && <audio controls src={audioURL} />}
    </div>
  )
}

export default Speaker;
