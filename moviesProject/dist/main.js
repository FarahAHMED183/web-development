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
class MovieSlider {
    constructor(trackId, detailsId) {
        this.movies = [];
        this.currentIndex = 0;
        this.API_KEY = '21d6601622ce880a80939f3c1823ce8e';
        this.BASE_URL = 'https://api.themoviedb.org/3';
        this.track = this.getElementOrThrow(trackId);
        this.details = this.getElementOrThrow(detailsId);
        this.sliderContainer = this.getElementOrThrow('.slider-container');
        this.fetchMovies("spiderman");
        this.setupListeners();
    }
    getElementOrThrow(selector) {
        const element = selector.startsWith('.')
            ? document.querySelector(selector)
            : document.getElementById(selector);
        if (!element) {
            throw new Error(`Element not found: ${selector}`);
        }
        return element;
    }
    searchMovies(query) {
        return __awaiter(this, void 0, void 0, function* () {
            if (!query.trim()) {
                console.warn("Empty query provided to searchMovies");
                return;
            }
            try {
                yield this.fetchMovies(query);
            }
            catch (error) {
                console.error("Error during movie search:", error);
                this.showErrorToUser("Failed to search movies. Please try again.");
            }
        });
    }
    fetchMovies(query) {
        return __awaiter(this, void 0, void 0, function* () {
            console.log("Fetching movies for:", query);
            try {
                const url = `${this.BASE_URL}/search/movie?api_key=${this.API_KEY}&query=${encodeURIComponent(query)}`;
                const response = yield fetch(url);
                if (!response.ok) {
                    throw new Error(`API request failed with status ${response.status}`);
                }
                const data = yield response.json();
                this.movies = data.results || [];
                if (this.movies.length === 0) {
                    this.showNoResultsMessage();
                    return;
                }
                this.currentIndex = 0;
                this.render();
                this.updateBackgroundAndDetails();
            }
            catch (error) {
                console.error("Failed to fetch movies", error);
                this.showErrorToUser("Failed to load movies. Please check your connection.");
            }
        });
    }
    showNoResultsMessage() {
        this.track.innerHTML = '<div class="no-results">No movies found. Try a different search.</div>';
        this.details.innerHTML = '';
        this.sliderContainer.style.backgroundImage = '';
    }
    showErrorToUser(message) {
        this.track.innerHTML = `<div class="error-message">${message}</div>`;
    }
    setupListeners() {
        var _a, _b;
        (_a = document.getElementById("prev")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => this.navigate(-1));
        (_b = document.getElementById("next")) === null || _b === void 0 ? void 0 : _b.addEventListener("click", () => this.navigate(1));
    }
    navigate(direction) {
        const newIndex = this.currentIndex + direction;
        if (newIndex >= 0 && newIndex < this.movies.length) {
            this.currentIndex = newIndex;
            this.render();
            this.updateBackgroundAndDetails();
        }
    }
    render() {
        this.track.innerHTML = "";
        this.movies.forEach((movie, index) => {
            const card = document.createElement("div");
            card.className = `movie-card ${index === this.currentIndex ? "focused" : ""}`;
            card.innerHTML = `
        <img src="${this.getPosterUrl(movie.poster_path)}" alt="${movie.title}" />
        <div class="overlay"><h4>${movie.title}</h4></div>
      `;
            this.track.appendChild(card);
            if (index === this.currentIndex) {
                setTimeout(() => {
                    card.scrollIntoView({
                        behavior: "smooth",
                        inline: "center",
                        block: "nearest"
                    });
                }, 100);
            }
        });
    }
    getPosterUrl(path) {
        return path
            ? `https://image.tmdb.org/t/p/w300${path}`
            : 'https://via.placeholder.com/300x450?text=No+Poster';
    }
    updateBackgroundAndDetails() {
        var _a;
        const movie = this.movies[this.currentIndex];
        if (!movie)
            return;
        this.sliderContainer.style.backgroundImage = movie.backdrop_path
            ? `url(https://image.tmdb.org/t/p/original${movie.backdrop_path})`
            : '';
        const shortOverview = movie.overview.slice(0, 120);
        const isLong = movie.overview.length > 120;
        const releaseYear = ((_a = movie.release_date) === null || _a === void 0 ? void 0 : _a.split('-')[0]) || 'N/A';
        const rating = movie.vote_average ? movie.vote_average.toFixed(1) : 'N/A';
        this.details.innerHTML = `
      <div class="movie-info">
        <h1>${movie.title}</h1>
        <div class="movie-meta">
          <span class="rating">⭐ ${rating}</span>
          <span class="year">${releaseYear}</span>
          <span class="genre">${this.getGenreNames(movie.genre_ids)}</span>
        </div>
        <p class="overview">
          <span class="short-text">${shortOverview}${isLong ? "..." : ""}</span>
          ${isLong ? `<span class="full-text hidden">${movie.overview}</span>` : ''}
          ${isLong ? '<button class="toggle-overview">Read more</button>' : ""}
        </p>
        <div class="movie-buttons">
          <button class="trailer-btn">Watch trailer</button>
          <button class="watch-btn">Watch now</button>
        </div>
      </div>
    `;
        this.setupOverviewToggle();
    }
    getGenreNames(genreIds) {
        // In a real app, you'd map these IDs to names from the API
        if (!genreIds || genreIds.length === 0)
            return 'Genre not specified';
        return genreIds.slice(0, 2).map(id => `Genre ${id}`).join(', ');
    }
    setupOverviewToggle() {
        const toggle = this.details.querySelector(".toggle-overview");
        if (!toggle)
            return;
        const shortText = this.details.querySelector(".short-text");
        const fullText = this.details.querySelector(".full-text");
        toggle.addEventListener("click", () => {
            const isExpanded = toggle.textContent === "Read less";
            if (isExpanded) {
                fullText.classList.add("hidden");
                shortText.classList.remove("hidden");
                toggle.textContent = "Read more";
            }
            else {
                fullText.classList.remove("hidden");
                shortText.classList.add("hidden");
                toggle.textContent = "Read less";
            }
        });
    }
}
// DOM Initialization
document.addEventListener("DOMContentLoaded", () => {
    try {
        const slider = new MovieSlider("slider-track", "details-center");
        setupSearchControls(slider);
    }
    catch (error) {
        console.error("Initialization error:", error);
        document.body.innerHTML = `<div class="error">Failed to initialize the application. Please refresh.</div>`;
    }
});
function setupSearchControls(slider) {
    const toggleSearch = document.getElementById("toggle-search");
    const dropdownSearch = document.getElementById("dropdown-search");
    const searchInput = document.getElementById("search-input");
    const searchButton = document.getElementById("search-btn");
    if (!toggleSearch || !dropdownSearch || !searchInput || !searchButton) {
        console.error("Search controls not found");
        return;
    }
    toggleSearch.addEventListener("click", (e) => {
        e.preventDefault();
        dropdownSearch.classList.toggle("hidden");
        if (!dropdownSearch.classList.contains("hidden")) {
            searchInput.focus();
        }
    });
    const performSearch = () => {
        const query = searchInput.value.trim();
        if (query) {
            slider.searchMovies(query);
            dropdownSearch.classList.add("hidden");
        }
    };
    searchButton.addEventListener("click", performSearch);
    searchInput.addEventListener("keydown", (e) => e.key === "Enter" && performSearch());
}
