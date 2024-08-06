import React from "react";
import "./header.styles.css"
import logo from "../../assets/logo.png";
import { Link } from "react-router-dom";

export const Header:React.FC = () => {
    return (
        <div className = "headerContainer">
            <div className = "headerLogo">
            <img src={logo} alt="Logo"  width="32" height="32" />
            <span>MANEKO</span>
            </div>
            <div className = "headerRow">
                <Link to= "/main"><div className = "headerElement">Ballance</div></Link>
                <Link to= "/homeMortage"><div className = "headerElement">Home Mortage</div></Link>
                <Link to= "/stock"><div className = "headerElement">Stock</div></Link>
            </div>

            <div className = "headerEnd">
            <Link to= "/login"><div className = "headerElement">Login</div></Link>
            </div>
        </div>
    )
};

export default Header;

