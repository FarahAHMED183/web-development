
export interface User {
    id: {
      value: string;
    };
    name: {
      first: string;
      last: string;
    };
    email: string;
    phone: string;
    picture: {
      medium: string;
    };
    location: {
      street: {
        number: number;
        name: string;
      };
      city: string;
      state: string;
      country: string;
      postcode: string;
    };
    dob: {
      date: string;
    };
    registered: {
      date: string;
    };
    login: {
      uuid: string;
    };
  }
  
  export interface ApiResponse {
    results: User[];
    info: {
      page: number;
      results: number;
      seed: string;
      version: string;
    };
  }
  
  export interface UserDetails {
    officeLocation: string;
    teamMates: string[];
    birthday: string;
    hrYear: string;
    address: string;
  }
  
  export interface UserWithDetails extends User {
    position: string;
    department: string;
    status: 'Full Time' | 'Part Time';
    details: UserDetails;
  }