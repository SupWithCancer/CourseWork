import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";
import PrivateRoute from "./PrivateRoute";
import FilmsCatalogue from "./FilmsCatalogue";
import FilmPage from "./FilmPage";
import PersonPage from "./PersonPage";
import LoginPage from "./LoginPage";
import PersonCatalogue from "./PersonCatalogue";

const App = () => {
	return (
		<Router>
			<Routes>
				<Route path="/" element={<FilmsCatalogue />} />
				<Route path="/films" element={<FilmsCatalogue />} />
				<Route path="/films/:filmId" element={<FilmPage />} />
				<Route path="/persons" element={<PersonCatalogue />} />
				<Route path="/persons/:personId" element={<PersonPage />} />
				<Route path="/login" element={<LoginPage />} />
				<Route path="*" element={<FilmsCatalogue />} />
			</Routes>
		</Router>
	);
};

export default App;