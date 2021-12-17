import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";

import "./PersonPage.css"


const PersonPoster = (props) => {
	let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + "/persons/" + props.image + ")" };
     return(
    <div className="personPoster">
        <div className="image"style={backgroundImage}> </div>
        </div>)
    // </div>)
}

const PersonInfo = (props) => {
	return(
	<div className="info-container">
		<div className="name">{props.name}</div>
			<div className="age">{props.age} рік</div>
			
			<div className="description">{props.desc} </div>
				
	</div>)
}

const PersonFilms = (props) => {
	return(
	<div className="filmography">
	<Link to={"/films/" + props.filmId} className="film"> {props.title}
	</Link>
	</div>)
}

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

const PersonFilmsContainer = (props) => {
	return (
		<SectionContainer heading={props.heading}>
			<div className="filmogrphycontainer">{props.children}</div>
		</SectionContainer>
	);
};
const PersonPage = () => {
	// let { personId } = useParams();

	return (
		<>
			<Header />
			<PersonPoster
			image = "jnl.jpg"/>
            <PersonInfo
			name = "Дженнифер Лоуренс"
			age = "21"
			desc = "Дже́ннифер Шре́йдер Ло́уренс — американская актриса кино и телевидения. Лауреат премии «Оскар», трёх премий «Золотой глобус», премии BAFTA, двух «Премий Гильдии киноактёров США» и премии «Сатурн»."
			/>
			<PersonFilmsContainer heading = "Фільмографія">
			<PersonFilms
			filmId="1"
			title = "Мстители"/>
			</PersonFilmsContainer>
		</>
	);
};

export default PersonPage;
