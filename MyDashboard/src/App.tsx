// src/App.tsx
import React from 'react';
import { Box, CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import TopBar from './components/TopBar';
import TeamList from './components/TeamList';
import Sidebar from './components/Sidebar';

// Create a theme instance
const theme = createTheme({
  palette: {
    primary: {
      main: '#2563eb',
    },
    secondary: {
      main: '#f59e0b',
    },
    background: {
      default: '#f9fafb',
    },
  },
  typography: {
    fontFamily: [
      'Inter',
      '-apple-system',
      'BlinkMacSystemFont',
      '"Segoe UI"',
      'Roboto',
      '"Helvetica Neue"',
      'Arial',
      'sans-serif',
    ].join(','),
  },
  components: {
    MuiTableCell: {
      styleOverrides: {
        root: {
          padding: '12px 16px',
        },
        head: {
          fontWeight: 600,
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
        },
      },
    },
  },
});

const App: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Box sx={{ display: 'flex', minHeight: '100vh' }}>
        <Sidebar open={true} />
        <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
          <TopBar title="Team List" />
          <TeamList />
        </Box>
      </Box>
    </ThemeProvider>
  );
};

export default App;