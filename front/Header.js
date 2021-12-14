import React from "react";
import "./Header.css";
import "./index.css";

const Header = () => {
	return (
		<header id="header" className="container">
			<a className="logo-container">
				<div className="img logo"></div>
				<div className="logo-text">
			<span>FilmLibrary</span>
				</div>
			</a>
			<nav>
				<a href="">Фільми</a>
				<a href="">Люди</a>
				<a className="login-button" href="">
					Вхід
				</a>
			</nav>
		</header>
	);
};

export default Header;
