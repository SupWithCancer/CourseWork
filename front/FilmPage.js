import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";

import "./FilmPage.css"

const FilmPoster = (props) => {
	let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + "/films/" + props.image + ")" };
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

const PersonFilms = (props) => {
	return(
	<div className="filmography">
	<Link to={"/persons/" + props.personId} className="person">{props.role}:{props.name}
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
const FilmPage = () => {
	// let { title } = useParams();

	return (
		<>
			<Header />
			<FilmPoster
		
			image="avg.jpg"/>
			<FilmInfo 
			 title="Avengers"
			 genre="Приключения"
			 desc="Асгардский бог хитрости и обмана Локи мечтает захватить Землю и таким способом отомстить своему брату Тору. Для этого он заключает сделку с повелителем мощной инопланетной расы Читаури. В обмен на несметную армию Локи обещает достать Тессеракт, четырехмерный куб, обладающий несметной силой.

			 В данный момент он хранится на научной базе секретной организации Щ.И.Т., где астрофизик Эрик Селвиг и агент Мария Хилл изучают его удивительные свойства. Вскоре они сообщают руководителю организации Нику Фьюри, что куб нестабилен и может произойти колоссальный выброс энергии. Фьюри пытается принять меры, но не успевает: Тессеракт открывает портал, через который Локи попадает на землю."
		     genreId="1"
			 year="2022"
			rating={4}/> 
			<PersonFilmsContainer heading = "Фільм створювали">
			<PersonFilms
			personId="1"
			role = "актор"
			name = "Дженніфер Лоуренс"/>
			</PersonFilmsContainer>
			<CommentInput/>
			<CommentBox
			username = "topgamer"
			content="Фильм отстой"/>
			
		</>
	);
};

export default FilmPage;
