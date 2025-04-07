export class MovieCarousel {
    constructor(movies, carouselEl, infoEl, bgEl) {
        this.movies = movies;
        this.carouselEl = carouselEl;
        this.infoEl = infoEl;
        this.bgEl = bgEl;
        this.index = 0;
    }
    renderCards() {
        this.carouselEl.innerHTML = "";
        this.movies.forEach((movie, i) => {
            const card = document.createElement("div");
            card.className = "movie-card";
            if (i === this.index)
                card.classList.add("focused");
            card.innerHTML = `<img src="${movie.posterUrl}" alt="${movie.title}"/>`;
            this.carouselEl.appendChild(card);
        });
        this.updateFocus();
    }
    updateFocus() {
        const cards = this.carouselEl.querySelectorAll(".movie-card");
        cards.forEach(card => card.classList.remove("focused"));
        if (cards[this.index])
            cards[this.index].classList.add("focused");
        const movie = this.movies[this.index];
        this.infoEl.innerHTML = `
      <h1>${movie.title}</h1>
      <p><strong>Rating:</strong> ${movie.rating} | <strong>Year:</strong> ${movie.year}</p>
      <p>${movie.overview}</p>
    `;
        this.bgEl.style.backgroundImage = `url('${movie.backdropUrl}')`;
    }
    next() {
        if (this.index < this.movies.length - 1) {
            this.index++;
            this.renderCards();
        }
    }
    prev() {
        if (this.index > 0) {
            this.index--;
            this.renderCards();
        }
    }
}
