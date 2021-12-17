import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";
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
	return (
		<>
			<Header />
            <SearchBar heading="Назва Фільму"/>
            <CheckboxFilterSection heading="Жанр" count="37">
						
							<CheckboxItem content="Хоррор" count="0" />
						
					</CheckboxFilterSection>
            <SearchBar heading="Ім'я творця"/>
			<div className="page-content">
				<div className="cards-container">
					<FilmCard
						title="Мстители"
						image="avg.jpg"
						filmId="1"
						genre="Приключения"
						genreId="1"
						year="2022"
						rating={4}
                        
					/>
					{[...Array(30).keys()].map((n) => (
						<FilmCard
						title="Мстители"
						image="adv.jpg"
						filmId="1"
						genre="Приключения"
						genreId="1"
						year="2022"
						rating={4}
                        
					/>
					))}
				</div>
			</div>
		</>
	);
};

export default FilmsCatalogue