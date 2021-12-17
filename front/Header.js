import React from "react";
import { Link } from "react-router-dom";
import "./Header.css";
import "./index.css";

const Header = () => {
	return (
		<header id="header" className="container">
			<Link to="/" className="logo-container">
				<div className="img logo"></div>
				<div className="logo-text">
			<span>FilmLibrary</span>
				</div>
				</Link>
			<nav>
			<Link to="/films">Фільми</Link>
			<Link to="/persons">Люди</Link>
			<Link to="/login" className="login-button" href="">
					Вхід
					</Link>
			</nav>
		</header>
	);
};

export default Header;
