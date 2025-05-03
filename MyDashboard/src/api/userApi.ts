// src/api/userApi.ts
import axios from 'axios';
import { ApiResponse, User, UserWithDetails } from '../types/types';

// Create axios instance
const userApi = axios.create({
  baseURL: 'https://randomuser.me/api',
});

// Department options for generating random department assignment
const departments = [
  { name: 'Sales Team', position: 'Graphics Designer' },
  { name: 'Sales', position: 'Graphics Designer' },
  { name: 'Finances', position: 'Joomla Developer' },
  { name: 'Management', position: 'Human Resource' },
  { name: 'Engineering', position: 'PHP Developer' },
  { name: 'Human Resources', position: 'UI UX Designer' },
  { name: 'Customer Success', position: 'UX Architect' },
  { name: 'Marketing', position: 'Python Developer' },
  { name: 'Product', position: 'Freshers' },
];

// Status options
const statuses = ['Full Time', 'Part Time'] as const;

// Example team mates
const teamMatesPool = [
  'Ronald Richards',
  'Floyd Miles',
  'Savannah Nguyen',
  'Ralph Edwards',
  'Edward John',
  'Esther Howard',
];

// Function to get random items from an array
const getRandomItems = <T>(arr: T[], count: number): T[] => {
  const shuffled = [...arr].sort(() => 0.5 - Math.random());
  return shuffled.slice(0, count);
};

// Convert API user to app user format
const enrichUserData = (user: User): UserWithDetails => {
  const randomDept = departments[Math.floor(Math.random() * departments.length)];
  const isFullTime = Math.random() > 0.3;
  
  return {
    ...user,
    position: randomDept.position,
    department: randomDept.name,
    status: isFullTime ? 'Full Time' : 'Part Time',
    details: {
      officeLocation: `${Math.floor(Math.random() * 5000)} ${user.location.street.name}, ${user.location.city}, ${user.location.state} ${user.location.postcode}`,
      teamMates: getRandomItems(teamMatesPool, Math.floor(Math.random() * 3) + 1),
      birthday: new Date(user.dob.date).toLocaleDateString('en-US', { month: '2-digit', day: '2-digit', year: 'numeric' }),
      hrYear: `${Math.floor(Math.random() * 10) + 1} Years`,
      address: `${Math.floor(Math.random() * 9000)} ${user.location.street.name}, ${user.location.city}, ${user.location.state} ${user.location.postcode}`
    }
  };
};

// Get users with pagination
export const getUsers = async (page: number, pageSize: number, searchQuery = ''): Promise<{ users: UserWithDetails[], totalPages: number }> => {
  try {
    const response = await userApi.get<ApiResponse>('', {
      params: {
        page,
        results: pageSize,
        seed: 'teamlist'
      }
    });
    
    let users = response.data.results.map(enrichUserData);
    
    // Apply search filtering if search query exists
    if (searchQuery) {
      const query = searchQuery.toLowerCase();
      users = users.filter(user => 
        user.name.first.toLowerCase().includes(query) || 
        user.name.last.toLowerCase().includes(query) ||
        user.email.toLowerCase().includes(query) ||
        user.department.toLowerCase().includes(query) ||
        user.position.toLowerCase().includes(query)
      );
    }
    
    return {
      users,
      totalPages: 10 // Hard-coded for demo purposes, API doesn't provide total pages
    };
  } catch (error) {
    console.error('Error fetching users:', error);
    throw error;
  }
};

export default userApi;