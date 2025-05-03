// src/components/TeamList.tsx
import React, { useState, useEffect } from 'react';
import {
  Box,
  Button,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Checkbox,
  Paper,
  TextField,
  InputAdornment,
  Chip,
  IconButton,
  Breadcrumbs,
  Link,
  Pagination,
  CircularProgress,
  Avatar,
  Tooltip
} from '@mui/material';
import {
  Search as SearchIcon,
  Edit as EditIcon,
  Delete as DeleteIcon,
  KeyboardArrowDown as KeyboardArrowDownIcon,
  Add as AddIcon,
  ViewList as ViewListIcon
} from '@mui/icons-material';
import { UserWithDetails } from '../types/types';
import { getUsers } from '../api/userApi';
import UserDetails from './UserDetails';

const ROWS_PER_PAGE = 10;

const TeamList: React.FC = () => {
  const [users, setUsers] = useState<UserWithDetails[]>([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  const [searchQuery, setSearchQuery] = useState('');
  const [expandedUser, setExpandedUser] = useState<string | null>(null);
  const [selectedUsers, setSelectedUsers] = useState<Set<string>>(new Set());
  
  useEffect(() => {
    fetchUsers();
  }, [page]);
  
  const fetchUsers = async () => {
    setLoading(true);
    try {
      const data = await getUsers(page, ROWS_PER_PAGE, searchQuery);
      setUsers(data.users);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error('Error fetching users:', error);
    } finally {
      setLoading(false);
    }
  };
  
  const handleSearch = () => {
    setPage(1); // Reset to first page when searching
    fetchUsers();
  };
  
  const handleChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };
  
  const handleExpandUser = (userId: string) => {
    setExpandedUser(expandedUser === userId ? null : userId);
  };
  
  const handleToggleSelectAll = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      const newSelected = new Set(selectedUsers);
      users.forEach(user => newSelected.add(user.login.uuid));
      setSelectedUsers(newSelected);
    } else {
      setSelectedUsers(new Set());
    }
  };
  
  const handleToggleSelect = (userId: string) => {
    const newSelected = new Set(selectedUsers);
    if (selectedUsers.has(userId)) {
      newSelected.delete(userId);
    } else {
      newSelected.add(userId);
    }
    setSelectedUsers(newSelected);
  };
  
  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(e.target.value);
  };
  
  const handleSearchKeyDown = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      handleSearch();
    }
  };
  
  const getFullName = (user: UserWithDetails) => {
    return `${user.name.first} ${user.name.last}`;
  };

  return (
    <Box sx={{ p: 3, backgroundColor: '#f9fafb', minHeight: 'calc(100vh - 64px)', width: '100%' }}>
      <Breadcrumbs aria-label="breadcrumb" sx={{ mb: 2 }}>
        <Link
          underline="hover"
          color="inherit"
          href="#"
        >
          Admin Dashboard
        </Link>
        <Typography color="text.primary">Team List</Typography>
      </Breadcrumbs>
      
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <TextField
            placeholder="Search Task"
            size="small"
            value={searchQuery}
            onChange={handleSearchChange}
            onKeyDown={handleSearchKeyDown}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon sx={{ color: '#9ca3af' }} />
                </InputAdornment>
              ),
            }}
            sx={{ mr: 2, backgroundColor: 'white' }}
          />
          <ViewListIcon sx={{ color: '#9ca3af', ml: 1 }} />
          <Typography sx={{ ml: 2, color: '#6b7280' }}>
            {selectedUsers.size > 0 ? `${selectedUsers.size} Selected` : ''}
          </Typography>
        </Box>
        
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          sx={{ 
            backgroundColor: '#2563eb',
            '&:hover': {
              backgroundColor: '#1d4ed8',
            },
            textTransform: 'none',
            fontWeight: 'bold'
          }}
        >
          ADD USER
        </Button>
      </Box>
      
      <Paper sx={{ overflow: 'hidden' }}>
        <TableContainer>
          <Table sx={{ minWidth: 650 }}>
            <TableHead sx={{ backgroundColor: '#f9fafb' }}>
              <TableRow>
                <TableCell padding="checkbox">
                  <Checkbox
                    color="primary"
                    indeterminate={selectedUsers.size > 0 && selectedUsers.size < users.length}
                    checked={users.length > 0 && selectedUsers.size === users.length}
                    onChange={handleToggleSelectAll}
                  />
                </TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Name</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Position</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Department</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Email</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Phone</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Status</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Edit</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {loading ? (
                <TableRow>
                  <TableCell colSpan={8} align="center" sx={{ py: 3 }}>
                    <CircularProgress />
                  </TableCell>
                </TableRow>
              ) : users.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={8} align="center" sx={{ py: 3 }}>
                    No users found
                  </TableCell>
                </TableRow>
              ) : (
                users.map((user) => {
                  const isSelected = selectedUsers.has(user.login.uuid);
                  const isExpanded = expandedUser === user.login.uuid;
                  return (
                    <React.Fragment key={user.login.uuid}>
                      <TableRow 
                        hover
                        role="checkbox"
                        tabIndex={-1}
                        selected={isSelected}
                        sx={{
                          ...(isExpanded && {
                            '& > td': {
                              borderBottom: 0,
                            },
                          }),
                        }}
                      >
                        <TableCell padding="checkbox">
                          <Checkbox
                            color="primary"
                            checked={isSelected}
                            onClick={() => handleToggleSelect(user.login.uuid)}
                          />
                        </TableCell>
                        <TableCell>
                          <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <IconButton
                              aria-label="expand row"
                              size="small"
                              onClick={() => handleExpandUser(user.login.uuid)}
                              sx={{ mr: 1 }}
                            >
                              <KeyboardArrowDownIcon sx={{
                                transform: isExpanded ? 'rotate(180deg)' : 'rotate(0deg)',
                                transition: '0.2s',
                              }} />
                            </IconButton>
                            <Avatar src={user.picture.medium} alt={getFullName(user)} sx={{ mr: 2 }} />
                            {getFullName(user)}
                          </Box>
                        </TableCell>
                        <TableCell>{user.position}</TableCell>
                        <TableCell>{user.department}</TableCell>
                        <TableCell>{user.email}</TableCell>
                        <TableCell>{user.phone}</TableCell>
                        <TableCell>
                          <Chip
                            label={user.status}
                            size="small"
                            sx={{
                              backgroundColor: user.status === 'Full Time' ? '#dcfce7' : '#fef9c3',
                              color: user.status === 'Full Time' ? '#166534' : '#854d0e',
                              fontWeight: 500,
                            }}
                          />
                        </TableCell>
                        <TableCell>
                          <Box sx={{ display: 'flex' }}>
                            <Tooltip title="Edit user">
                              <IconButton size="small">
                                <EditIcon fontSize="small" sx={{ color: '#9ca3af' }} />
                              </IconButton>
                            </Tooltip>
                            <Tooltip title="Delete user">
                              <IconButton size="small">
                                <DeleteIcon fontSize="small" sx={{ color: '#9ca3af' }} />
                              </IconButton>
                            </Tooltip>
                          </Box>
                        </TableCell>
                      </TableRow>
                      <TableRow>
                        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={8}>
                          <UserDetails user={user} expanded={isExpanded} />
                        </TableCell>
                      </TableRow>
                    </React.Fragment>
                  );
                })
              )}
            </TableBody>
          </Table>
        </TableContainer>
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'flex-end',
            p: 2,
            borderTop: '1px solid #e5e7eb',
          }}
        >
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <Typography variant="body2" color="text.secondary" sx={{ mr: 2 }}>
              {`${page} - ${Math.min(page, totalPages)} of ${totalPages}`}
            </Typography>
            <Pagination
              count={totalPages}
              page={page}
              onChange={handleChangePage}
              shape="rounded"
              size="small"
            />
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default TeamList;