import React from "react";
import Classes from "./BasicSelect.module.css"

const BasicSelect = ({options,onChange}) =>{
    return(
        <select className={Classes.BasicSelect} onChange={onChange}>
            {options.map(opt=>
                <option key={opt.value} value ={opt.value}>
                    {opt.name}
                </option>
            )}
      </select>
    )
}

export default BasicSelect