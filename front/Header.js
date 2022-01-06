import React from "react";
import { Link } from "react-router-dom";
import useToken from "./useToken";
import "./Header.css";
import "./index.css";


const NavBar = () => {
	const [token, setToken] = useToken();

  if(!token)
	return (
		<nav className="header-nav container">

			<Link to="/films">Фільми</Link>
			<Link to="/persons">Люди</Link>
			<Link to="/login" className="login-button" href="">
					Вхід
					</Link>
			</nav>
	
	)
	else
	return (
		<nav className="header-nav container">
			<Link to="/films">Фільми</Link>
			<Link to="/persons">Люди</Link>
			<div>
				<div className="user-info">
					{token.name} 
				</div>
				<button className="sign-out-button" onClick={() => setToken(null)}>
					Вийти
				</button>
			</div>
		</nav>
	);
};


const Header = () => {
	return (
		<header id="header" className="container">
		<Link to="/" className="logo-container">
			<div className="img logo"></div>
			<div className="logo-text">
		<span>FilmLibrary</span>
			</div>
			</Link>
			<NavBar />
		</header>
	);
};

export default Header;
