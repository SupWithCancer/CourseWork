import React, { useState, useEffect, useContext } from "react";
import { useParams } from "react-router-dom";
import { UrlContext } from "./App";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";
import sendRequest from "./sendRequest";
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

const PersonFilms = ({filmId}) => {

		const [film, setFilm] = useState();
	const rootRequestUrl = useContext(UrlContext);
	const filmRequestUrl = rootRequestUrl + "film/" + filmId;

    
	useEffect(() => sendRequest(null, filmRequestUrl, "GET").then(setFilm).catch(console.log), []);
	return(
		film ? 
	(<div className="filmography">
	<Link to={"/films/" + filmId} className="film">   "{film.name}"
	</Link>
	</div>) : null ) 
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
	const { personId } = useParams();
	const [person, setPerson] = useState();
	const [filmPerson, setFilmPerson] = useState();
	const rootRequestUrl = useContext(UrlContext);
	const personRequestUrl = rootRequestUrl + "person/" + personId;
	
	const filmPersonRequestUrl = rootRequestUrl + "filmperson/person/" + personId;
   

	useEffect(() => sendRequest(null, personRequestUrl, "GET").then(setPerson).catch(console.log), []);
	useEffect(() => sendRequest(null, filmPersonRequestUrl, "GET").then(setFilmPerson).catch(console.log), []);

	return (
		<>
			<Header />
			{person ? (
			<><PersonPoster
			        personId = {person.id}
					image={person.imagePath} /><PersonInfo
						name={person.name}
						age= {person.age}
						desc={person.description} />
						{filmPerson ? (
						<PersonFilmsContainer heading="Фільмографія">
						{filmPerson.map(fp => 
						<PersonFilms
							filmId={fp.filmId}
							 /> )}
					</PersonFilmsContainer>	) : null}</> 
			) : null}
			
		</>
	);
};

export default PersonPage;
