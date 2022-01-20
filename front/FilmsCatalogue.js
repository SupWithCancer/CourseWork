import React, { useState, useContext, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
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

const FilmCard = (props) => {
    let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + "/films/" + props.image + ")" };
    return (
        <div className="film-container">
            <div className="image" style={backgroundImage}>
                {" "}
            </div>
            <div className="film-info">
                <Link to={"/films/" + props.filmId} className="title">
                    {" " + props.title}
                </Link>
                <div className="year">{props.year} рік</div>
                <div className="genres">Жанр: {props.genre} </div>
                <div className="card-links"></div>
                <Link to={"/films/" + props.filmId} className="title"></Link>
                <div className="grade-container">
                    <div className="stars" style={{ "--rating": props.rating }}></div>
                    {props.rating}
                </div>
            </div>
        </div>
    );
};

const FilmsCatalogue = () => {
    const { year, genreId } = useParams();
    const rootApiUrl = useContext(UrlContext);
    const [films, setFilms] = useState();
    const [isCollapsed, setCollapse] = useState(false);

    const requestUrl = year
        ? `${rootApiUrl}film/year/${year}`
        : genreId || genreId === 0
        ? `${rootApiUrl}GenreFilm/genre/${genreId}`
        : `${rootApiUrl}film/`;
    const onClickHandle = function () {
        setCollapse((isCollapsed) => !isCollapsed);
    };

    const onNameSearchChangeHandle = (event) =>
        sendRequest(null, rootApiUrl + "film/" + event.target.value, "GET")
            .then(setFilms)
            .catch(console.log);

    useEffect(() => {
        if (!(genreId || genreId === 0)) sendRequest(null, requestUrl, "GET").then(setFilms).catch(console.log);
        else {
            const films = [];
            sendRequest(null, requestUrl, "GET")
                .then(async (response) => {
                    await Promise.all(
                        response.map((gf) =>
                            sendRequest(null, rootApiUrl + "film/" + gf.filmId, "GET")
                                .then((response) => films.push(response))
                                .catch(console.log)
                        )
                    );
                    setFilms(films.sort((a, b) => a.id - b.id));
                })
                .catch(console.log);
        }
    }, []);

    return (
        <>
            <Header />
            <div className="search-container" style={{ maxHeight: isCollapsed ? "60px" : "600px" }}>
                <button className="collapse-button container" onClick={onClickHandle}>
                    Пошук....
                    <div className="img" style={{ transform: isCollapsed ? "rotate(180deg)" : "" }}></div>
                </button>
                <input onChange={onNameSearchChangeHandle} type="search" placeholder="Пошук..." />
            </div>

            <div className="page-content">
                <div className="cards-container">
                    {films?.map((f) => (
                        <FilmCard
                            key={f.id}
                            filmId={f.id}
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

export default FilmsCatalogue;
