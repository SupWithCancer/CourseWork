import React, { useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";
import "./PersonCatalogue.css";
import sendRequest from "./sendRequest";
import { UrlContext } from "./App";

const SectionContainer = (props) => {
	const [isCollapsed, setCollapse] = useState(false);

	const onClickHandle = function () {
		setCollapse((isCollapsed) => !isCollapsed);
	};

	return (
		<div className="search-container" style={{ maxHeight: isCollapsed ? "60px" : "600px" }}>
			<button className="collapse-button container" onClick={onClickHandle}>
				{props.heading}
				<div className="img" style={{ transform: isCollapsed ? "rotate(180deg)" : "" }}></div>
			</button>
			{props.children}
		</div>
	);
};


const PersonCard = (props) => {
	let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL  + props.image + ")" };
     return(
    <div className="person-container">
        <div className="image"style={backgroundImage}> </div>
        <div className="person-info"> 
		<Link to={"/persons/" + props.personId} className="name"> {props.name}
			</Link>
        <div className="age">{props.age} рік</div>
			</div>
        </div>)
    // </div>)
}

const SearchBar = (props)=> {
	return (
        <SectionContainer heading={props.heading}>
			<input type="search" placeholder="Пошук..." />
            </SectionContainer>
	)
};

const CheckboxFilterSection = (props) => {
	return (
		<SectionContainer heading={props.heading}>
			<input type="search" placeholder="Поиск..." />
			<div className="filter-items">{props.children}</div>
		</SectionContainer>
	);
};

const CheckboxItem = (props) => {
	return (
		<div className="checkbox-container">
			<input type="checkbox" />
			<span>{props.content}</span>
			<span className="count">({props.count})</span>
		</div>
	);
};

const PersonCatalogue = () => {
	const [persons, setPerson] = useState();
	const rootApiUrl = useContext(UrlContext);

	const [isCollapsed, setCollapse] = useState(false);
	const [isCollapsed2, setCollapse2] = useState(false);


	const onClickHandle = function () {
		setCollapse((isCollapsed) => !isCollapsed);
	};
	const onClick2Handle = function () {
		setCollapse2((isCollapsed2) => !isCollapsed2);
	};
	const onNameSearchChangeHandle = (event) => 
		sendRequest(null, rootApiUrl + "person/" + event.target.value, "GET").then(setPerson).catch(console.log)	
	

	

	useEffect(() => sendRequest(null, rootApiUrl + "person", "GET").then(setPerson).catch(console.log), []);
	return (
		<>
			<Header />
			<div className="search-container" style={{ maxHeight: isCollapsed ? "60px" : "600px" }}>
				<button className="collapse-button container" onClick={onClickHandle}>
					Пошук за ім ям...
					<div className="img" style={{ transform: isCollapsed ? "rotate(180deg)" : "" }}></div>
				</button>
				<input onChange={onNameSearchChangeHandle} type="search" placeholder="Пошук..." />
			</div>
			<div className="page-content">
				<div className="cards-container">
				{persons?.map((f) => (
						<PersonCard
					    key = {f.id}
						personId = {f.id}
						name={f.name}
						image={f.imagePath}
					
						age={f.age}
						
					/>
					))}
                        
				</div>
			</div>
		</>
	);
};



export default PersonCatalogue;
