// src/components/TopBar.tsx
import React from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Badge,
  Menu,
  MenuItem,
  Tooltip,
} from '@mui/material';
import {
  Mail as MailIcon,
  Notifications as NotificationsIcon,
  LanguageOutlined as LanguageIcon,
  KeyboardArrowDown as KeyboardArrowDownIcon,
} from '@mui/icons-material';

interface TopBarProps {
  title: string;
}

const TopBar: React.FC<TopBarProps> = ({ title }) => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <AppBar 
      position="static" 
      color="default" 
      sx={{ 
        boxShadow: 'none', 
        backgroundColor: 'white',
        borderBottom: '1px solid #e0e0e0',
      }}
    >
      <Toolbar sx={{ justifyContent: 'space-between' }}>
        <Box display="flex" alignItems="center">
          <Box 
            component="img" 
            src="https://placehold.co/24x24/2563eb/white.png?text=i"
            alt="Logo" 
            sx={{ mr: 1 }}
          />
          <Typography variant="h6" color="textPrimary" sx={{ fontWeight: 600 }}>
            {title}
          </Typography>
        </Box>

        <Box display="flex" alignItems="center">
          <Badge badgeContent={2} color="error" sx={{ mr: 1 }}>
            <IconButton color="inherit">
              <NotificationsIcon />
            </IconButton>
          </Badge>
          
          <IconButton color="inherit" sx={{ mr: 1 }}>
            <MailIcon />
          </IconButton>
          
          <Tooltip title="Select language">
            <IconButton
              onClick={handleClick}
              size="small"
              sx={{ ml: 1 }}
              aria-controls={open ? 'language-menu' : undefined}
              aria-haspopup="true"
              aria-expanded={open ? 'true' : undefined}
            >
              <LanguageIcon />
              <Box component="span" sx={{ display: 'flex', alignItems: 'center' }}>
                <Typography variant="body2" sx={{ ml: 0.5 }}>
                  EN
                </Typography>
                <KeyboardArrowDownIcon fontSize="small" />
              </Box>
            </IconButton>
          </Tooltip>
          
          <Menu
            id="language-menu"
            anchorEl={anchorEl}
            open={open}
            onClose={handleClose}
            MenuListProps={{
              'aria-labelledby': 'language-button',
            }}
          >
            <MenuItem onClick={handleClose}>English</MenuItem>
            <MenuItem onClick={handleClose}>Spanish</MenuItem>
            <MenuItem onClick={handleClose}>French</MenuItem>
            <MenuItem onClick={handleClose}>German</MenuItem>
          </Menu>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default TopBar;