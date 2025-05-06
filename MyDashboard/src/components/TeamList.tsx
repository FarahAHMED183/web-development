import React, { useState } from 'react';
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
  Tooltip,
  Skeleton
} from '@mui/material';
import {
  Search as SearchIcon,
  Edit as EditIcon,
  Delete as DeleteIcon,
  KeyboardArrowDown as KeyboardArrowDownIcon,
  Add as AddIcon,
  ViewList as ViewListIcon
} from '@mui/icons-material';
import { useQuery } from 'react-query';
import { getUsers } from '../api/userApi';
import UserDetails from './UserDetails';

const ROWS_PER_PAGE = 10;

const TeamList = () => {
  const [page, setPage] = useState(1);
  const [searchQuery, setSearchQuery] = useState('');
  const [expandedUser, setExpandedUser] = useState(null);
  const [selectedUsers, setSelectedUsers] = useState(new Set());
  
  // Using React Query for data fetching
  const { 
    data, 
    isLoading, 
    isError, 
    error, 
    refetch 
  } = useQuery(
    ['users', page, ROWS_PER_PAGE, searchQuery],
    () => getUsers(page, ROWS_PER_PAGE, searchQuery),
    {
      keepPreviousData: true,
      staleTime: 5 * 60 * 1000, 
    }
  );
  
  const users = data?.users || [];
  const totalPages = data?.totalPages || 0;
  
  const handleSearch = () => {
    setPage(1); // Reset to first page when searching
    refetch();
  };
  
  const handleChangePage = (event, value) => {
    setPage(value);
  };
  
  const handleExpandUser = (userId) => {
    setExpandedUser(expandedUser === userId ? null : userId);
  };
  
  const handleToggleSelectAll = (event) => {
    if (event.target.checked) {
      const newSelected = new Set(selectedUsers);
      users.forEach(user => newSelected.add(user.login.uuid));
      setSelectedUsers(newSelected);
    } else {
      setSelectedUsers(new Set());
    }
  };
  
  const handleToggleSelect = (userId) => {
    const newSelected = new Set(selectedUsers);
    if (selectedUsers.has(userId)) {
      newSelected.delete(userId);
    } else {
      newSelected.add(userId);
    }
    setSelectedUsers(newSelected);
  };
  
  const handleSearchChange = (e) => {
    setSearchQuery(e.target.value);
  };
  
  const handleSearchKeyDown = (e) => {
    if (e.key === 'Enter') {
      handleSearch();
    }
  };
  
  const getFullName = (user) => {
    return `${user.name.first} ${user.name.last}`;
  };

  // Skeleton loader for a row
  const UserRowSkeleton = () => (
    <TableRow>
      <TableCell padding="checkbox">
        <Skeleton variant="rectangular" width={24} height={24} />
      </TableCell>
      <TableCell>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Skeleton variant="circular" width={40} height={40} sx={{ mr: 1 }} />
          <Skeleton variant="text" width={120} />
        </Box>
      </TableCell>
      <TableCell><Skeleton variant="text" width={100} /></TableCell>
      <TableCell><Skeleton variant="text" width={100} /></TableCell>
      <TableCell><Skeleton variant="text" width={180} /></TableCell>
      <TableCell><Skeleton variant="text" width={120} /></TableCell>
      <TableCell>
        <Skeleton variant="rectangular" width={80} height={24} sx={{ borderRadius: 1 }} />
      </TableCell>
      <TableCell>
        <Box sx={{ display: 'flex' }}>
          <Skeleton variant="circular" width={30} height={30} sx={{ mr: 1 }} />
          <Skeleton variant="circular" width={30} height={30} />
        </Box>
      </TableCell>
    </TableRow>
  );

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
              endAdornment: isLoading && (
                <InputAdornment position="end">
                  <CircularProgress size={20} />
                </InputAdornment>
              ),
            }}
            sx={{ mr: 2, backgroundColor: 'white' }}
          />
          <Button 
            variant="outlined" 
            size="small" 
            onClick={handleSearch} 
            sx={{ mr: 2 }}
          >
            Search
          </Button>
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
              {isLoading ? (
                
                Array(5).fill(0).map((_, index) => <UserRowSkeleton key={index} />)
              ) : isError ? (
                <TableRow>
                  <TableCell colSpan={8} align="center" sx={{ py: 3 }}>
                    <Typography color="error">Error loading data: {error?.message || 'Unknown error'}</Typography>
                    <Button 
                      variant="outlined" 
                      color="primary" 
                      sx={{ mt: 1 }} 
                      onClick={() => refetch()}
                    >
                      Retry
                    </Button>
                  </TableCell>
                </TableRow>
              ) : users.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={8} align="center" sx={{ py: 3 }}>
                    <Typography>No users found</Typography>
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
            {isLoading ? (
              <Skeleton variant="text" width={80} />
            ) : (
              <Typography variant="body2" color="text.secondary" sx={{ mr: 2 }}>
                {`${page} - ${Math.min(page, totalPages)} of ${totalPages}`}
              </Typography>
            )}
            {isLoading ? (
              <Skeleton variant="rectangular" width={200} height={32} sx={{ borderRadius: 1 }} />
            ) : (
              <Pagination
                count={totalPages}
                page={page}
                onChange={handleChangePage}
                shape="rounded"
                size="small"
                disabled={isLoading}
              />
            )}
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default TeamList;