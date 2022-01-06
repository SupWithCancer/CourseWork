import React, { useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import { UrlContext } from "./App";
import "./index.css";
import Header from "./Header";
import sendRequest from "./sendRequest";
import "./FilmsCatalogue.css";


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

const SectionContainer2 = (props) => {
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
const FilmCard = (props) => {
	let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + "/films/" + props.image + ")" };
     return(
    <div className="film-container">
        <div className="image"style={backgroundImage}> </div>
        <div className="film-info"> 
		<Link to={"/films/" + props.filmId} className="title"> {props.title}
			</Link>
        <div className="year">{props.year} рік</div>
		<div className="genres">Жанр: {props.genre} </div>
        <div className="card-links">
			</div>
			<Link to={"/films/" + props.filmId} className="title">
			</Link>
            <div className="grade-container">
				<div className="stars" style={{ "--rating": props.rating }}>
					{/* [1, 2, 3, 4, 5].map((n) => (
						<button className="star" key={n}></button>
					)) */}
				</div>
				{props.rating}
                </div>
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

const FilmsCatalogue = () => {
	const [films, setFilms] = useState();

	const rootApiUrl = useContext(UrlContext);

	const [isCollapsed, setCollapse] = useState(false);
	const [isCollapsed2, setCollapse2] = useState(false);
	const [isCollapsed3, setCollapse3] = useState(false);



	const onClickHandle = function () {
		setCollapse((isCollapsed) => !isCollapsed);
	};
	const onClick2Handle = function () {
		setCollapse2((isCollapsed2) => !isCollapsed2);
	};
	const onClick3Handle = function () {
		setCollapse3((isCollapsed3) => !isCollapsed3);
	};
	const onNameSearchChangeHandle = (event) => 
		sendRequest(null, rootApiUrl + "film/" + event.target.value, "GET").then(setFilms).catch(console.log)	
	
	const onYearSearchChangeHandle = (event) => 
		sendRequest(null, rootApiUrl + "film/year/" + event.target.value, "GET").then(setFilms).catch(console.log)
	
	const onGenreSearchChangeHandle = (event) => 
		sendRequest(null, rootApiUrl + "film/theme/" + event.target.value, "GET").then(setFilms).catch(console.log)
	
	useEffect(() => sendRequest(null, rootApiUrl + "film/", "GET").then(setFilms).catch(console.log), []);
    //    useEffect(()=> console.log(apiRequestUrl))
	return (
		<>
			<Header />
			<div className="search-container" style={{ maxHeight: isCollapsed ? "60px" : "600px" }}>
				<button className="collapse-button container" onClick={onClickHandle}>
					Пошук за назвою...
					<div className="img" style={{ transform: isCollapsed ? "rotate(180deg)" : "" }}></div>
				</button>
				<input onChange={onNameSearchChangeHandle} type="search" placeholder="Пошук..." />
			</div>
			<div className="search-container" style={{ maxHeight: isCollapsed2 ? "60px" : "600px" }}>
				<button className="collapse-button container" onClick={onClick2Handle}>
					Пошук за роком....
					<div className="img" style={{ transform: isCollapsed2 ? "rotate(180deg)" : "" }}></div>
				</button>
				<input onChange={onYearSearchChangeHandle} type="search" placeholder="Пошук..." />
			</div>
			<div className="search-container" style={{ maxHeight: isCollapsed3 ? "60px" : "600px" }}>
				<button className="collapse-button container" onClick={onClick3Handle}>
					Пошук за жанром....
					<div className="img" style={{ transform: isCollapsed3 ? "rotate(180deg)" : "" }}></div>
				</button>
				<input onChange={onGenreSearchChangeHandle} type="search" placeholder="Пошук..." />
			</div>
		
         
			<div className="page-content">
				<div className="cards-container">	
				{films?.map((f) => (
						<FilmCard
					    key = {f.id}
						filmId = {f.id}
						title={f.name}
						image={f.imagePath}
						genre={f.theme}
						genreId={f.genreId}
						year={f.year}
						rating={f.rank}

						
                        
					/>
					))}
					
				</div>
			</div>
		</>
	);
};

export default FilmsCatalogue