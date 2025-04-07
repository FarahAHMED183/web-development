interface Movie {
  title: string;
  poster_path: string;
  backdrop_path: string;
  overview: string;
}

class MovieSlider {
  private movies: Movie[] = [];
  private currentIndex = 0;
  private track: HTMLElement;
  private details: HTMLElement;
  private sliderContainer: HTMLElement;

  constructor(trackId: string, detailsId: string) {
    const trackEl = document.getElementById(trackId);
    const detailsEl = document.getElementById(detailsId);
    const sliderEl = document.querySelector(".slider-container");

    if (!trackEl || !detailsEl || !sliderEl) {
      throw new Error("Missing elements with provided IDs or .slider-container.");
    }

    this.track = trackEl;
    this.details = detailsEl;
    this.sliderContainer = sliderEl as HTMLElement;

    this.fetchMovies("spiderman");
    this.setupListeners();
  }

  private async fetchMovies(query: string) {
    console.log("Fetching movies for:", query); 
    try {
      const res = await fetch(
        `https://api.themoviedb.org/3/search/movie?api_key=21d6601622ce880a80939f3c1823ce8e&query=${encodeURIComponent(query)}`
      );
      const data = await res.json();
      console.log("Received data:", data); 
      this.movies = data.results;
      this.currentIndex = 0;
      this.render();
      this.updateBackgroundAndDetails();
    } catch (error) {
      console.error("Failed to fetch movies", error);
    }
  }

  public searchMovies(query: string) {
    console.log("searchMovies() called with query:", query); // ✅ Debug
    if (!query.trim()) return;
    this.fetchMovies(query);
  }

  private setupListeners() {
    document.getElementById("prev")?.addEventListener("click", () => {
      if (this.currentIndex > 0) {
        this.currentIndex--;
        this.render();
        this.updateBackgroundAndDetails();
      }
    });

    document.getElementById("next")?.addEventListener("click", () => {
      if (this.currentIndex < this.movies.length - 1) {
        this.currentIndex++;
        this.render();
        this.updateBackgroundAndDetails();
      }
    });
  }

  private render() {
    this.track.innerHTML = "";
    this.movies.forEach((movie, index) => {
      const card = document.createElement("div");
      card.className = "movie-card" + (index === this.currentIndex ? " focused" : "");
      card.innerHTML = `
        <img src="https://image.tmdb.org/t/p/w300${movie.poster_path}" alt="${movie.title}" />
        <div class="overlay"><h4>${movie.title}</h4></div>
      `;
      this.track.appendChild(card);

      if (index === this.currentIndex) {
        setTimeout(() => {
          card.scrollIntoView({ behavior: "smooth", inline: "center", block: "nearest" });
        }, 100);
      }
    });
  }
  private updateBackgroundAndDetails() {
    const movie = this.movies[this.currentIndex];
    if (!movie) return;
  
    this.sliderContainer.style.backgroundImage = `url(https://image.tmdb.org/t/p/original${movie.backdrop_path})`;
  
    this.details.innerHTML = `
      <div class="movie-info">
        <h1>${movie.title}</h1>
        <div class="movie-meta">
          <span class="rating">IMDb 8.2</span>
          <span class="year">2021</span>
          <span class="duration">1 hour 55 minutes</span>
          <span class="genre">Sci-fi</span>
        </div>
        <p class="overview">${movie.overview || "No description available."}</p>
        <div class="movie-buttons">
          <button class="trailer-btn">Watch trailer</button>
          <button class="watch-btn">Watch now</button>
        </div>
      </div>
    `;
  }
  
}


document.addEventListener("DOMContentLoaded", () => {
  console.log("DOM fully loaded"); 

  const slider = new MovieSlider("slider-track", "details-center");

  const toggleSearch = document.getElementById("toggle-search");
  const dropdownSearch = document.getElementById("dropdown-search");
  const searchInput = document.getElementById("search-input") as HTMLInputElement;
  const searchButton = document.getElementById("search-btn");

  console.log("Search Button:", searchButton); 

  toggleSearch?.addEventListener("click", (e) => {
    e.preventDefault();
    dropdownSearch?.classList.toggle("hidden");
  });

  searchButton?.addEventListener("click", () => {
    const query = searchInput?.value.trim();
    console.log("Search button clicked. Query:", query); 
    if (query) {
      console.log("Calling searchMovies()..."); 
      slider.searchMovies(query);
    } else {
      console.log("Empty query – not searching.");
    }
  });

  searchInput?.addEventListener("keydown", (e) => {
    if (e.key === "Enter") {
      const query = searchInput.value.trim();
      console.log("Enter pressed. Query:", query); 
      if (query) {
        slider.searchMovies(query);
      }
    }
  });
});
