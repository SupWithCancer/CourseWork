import React, { useState, useContext } from "react";
import { Link, useNavigate, useParams} from "react-router-dom";
import { UrlContext } from "./App";
import useValueSaver from "./useValueSaver";
import useToken from "./useToken";
import sendRequest from "./sendRequest";
import "./index.css";
import "./FilmPage.css"

const initialState = {
	body: ""
};

const CommentForm = ({setComments}) => {
	const navigate = useNavigate();
	const { filmId } = useParams();
	const [token] = useToken();
	const [content, setContent] = useState();
	const apiRootUrl = useContext(UrlContext);
	const filmCommentRequestUrl = apiRootUrl + "comment/film/" + filmId;
   



	const onSubmitHandle = (event) => {
		event.preventDefault();
	
		        sendRequest(
				{
					body: content,
					userId: parseInt(token.id),
					filmId: filmId
				},
				apiRootUrl + "comment",
				"POST"
			)
				.then((response) => {
					setComments((comments) => comments.concat(response))
					setContent("")
				})
				.catch(console.log);
		};
		

	return (
		<form className="form-container" onSubmit={onSubmitHandle}>
			<textarea value={content} onChange={x => setContent(x.target.value)} name="steps" type="text" placeholder="Введіть комментар" autoComplete="on" className="description" />
			<button className="sign-up-button">Запостити</button>
		</form>
	);
};


export default CommentForm;
