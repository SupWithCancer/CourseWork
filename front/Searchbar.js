import React from "react";
import './index.css';
import './SearchBar.css';
import SearchIcon from "@material-ui/icons/Search";
function SearchBar({placeholder, data}) {
    return (
        <div className="search">
            <div className="searchInputs">
                <input type="text" placeholder={placeholder}/>
                 <div className="searchIcon" >
                     <SearchIcon />
                 </div>
            </div>
            <div className="dataResult"></div>
        </div>
    )
}


export default SearchBar;
