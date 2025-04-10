interface Movie {
  title: string;
  poster_path: string;
  backdrop_path: string;
  overview: string;
  vote_average?: number;
  release_date?: string;
  genre_ids?: number[];
}

class MovieSlider {
  private movies: Movie[] = [];
  private currentIndex = 0;
  private track: HTMLElement;
  private details: HTMLElement;
  private sliderContainer: HTMLElement;
  private readonly API_KEY = '21d6601622ce880a80939f3c1823ce8e';
  private readonly BASE_URL = 'https://api.themoviedb.org/3';

  constructor(trackId: string, detailsId: string) {
    this.track = this.getElementOrThrow(trackId);
    this.details = this.getElementOrThrow(detailsId);
    this.sliderContainer = this.getElementOrThrow('.slider-container');

    this.fetchMovies("spiderman");
    this.setupListeners();
  }

  private getElementOrThrow(selector: string): HTMLElement {
    const element = selector.startsWith('.') 
      ? document.querySelector(selector)
      : document.getElementById(selector);

    if (!element) {
      throw new Error(`Element not found: ${selector}`);
    }
    return element as HTMLElement;
  }

  public async searchMovies(query: string): Promise<void> {
    if (!query.trim()) {
      console.warn("Empty query provided to searchMovies");
      return;
    }
    
    try {
      await this.fetchMovies(query);
    } catch (error) {
      console.error("Error during movie search:", error);
      this.showErrorToUser("Failed to search movies. Please try again.");
    }
  }

  private async fetchMovies(query: string): Promise<void> {
    console.log("Fetching movies for:", query);
    
    try {
      const url = `${this.BASE_URL}/search/movie?api_key=${this.API_KEY}&query=${encodeURIComponent(query)}`;
      const response = await fetch(url);

      if (!response.ok) {
        throw new Error(`API request failed with status ${response.status}`);
      }

      const data = await response.json();
      this.movies = data.results || [];
      
      if (this.movies.length === 0) {
        this.showNoResultsMessage();
        return;
      }

      this.currentIndex = 0;
      this.render();
      this.updateBackgroundAndDetails();
    } catch (error) {
      console.error("Failed to fetch movies", error);
      this.showErrorToUser("Failed to load movies. Please check your connection.");
    }
  }

  private showNoResultsMessage(): void {
    this.track.innerHTML = '<div class="no-results">No movies found. Try a different search.</div>';
    this.details.innerHTML = '';
    this.sliderContainer.style.backgroundImage = '';
  }

  private showErrorToUser(message: string): void {
    this.track.innerHTML = `<div class="error-message">${message}</div>`;
  }

  private setupListeners(): void {
    document.getElementById("prev")?.addEventListener("click", () => this.navigate(-1));
    document.getElementById("next")?.addEventListener("click", () => this.navigate(1));
  }

  private navigate(direction: -1 | 1): void {
    const newIndex = this.currentIndex + direction;
    
    if (newIndex >= 0 && newIndex < this.movies.length) {
      this.currentIndex = newIndex;
      this.render();
      this.updateBackgroundAndDetails();
    }
  }

  private render(): void {
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

  private getPosterUrl(path: string): string {
    return path 
      ? `https://image.tmdb.org/t/p/w300${path}`
      : 'https://via.placeholder.com/300x450?text=No+Poster';
  }

  private updateBackgroundAndDetails(): void {
    const movie = this.movies[this.currentIndex];
    if (!movie) return;
  
    this.sliderContainer.style.backgroundImage = movie.backdrop_path
      ? `url(https://image.tmdb.org/t/p/original${movie.backdrop_path})`
      : '';
  
    const shortOverview = movie.overview.slice(0, 120);
    const isLong = movie.overview.length > 120;
    const releaseYear = movie.release_date?.split('-')[0] || 'N/A';
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

  private getGenreNames(genreIds?: number[]): string {
    // In a real app, you'd map these IDs to names from the API
    if (!genreIds || genreIds.length === 0) return 'Genre not specified';
    return genreIds.slice(0, 2).map(id => `Genre ${id}`).join(', ');
  }

  private setupOverviewToggle(): void {
    const toggle = this.details.querySelector(".toggle-overview") as HTMLButtonElement;
    if (!toggle) return;

    const shortText = this.details.querySelector(".short-text") as HTMLElement;
    const fullText = this.details.querySelector(".full-text") as HTMLElement;

    toggle.addEventListener("click", () => {
      const isExpanded = toggle.textContent === "Read less";
      
      if (isExpanded) {
        fullText.classList.add("hidden");
        shortText.classList.remove("hidden");
        toggle.textContent = "Read more";
      } else {
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
  } catch (error) {
    console.error("Initialization error:", error);
    document.body.innerHTML = `<div class="error">Failed to initialize the application. Please refresh.</div>`;
  }
});

function setupSearchControls(slider: MovieSlider): void {
  const toggleSearch = document.getElementById("toggle-search");
  const dropdownSearch = document.getElementById("dropdown-search");
  const searchInput = document.getElementById("search-input") as HTMLInputElement;
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