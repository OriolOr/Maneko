import React from "react";
import "./balance.styles.css"


interface BalanceProps {
    value: number;
}

const Balance:React.FC<BalanceProps> = (props) => {
    if (props.value>0){
        return <span className = "positiveValue">+ {props.value} €</span>
    }
    else {
        return <span className = "negativeValue">- {Math.abs(props.value)} €</span>
    }
}

export default Balance;