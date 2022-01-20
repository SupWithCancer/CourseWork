import React, { useState, useEffect, useContext } from "react";
import { useParams } from "react-router-dom";
import { UrlContext } from "./App";
import { Link } from "react-router-dom";
import "./index.css";
import Header from "./Header";
import sendRequest from "./sendRequest";
import "./FilmPage.css";
import CommentForm from "./CreateСomment";

const FilmPoster = (props) => {
    let backgroundImage = { backgroundImage: "url(" + process.env.PUBLIC_URL + props.image + ")" };
    return (
        <div className="filmPoster">
            <div className="image" style={backgroundImage}>
                {" "}
            </div>
        </div>
    );
};

const FilmInfo = (props) => {
    const rootUrl = useContext(UrlContext);
    const [genres, setGenres] = useState([]);

    useEffect(() => {
        const genres = [];
        sendRequest(null, rootUrl + "GenreFilm/Film/" + props.id, "GET")
            .then(async (response) => {
                await Promise.all(
                    response.map((g) =>
                        sendRequest(null, rootUrl + "genres/" + g.genreId, "GET")
                            .then((response) => genres.push(response))
                            .catch(console.log)
                    )
                );
                setGenres(genres.sort((a, b) => a.id - b.id));
            })
            .catch(console.log);
    }, []);

    return (
        <div className="info-container">
            <div className="title">{props.title}</div>
            <Link className="year" to={"/films/year/" + props.year}>
                {props.year} рік
            </Link>
            {genres ? (
                <div className="genres">
                    {genres.map((g) => (
                        <Link className="genre" to={"/films/genre/" + g.id} key={g.id}>
                            Жанр: {g.name}
                        </Link>
                    ))}
                </div>
            ) : null}
            <div className="rating-container">
                <div className="stars" style={{ "--rating": props.rating }}></div>
                {props.rating}
            </div>
            <div className="description">{props.desc} </div>
            <div className="rating-container"></div>
        </div>
    );
};

const CommentBox = (props) => {
    return (
        <>
            <div className="comment-box">
                <div className="comment-cont">{props.content}</div>
            </div>
        </>
    );
};

const PersonFilms = ({ personId, roleId }) => {
    const [person, setPerson] = useState();
    const [role, setRole] = useState();
    const rootRequestUrl = useContext(UrlContext);
    const personRequestUrl = rootRequestUrl + "person/" + personId;
    const roleRequestUrl = rootRequestUrl + "roles/" + roleId;

    useEffect(() => sendRequest(null, personRequestUrl, "GET").then(setPerson).catch(console.log), []);
    useEffect(() => sendRequest(null, roleRequestUrl, "GET").then(setRole).catch(console.log), []);
    return person && role ? (
        <div className="filmography">
            <Link to={"/persons/" + personId} className="person">
                {role.title}:{person.name}
            </Link>
        </div>
    ) : null;
};

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
    const [comments, setComments] = useState();
    const rootRequestUrl = useContext(UrlContext);
    const filmRequestUrl = rootRequestUrl + "film/" + filmId;
    const filmPersonRequestUrl = rootRequestUrl + "filmperson/film/" + filmId;
    const filmCommentRequestUrl = rootRequestUrl + "comment/film/" + filmId;

    useEffect(() => sendRequest(null, filmRequestUrl, "GET").then(setFilm).catch(console.log), []);
    useEffect(() => sendRequest(null, filmPersonRequestUrl, "GET").then(setFilmPerson).catch(console.log), []);
    useEffect(() => sendRequest(null, filmCommentRequestUrl, "GET").then(setComments).catch(console.log), []);

    return (
        <>
            <Header />
            {film ? (
                <>
                    <FilmPoster filmId={film.id} image={film.imagePath} />
                    <FilmInfo
                        id={film.id}
                        title={film.name}
                        genre={film.theme}
                        desc={film.description}
                        year={film.year}
                        rating={film.rank}
                    />
                    {filmPerson ? (
                        <PersonFilmsContainer heading="Фільм створювали">
                            {filmPerson.map((fp, num) => (
                                <PersonFilms key={num} personId={fp.personId} roleId={fp.roleId} />
                            ))}
                        </PersonFilmsContainer>
                    ) : null}
                    <CommentForm setComments={setComments} />
                    {comments ? comments.map((c) => <CommentBox key={c.commentId} userId={c.userId} content={c.body} />) : null}
                </>
            ) : null}
        </>
    );
};

export default FilmPage;
