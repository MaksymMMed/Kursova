import React, { useEffect, useState } from "react"

const Result = ({Dist}) =>{

    const [Letter,setLetter] = useState("0")

    useEffect(()=>{
        
        var letter= "F"
        if(Dist>=181)
            letter = "F"
        if(Dist<=180)
            letter = "D"
        if(Dist<=160)
            letter = "C"
        if(Dist<=140)
            letter = "B"
        if(Dist<=125)
            letter = "A"
        if(Dist=="N")
            letter= "Немає"
        setLetter(letter)
    }
    ,[Dist])

    return(
        <div style ={{display: "flex", flexDirection: "column", alignItems: "center" }}>
            <h2>Ваш результат {Letter}</h2>
        </div>
    )
}

export default Result