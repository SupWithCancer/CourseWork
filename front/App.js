import React, { createContext } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";
import PrivateRoute from "./PrivateRoute";
import FilmsCatalogue from "./FilmsCatalogue";
import FilmPage from "./FilmPage";
import PersonPage from "./PersonPage";
import LoginPage from "./LoginPage";
import PersonCatalogue from "./PersonCatalogue";
import RegistrationPage from "./RegistrationPage";

const apiUrl = "https://localhost:7017/api/";
const UrlContext = createContext(apiUrl);
const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<FilmsCatalogue />} />
                <Route path="/films" element={<FilmsCatalogue />} />
                <Route path="/films/:filmId" element={<FilmPage />} />
                <Route path="/films/genre/:genreId" element={<FilmsCatalogue />} />
                <Route path="/films/year/:year" element={<FilmsCatalogue />} />
                <Route path="/persons" element={<PersonCatalogue />} />
                <Route path="/persons/:personId" element={<PersonPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/sign-up" element={<RegistrationPage />} />
                <Route path="*" element={<FilmsCatalogue />} />
            </Routes>
        </Router>
    );
};

export default App;

export { UrlContext };
