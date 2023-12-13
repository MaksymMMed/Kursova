import React, { useEffect } from "react";
import { useState } from 'react';
import axios from "axios";
import './LoginPage.css'
import BigButton from "../../UI/button/bigButton/BigButton"
import BasicInput from "../../UI/Input/BasicInput";
import { Link,useNavigate } from "react-router-dom";

const LoginForm = () =>{

    const Navigate = useNavigate();
    const [userData,setUserData] = useState({email:'maksim2003mmm15@gmail.com',password:'qwerty123'})
    const [IsDataCorrect,setIsDataCorrect] = useState()
    const config = {
      headers: {
        'Content-Type': 'application/json'
      }
    };

    useEffect(()=>{
      setIsDataCorrect(true)
    },[])

      const LogIn = (e) =>{
        e.preventDefault();
        axios.post
        ('https:/localhost:7000/UserController/SignIn',JSON.stringify(userData),config)
        .then(response => {
          if (response.status === 200)
          {
              let userData = {
              Id : response.data.Id,
              Email : response.data.Email,
              Login : response.data.Login,
              Role : response.data.Role
            }
            localStorage.setItem("UserData",JSON.stringify(userData))
            localStorage.setItem("Token",response.data.Token)
            Navigate("/MainPage")
          }    
        })
        .catch(error => {
          setIsDataCorrect(false)
          setUserData({email:"",password:""})
          console.log(error);
        });
      }

  


    return (
        <div className="LoginForm">
          <h1>Speech Improve</h1>
          <div className="InputPlace">
            <h3>Email</h3>
            <BasicInput placeholder=" Введіть email..." value={userData.email} onChange={e=>setUserData({...userData, email : e.target.value})} />
          </div>
          <div className="InputPlace">
            <h3>Password</h3>
            <BasicInput placeholder=" Введіть пароль..." value={userData.password} onChange={e=>setUserData({...userData, password : e.target.value})} />
          </div>
          {IsDataCorrect === false
          ?
            <p style={{color:"red"}}>Неправильний логін або пароль</p>
          : null
          } 
          <div className="Other">
            <p><Link to="/SignUp">Зареєструватися</Link></p>
          </div>
          <div className="ButtonPlace">
          <BigButton onClick={LogIn}>Увійти</BigButton>
          </div>
        </div>
        
      );

      
    }


export default LoginForm