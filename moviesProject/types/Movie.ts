export class Movie {
    constructor(
      public title: string,
      public overview: string,
      public posterPath: string,
      public backdropPath: string,
      public rating: number,
      public releaseDate: string
    ) {}
  
    get posterUrl(): string {
      return `https://image.tmdb.org/t/p/w500${this.posterPath}`;
    }
  
    get backdropUrl(): string {
      return `https://image.tmdb.org/t/p/original${this.backdropPath}`;
    }
  
    get year(): string {
      return this.releaseDate?.split('-')[0] || 'N/A';
    }
  }
  