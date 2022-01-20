import React, { useContext, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import useValueSaver from "./useValueSaver";
import useToken from "./useToken";
import sendRequest from "./sendRequest";
import { UrlContext } from "./App";
import Header from "./Header";
import "./index.css";
import "./Authorization.css";

const initialState = {
	email: "",
	name: "",
	password: "",
};

const RegistrationForm = () => {
	const navigate = useNavigate();
	const [fields, setValue] = useValueSaver(initialState);
	const [token, setToken] = useToken();
	const apiRequestUrl = useContext(UrlContext) + "account/register";

	useEffect(() => {
		if (token) navigate("/", { replace: true });
	}, [token]);

	const onSubmitHandle = async (event) => {
		event.preventDefault();

		await sendRequest(
			{
				email: fields.email,
				password: fields.password,
				name: fields.name,
				
			},
			apiRequestUrl,
			"POST"
		)
			.then(setToken)
			.catch(console.log);
	};

	const passwordRegExp = "^(?=.*[a-z])(?=.*[A-Z])(?!=.*s).*$",
		passwordMinLength = 6,
		passwordMaxLenght = 32,
		passwordTitle = `Будь ласка, введіть хоча б 1 символ верхнього і нижнього регістрів, та принаймні 1 цифру
						   Пароль повинен містити від ${passwordMinLength} до ${passwordMaxLenght} символів`;
	return (
		<form className="form-container" onSubmit={onSubmitHandle}>
			<div className="container">
				<input value={fields.email} onChange={setValue} name="email" type="email" placeholder="Введіть пошту" required />
				<input value={fields.password} onChange={setValue} name="password" type="password" placeholder="Введіть пароль" pattern={passwordRegExp} title={passwordTitle} minlength={passwordMinLength} maxlength={passwordMaxLenght} />
			</div>
			<div className="container">
				<input value={fields.name} onChange={setValue} name="name" type="text" placeholder="Введіть ім'я користувача" minlength="2" maxlength="32" required />
				
			</div>
			<button className="sign-up-button">Реєстрація</button>
			<Link to="/login" className="link">
				Вже зарєєстровані?
			</Link>
		</form>
	);
};

const RegistrationPage = () => {
	return (
		<>
			<Header />
			<RegistrationForm />
		</>
	);
};

export default RegistrationPage;
