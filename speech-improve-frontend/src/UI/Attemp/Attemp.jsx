import React, {useState,useEffect} from "react";

const Attemp = ({item}) =>{


    const [Letter,setLetter] = useState("0")

    useEffect(()=>{
        
        var letter= "F"
        if(item.Accuracy>=181)
            letter = "F"
        if(item.Accuracy<=180)
            letter = "D"
        if(item.Accuracy<=160)
            letter = "C"
        if(item.Accuracy<=140)
            letter = "B"
        if(item.Accuracy<=125)
            letter = "A"
        setLetter(letter)
    }
    ,[])

    const formattedDateTime = new Date(item.DateTime).toLocaleString('en-GB', {
        year: 'numeric',
        month: 'short',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
      });

    return(
        <div style={{display:"flex",justifyContent:"space-between",width:"350px",height:"60px",background:"white",marginTop:"25px",padding:" 0 15px 0 15px"
        ,borderRadius:"15px"}}>
            <p>Оцінка: {Letter}</p>
            <p>Дата: {formattedDateTime}</p>
        </div>
    )
}

export default Attemp