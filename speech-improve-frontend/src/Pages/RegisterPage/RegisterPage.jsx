import React, { useState,useEffect } from 'react';
import axios from "axios";
import './RegisterPage.css'
import { Link,useNavigate } from "react-router-dom";
import BigButton from "../../UI/button/bigButton/BigButton";
import BasicInput from "../../UI/Input/BasicInput";


const RegisterPage = () =>{
    
    const [IsDataCorrect,setIsDataCorrect] = useState()
    const [userData,setUserData] = useState({email:'HeisenbergW@gmail.com',password:'password',name:'Walther',surname:"White"})
    const config = {
      headers: {
        'Content-Type': 'application/json'
      }
    };

    useEffect(()=>{
      setIsDataCorrect(true)
    },[])

    const Navigate = useNavigate();

    const Register = async(e) =>{
      console.log(userData);
      e.preventDefault();
      axios.post
      ('https:/localhost:7000/UserController/SignUp',JSON.stringify(userData),config)
      .then(response => {
        if (response.status === 200)
        {
          Navigate("/")
        }
      })
      .catch(error => {
        setUserData({name:"",email:"",password:"",surname:""})
        console.log(error);
        setIsDataCorrect(false)
      });
    }

    return (
        <div className="RegisterPage">
          <h1>Speech Improve</h1>
          <div className="InputPlace">
            <h3>Email</h3>
            <BasicInput placeholder=" Введіть email..." value={userData.email} onChange={e=>setUserData({...userData, email : e.target.value})} />
          </div>
          <div className="InputPlace">
            <h3>Name</h3>
            <BasicInput placeholder=" Введіть ім'я..." value={userData.name} onChange={e=>setUserData({...userData, name : e.target.value})} />
          </div>
          <div className="InputPlace">
            <h3>Surname</h3>
            <BasicInput placeholder=" Введіть прізвище..." value={userData.surname} onChange={e=>setUserData({...userData, surname : e.target.value})} />
          </div>
          <div className="InputPlace">
            <h3>Password</h3>
            <BasicInput placeholder=" Введіть пароль..." value={userData.password} onChange={e=>setUserData({...userData, password : e.target.value})} />
          </div>
          {IsDataCorrect === false
          ?
            <p style={{color:"red"}}>Користувач з таким email вже існує</p>
          : null
          } 
          <div className="Other">
            <p><Link to="/">Авторизуватися</Link></p>
          </div>
          <div className="ButtonPlace">
          <BigButton onClick={Register}>Зареєструватися</BigButton>
          </div>
        </div>
        
      );

}

export default RegisterPage