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
        return this.overview.length > maxLength
            ? `${this.overview.slice(0, maxLength)}...`
            : this.overview;
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
            movies = data.results.map((movieData) => new Movie(movieData));
            if (movies.length > 0) {
                displayFeaturedMovie(currentIndex);
                displayMovieCards();
            }
        }
        catch (error) {
            console.error("Error fetching movie data:", error);
        }
    });
}
function displayFeaturedMovie(index) {
    const movie = movies[index];
    const banner = document.getElementById("banner");
    const title = document.getElementById("movie-title");
    const info = document.getElementById("movie-info");
    const desc = document.getElementById("movie-desc");
    const tagline = document.getElementById("movie-tagline");
    if (movie.posterUrl) {
        banner.style.backgroundImage = `linear-gradient(to right, rgba(0, 0, 0, 0.8), rgba(0, 0, 0, 0.4)), url(${movie.posterUrl})`;
    }
    // Format title with line breaks
    const titleParts = splitTitle(movie.title);
    title.innerHTML = titleParts.join('<br>');
    info.innerHTML = `
      <span class="rating">⭐ ${movie.vote_average.toFixed(1)} (${(movie.vote_average * 1.5).toFixed(2)})</span>
      <span class="year">• ${movie.releaseYear}</span>
      <span class="runtime">• ${getRandomRuntime()}</span>
      <span class="genre">• ${getRandomGenre()}</span>
  `;
    desc.innerHTML = `
      ${movie.getShortOverview(150)}
      <a href="#" class="see-more">See more</a>
  `;
    tagline.textContent = generateTagline(movie.title);
}
function splitTitle(title) {
    const words = title.split(' ');
    if (words.length <= 3)
        return [title];
    const midPoint = Math.ceil(words.length / 2);
    return [
        words.slice(0, midPoint).join(' '),
        words.slice(midPoint).join(' ')
    ];
}
function generateTagline(title) {
    const phrases = [
        "Is that ATA?",
        "The legend returns",
        "A new adventure begins",
        "The ultimate showdown",
        "Beyond the spider-verse"
    ];
    return `${title.toUpperCase()}<br>${phrases[Math.floor(Math.random() * phrases.length)]}`;
}
function displayMovieCards() {
    const container = document.getElementById("movie-cards-container");
    container.innerHTML = "";
    movies.slice(0, 10).forEach((movie, index) => {
        const card = document.createElement("div");
        card.className = "movie-card";
        card.addEventListener('click', () => {
            currentIndex = index;
            displayFeaturedMovie(currentIndex);
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
        card.innerHTML = `
          <img class="movie-poster" src="${movie.posterUrl || 'https://via.placeholder.com/500x750?text=No+Image'}" alt="${movie.title}" />
          <div class="movie-content">
              <div class="movie-title">${movie.title}</div>
              <div class="movie-info">
                  <span class="rating">⭐ ${movie.vote_average.toFixed(1)}</span>
                  <span class="year">• ${movie.releaseYear}</span>
                  <span class="language">• ${movie.languageUpperCase}</span>
              </div>
              <div class="movie-overview">${movie.getShortOverview(80)}</div>
          </div>
      `;
        container.appendChild(card);
    });
}
// Helper functions
function getRandomRuntime() {
    const hours = Math.floor(Math.random() * 2) + 1;
    const minutes = Math.floor(Math.random() * 60);
    return `${hours}h ${minutes}m`;
}
function getRandomGenre() {
    const genres = ['Action', 'Sci-fi', 'Adventure', 'Drama', 'Comedy', 'Thriller'];
    return genres[Math.floor(Math.random() * genres.length)];
}
// Event listeners
(_a = document.getElementById("nextBtn")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => {
    if (movies.length > 0) {
        currentIndex = (currentIndex + 1) % movies.length;
        displayFeaturedMovie(currentIndex);
    }
});
(_b = document.getElementById("prevBtn")) === null || _b === void 0 ? void 0 : _b.addEventListener("click", () => {
    if (movies.length > 0) {
        currentIndex = (currentIndex - 1 + movies.length) % movies.length;
        displayFeaturedMovie(currentIndex);
    }
});
// Initialize
fetchMovies();
