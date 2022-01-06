import React, { useState, useEffect, useContext } from "react";
import { useParams } from "react-router-dom";
import { UrlContext } from "./App";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";
import sendRequest from "./sendRequest";
import "./FilmPage.css"

const FilmPoster = (props) => {
	let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + props.image + ")" };
     return(
    <div className="filmPoster">
        <div className="image"style={backgroundImage}> </div>
        </div>)
    // </div>)
}

const FilmInfo = (props) => {
	return(
	<div className="info-container">
		<div className="title">{props.title}</div>
			<div className="year">{props.year} рік</div>
			<div className="genres">Жанр: {props.genre} </div>
			<div className="rating-container">
					<div className="stars" style={{ "--rating": props.rating }}>
						{/* [1, 2, 3, 4, 5].map((n) => (
							<button className="star" key={n}></button>
						)) */}
					</div>
					{props.rating}
					</div>
			<div className="description">{props.desc} </div>
				<div className="rating-container">
				
					</div>
	</div>)
}
const CommentInput = () => {
	return (
		<div className="comment-input">
			<input type="text" placeholder="Введіть комментар" />
			
		</div>
	);
};

const CommentBox = (props) =>{
	return(
        <><div className="username">{props.username}</div><div className="comment-box">
			<div className="comment-cont">{props.content}</div>
		</div></>
	)
}

const PersonFilms = ({personId, roleId}) => {
	const [person, setPerson] = useState();
	const [role, setRole] = useState();
	const rootRequestUrl = useContext(UrlContext);
	const personRequestUrl = rootRequestUrl + "person/" + personId;
	const roleRequestUrl = rootRequestUrl + "roles/" + roleId;
    
	useEffect(() => sendRequest(null, personRequestUrl, "GET").then(setPerson).catch(console.log), []);
	useEffect(() => sendRequest(null, roleRequestUrl, "GET").then(setRole).catch(console.log), []);
	return(
		(person && role) ?  
	(<div className="filmography">
	<Link to={"/persons/" + personId} className="person">{role.title}:{person.name}
	</Link>
	</div>) : null  )
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
const FilmPage = () => {
	// let { title } = useParams();
	const { filmId } = useParams();
	const [film, setFilm] = useState();
	const [filmPerson, setFilmPerson] = useState();
	const rootRequestUrl = useContext(UrlContext);
	const filmRequestUrl = rootRequestUrl + "film/" + filmId;
	const filmPersonRequestUrl = rootRequestUrl + "filmperson/film/" + filmId;
    
	useEffect(() => sendRequest(null, filmRequestUrl, "GET").then(setFilm).catch(console.log), []);
	useEffect(() => sendRequest(null, filmPersonRequestUrl, "GET").then(setFilmPerson).catch(console.log), []);
	return (
		<>
			<Header />
			{film ? (
             
			<><FilmPoster
		    filmId = {film.filmId}
			image={film.imagePath}/>
			<FilmInfo 
			title={film.name}
			 genre={film.theme}
			 desc= {film.description}
			 year={film.year}
			rating={film.rank}/> 
			{filmPerson ? (
			<PersonFilmsContainer heading = "Фільм створювали">
	           {filmPerson.map(fp => 
			<PersonFilms
			personId={fp.personId}
			roleId =  {fp.roleId} /> )}
			</PersonFilmsContainer>
			) : null}
			<CommentInput/>
			<CommentBox
			username = "topgamer"
			content="Фильм отстой"/>
			
		</>
		) : null} 
		</>
	);
};

export default FilmPage;
