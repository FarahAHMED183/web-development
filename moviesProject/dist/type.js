"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var _a, _b;
class Movie {
    constructor(data) {
        this.title = data.title;
        this.vote_average = data.vote_average;
        this.release_date = data.release_date;
        this.original_language = data.original_language;
        this.backdrop_path = data.backdrop_path;
        this.overview = data.overview;
        if (this.backdrop_path) {
            this.posterUrl = `https://image.tmdb.org/t/p/original${this.backdrop_path}`;
        }
    }
    get releaseYear() {
        return this.release_date.slice(0, 4);
    }
    get languageUpperCase() {
        return this.original_language.toUpperCase();
    }
    getShortOverview(maxLength = 100) {
        return this.overview.length > maxLength ? `${this.overview.slice(0, maxLength)}...` : this.overview;
    }
}
const API_URL = "https://api.themoviedb.org/3/search/movie?api_key=21d6601622ce880a80939f3c1823ce8e&query=spiderman";
let movies = [];
let currentIndex = 0;
function fetchMovies() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield fetch(API_URL);
            const data = yield response.json();
            // Map the raw results to Movie class instances
            movies = data.results.map((movieData) => new Movie(movieData));
            if (movies.length > 0) {
                displayMovie(currentIndex);
            }
        }
        catch (error) {
            console.error("Error fetching movie data:", error);
        }
    });
}
function displayMovie(index) {
    const movie = movies[index];
    const banner = document.getElementById("banner");
    const title = document.getElementById("movie-title");
    const info = document.getElementById("movie-info");
    const desc = document.getElementById("movie-desc");
    if (movie.posterUrl) {
        banner.style.backgroundImage = `url(${movie.posterUrl})`;
    }
    else {
        banner.style.backgroundImage = ''; // Clear background if no backdrop
    }
    title.textContent = movie.title;
    info.innerHTML = `⭐️ ${movie.vote_average} &nbsp; • &nbsp; ${movie.releaseYear} &nbsp; • &nbsp; ${movie.languageUpperCase}`;
    desc.textContent = movie.overview;
}
(_a = document.getElementById("nextBtn")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => {
    if (movies.length > 0) {
        currentIndex = (currentIndex + 1) % movies.length;
        displayMovie(currentIndex);
    }
});
(_b = document.getElementById("prevBtn")) === null || _b === void 0 ? void 0 : _b.addEventListener("click", () => {
    if (movies.length > 0) {
        currentIndex = (currentIndex - 1 + movies.length) % movies.length;
        displayMovie(currentIndex);
    }
});
fetchMovies();
