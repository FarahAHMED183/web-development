import React from 'react';
import { Box } from '@mui/material';
import TopBar from '../components/TopBar';
import TeamList from '../components/TeamList';

const Dashboard = () => {
  return (
    <Box sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
      <TopBar title="Team Dashboard" />
      <TeamList />
    </Box>
  );
};

export default Dashboard;