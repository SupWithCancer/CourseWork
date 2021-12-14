import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Header from "./Header.js";
import SearchBar from "./Searchbar.js"

ReactDOM.render(

	<>
		<Header />
	  <SearchBar placeholder="Ведіть назву фільму або ім'я людини" />
	</>,
	document.getElementById("root")
);
