export class Movie {
    constructor(title, overview, posterPath, backdropPath, rating, releaseDate) {
        this.title = title;
        this.overview = overview;
        this.posterPath = posterPath;
        this.backdropPath = backdropPath;
        this.rating = rating;
        this.releaseDate = releaseDate;
    }
    get posterUrl() {
        return `https://image.tmdb.org/t/p/w500${this.posterPath}`;
    }
    get backdropUrl() {
        return `https://image.tmdb.org/t/p/original${this.backdropPath}`;
    }
    get year() {
        var _a;
        return ((_a = this.releaseDate) === null || _a === void 0 ? void 0 : _a.split('-')[0]) || 'N/A';
    }
}
