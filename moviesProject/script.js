var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g = Object.create((typeof Iterator === "function" ? Iterator : Object).prototype);
    return g.next = verb(0), g["throw"] = verb(1), g["return"] = verb(2), typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var _a, _b;
var Movie = /** @class */ (function () {
    function Movie(data) {
        this.title = data.title;
        this.vote_average = data.vote_average;
        this.release_date = data.release_date;
        this.original_language = data.original_language;
        this.backdrop_path = data.backdrop_path;
        this.overview = data.overview;
        if (this.backdrop_path) {
            this.posterUrl = "https://image.tmdb.org/t/p/original".concat(this.backdrop_path);
        }
    }
    Object.defineProperty(Movie.prototype, "releaseYear", {
        get: function () {
            return this.release_date.slice(0, 4);
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(Movie.prototype, "languageUpperCase", {
        get: function () {
            return this.original_language.toUpperCase();
        },
        enumerable: false,
        configurable: true
    });
    Movie.prototype.getShortOverview = function (maxLength) {
        if (maxLength === void 0) { maxLength = 100; }
        return this.overview.length > maxLength ? "".concat(this.overview.slice(0, maxLength), "...") : this.overview;
    };
    return Movie;
}());
var API_URL = "https://api.themoviedb.org/3/search/movie?api_key=21d6601622ce880a80939f3c1823ce8e&query=spiderman";
var movies = [];
var currentIndex = 0;
function fetchMovies() {
    return __awaiter(this, void 0, void 0, function () {
        var response, data, error_1;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    _a.trys.push([0, 3, , 4]);
                    return [4 /*yield*/, fetch(API_URL)];
                case 1:
                    response = _a.sent();
                    return [4 /*yield*/, response.json()];
                case 2:
                    data = _a.sent();
                    // Map the raw results to Movie class instances
                    movies = data.results.map(function (movieData) { return new Movie(movieData); });
                    if (movies.length > 0) {
                        displayMovie(currentIndex);
                    }
                    return [3 /*break*/, 4];
                case 3:
                    error_1 = _a.sent();
                    console.error("Error fetching movie data:", error_1);
                    return [3 /*break*/, 4];
                case 4: return [2 /*return*/];
            }
        });
    });
}
function displayMovie(index) {
    var movie = movies[index];
    var banner = document.getElementById("banner");
    var title = document.getElementById("movie-title");
    var info = document.getElementById("movie-info");
    var desc = document.getElementById("movie-desc");
    if (movie.posterUrl) {
        banner.style.backgroundImage = "url(".concat(movie.posterUrl, ")");
    }
    else {
        banner.style.backgroundImage = ''; // Clear background if no backdrop
    }
    title.textContent = movie.title;
    info.innerHTML = "\u2B50\uFE0F ".concat(movie.vote_average, " &nbsp; \u2022 &nbsp; ").concat(movie.releaseYear, " &nbsp; \u2022 &nbsp; ").concat(movie.languageUpperCase);
    desc.textContent = movie.overview;
}
(_a = document.getElementById("nextBtn")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", function () {
    if (movies.length > 0) {
        currentIndex = (currentIndex + 1) % movies.length;
        displayMovie(currentIndex);
    }
});
(_b = document.getElementById("prevBtn")) === null || _b === void 0 ? void 0 : _b.addEventListener("click", function () {
    if (movies.length > 0) {
        currentIndex = (currentIndex - 1 + movies.length) % movies.length;
        displayMovie(currentIndex);
    }
});
fetchMovies();
