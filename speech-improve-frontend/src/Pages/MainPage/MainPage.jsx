import React, { useState } from "react";
import './MainPage.css'
import { useEffect } from "react";
import SmallButton from "../../UI/button/smallButton/SmallButton"
import { useNavigate } from "react-router-dom";
import axios from "axios";
import Mic from "../../UI/Mic/Mic"
import Speaker from "../../UI/Speaker/Speaker";
import BaseInput from "../../UI/Input/BasicInput"
import Result from "../../UI/Result/Result";

const MainPage = (props) =>{


  const config = {
    headers: {
      'Access-Control-Allow-Origin': "*",
      'Authorization': `Bearer ${localStorage.getItem("Token")}`
    },
  };

  const [SentenceId,SetSentenceId] = useState(8)
  const [ChoosenId,SetChoosen] = useState(8)
  const [Path,SetPath] = useState("C:/Audio/Voices/Voice8.mp")
  const [Points,SetPoints] = useState("N")


  const [UserData,SetUserData] = useState(null)

  const Navigate = useNavigate()
  useEffect(()=>
    {
      var data = JSON.parse(localStorage.getItem("UserData"))
      SetUserData(data)
    },[])
 
  const openProfile = () =>{
    Navigate("/ProfilePage",{state:UserData})
  }
  
  const HandleChoosen = () =>{
    SetChoosen(SentenceId)
    axios.get(`https://localhost:7000/SentenceController/GetSentencePath/?id=${SentenceId}`,config)
    .then(response=>{
      console.log(response.data)
      SetPath(response.data)
    })
  }

  const handleResult = (responseData) => {
    // Тут ви можете використовувати дані, які ви отримали з POST-запиту
    console.log('Отримані дані:', responseData);
    SetPoints(responseData)
    // Додайте код для обробки даних відповідно до вашої логіки
  };


      return(
        <div>
            <header style={{paddingBottom:"15px"}}>
                <h1>Speech Improve App</h1>
                <SmallButton style={{marginTop:"25px"}} onClick={openProfile}>Profile</SmallButton>
            </header>

            <div style ={{display: "flex", flexDirection: "column", alignItems: "center" }}>
              <h1>Виберіть номер запису</h1>
              <BaseInput value={SentenceId} onChange={e=>SetSentenceId(e.target.value)}/>
              <SmallButton style={{width:"200px",marginTop:"15px"}} onClick={HandleChoosen}>Вибрати номер</SmallButton>
            </div>

            <div style={{marginTop:"10px",display:"flex",flexDirection:"column",justifyContent:'center',alignItems:'center'}}>
              <Speaker id ={ChoosenId}/>
            </div>

            <div style={{marginTop:"10px"}}>
              <Mic path = {Path} IdSent={SentenceId} HandleResult = {handleResult}/>
            </div>

            <div style={{marginTop:"10px"}}>
              <Result Dist = {Points}/>
            </div>
            
        </div>
      )
  };

export default MainPage
