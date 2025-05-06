export const User = {
  id: {
    value: String,
  },
  name: {
    first: String,
    last: String,
  },
  email: String,
  phone: String,
  picture: {
    medium: String,
  },
  location: {
    street: {
      number: Number,
      name: String,
    },
    city: String,
    state: String,
    country: String,
    postcode: String,
  },
  dob: {
    date: String,
  },
  registered: {
    date: String,
  },
  login: {
    uuid: String,
  },
};

export const ApiResponse = {
  results: [User],
  info: {
    page: Number,
    results: Number,
    seed: String,
    version: String,
  },
};

export const UserDetails = {
  officeLocation: String,
  teamMates: [String],
  birthday: String,
  hrYear: String,
  address: String,
};

export const UserWithDetails = {
  ...User,
  position: String,
  department: String,
  status: ['Full Time', 'Part Time'],
  details: UserDetails,
};