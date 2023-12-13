import React, { useEffect,useState } from "react";
import BasicInput from "../../UI/Input/BasicInput";
import "./ProfilePage.css"
import BigButton from "../../UI/button/bigButton/BigButton"
import { useNavigate } from "react-router-dom";
import axios from "axios";
import Attemp from "../../UI/Attemp/Attemp";

const ProfilePage = () =>{

    const userData = JSON.parse(localStorage.getItem("UserData")) 
    const Navigate = useNavigate()
    
    const Logout = () =>{
        localStorage.clear()
                Navigate("/")
    }

    const [attemps,setAttemps] = useState([])


    const getAttemps = () =>{
        axios
          .get('https://localhost:7000/AttempController/GetUserAttemps', {params : {id: userData.Id},headers:{'Authorization': `Bearer ${localStorage.getItem("Token")}`}})
        .then(response => {
          setAttemps(response.data);
          console.log(response.data)
        })
        .catch(error => {
          console.log(error);
        });
      }

    useEffect(()=>{
        getAttemps()
    },[])
    
    return(
        <div>
            <button onClick={()=>Navigate(-1)} style={{border:"none",backgroundColor:"transparent",fontSize:"34px",fontWeight:"bold",margin:"15px 0 0 50px"}}>Повернутися назад</button>
        <div className="ProfilePage">
            <div className="InputsPlace">
                <p style={{width:"80px"}}>Email:</p>
                <BasicInput disabled style ={{marginRight:"8px",width:"80%"}} value={userData.Email}/>
            </div>  
            <div className="ControlButtons">
            <BigButton style={{width: "40%"}} onClick={Logout}>Вийти з профілю</BigButton>   
            </div>
        </div>

            <div style={{ margin: '0 auto', width: 'fit-content' }}>
                {attemps.slice(-10).map(item =>
                (
                    <Attemp key ={item.Id} item={item}/>
                ))}
            </div>
        </div>
    )
}

export default ProfilePage