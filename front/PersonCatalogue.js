import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";
import "./PersonCatalogue.css";

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
	let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + "/persons/" + props.image + ")" };
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
	return (
		<>
			<Header />
            <SearchBar heading="Ім'я людини"/>
            <CheckboxFilterSection heading="Профессія" count="37">
						
							<CheckboxItem content="Актор" count="0" />
						
					</CheckboxFilterSection>
			<div className="page-content">
				<div className="cards-container">
					<PersonCard
						name="Дженнифер Лоуренс"
						image="jnl.jpg"
						personId="1"
						age="21"
						
                        
					/>
					{[...Array(30).keys()].map((n) => (
					
						<PersonCard
						name="Дженнифер Лоуренс"
						image="jnl.jpg"
						personId="1"
						age="21"
						
                        
					/>
                        
				
					))}
				</div>
			</div>
		</>
	);
};



export default PersonCatalogue;
