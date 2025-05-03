
import React from 'react';
import {
  Box,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Drawer,
  Tooltip,
} from '@mui/material';
import {
  Apps as AppsIcon,
  People as PeopleIcon,
  Description as DescriptionIcon,
  Folder as FolderIcon,
  Email as EmailIcon,
  CalendarMonth as CalendarIcon,
  Settings as SettingsIcon,
  AccountCircle as AccountCircleIcon
} from '@mui/icons-material';

interface SidebarProps {
  open: boolean;
  onClose?: () => void;
}

const DRAWER_WIDTH = 72;

const sidebarItems = [
  { icon: <AppsIcon />, label: 'Dashboard', active: false },
  { icon: <PeopleIcon />, label: 'Team', active: true },
  { icon: <DescriptionIcon />, label: 'Documents', active: false },
  { icon: <FolderIcon />, label: 'Files', active: false },
  { icon: <EmailIcon />, label: 'Mail', active: false },
  { icon: <CalendarIcon />, label: 'Calendar', active: false },
];

const Sidebar: React.FC<SidebarProps> = ({ open, onClose }) => {
  const drawerContent = (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        height: '100%',
        justifyContent: 'space-between',
        backgroundColor: 'white',
        py: 2,
      }}
    >
      <List>
        {sidebarItems.map((item, index) => (
          <ListItem key={index} disablePadding sx={{ display: 'block', mb: 1 }}>
            <Tooltip title={item.label} placement="right">
              <ListItemButton
                sx={{
                  minHeight: 48,
                  justifyContent: 'center',
                  px: 2.5,
                  borderLeft: item.active ? '3px solid #2563eb' : '3px solid transparent',
                  backgroundColor: item.active ? '#eff6ff' : 'transparent',
                }}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    mr: 'auto',
                    ml: 'auto',
                    justifyContent: 'center',
                    color: item.active ? '#2563eb' : '#64748b',
                  }}
                >
                  {item.icon}
                </ListItemIcon>
                <ListItemText primary={item.label} sx={{ display: 'none' }} />
              </ListItemButton>
            </Tooltip>
          </ListItem>
        ))}
      </List>
      
      <List>
        <ListItem disablePadding sx={{ display: 'block' }}>
          <Tooltip title="Settings" placement="right">
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: 'center',
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: 'auto',
                  ml: 'auto',
                  justifyContent: 'center',
                  color: '#64748b',
                }}
              >
                <SettingsIcon />
              </ListItemIcon>
            </ListItemButton>
          </Tooltip>
        </ListItem>
        <ListItem disablePadding sx={{ display: 'block' }}>
          <Tooltip title="Profile" placement="right">
            <ListItemButton
              sx={{
                minHeight: 48,
                justifyContent: 'center',
                px: 2.5,
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  mr: 'auto',
                  ml: 'auto',
                  justifyContent: 'center',
                  color: '#64748b',
                }}
              >
                <AccountCircleIcon />
              </ListItemIcon>
            </ListItemButton>
          </Tooltip>
        </ListItem>
      </List>
    </Box>
  );

  return (
    <Drawer
      variant="permanent"
      sx={{
        width: DRAWER_WIDTH,
        flexShrink: 0,
        '& .MuiDrawer-paper': {
          width: DRAWER_WIDTH,
          boxSizing: 'border-box',
          borderRight: '1px solid #e5e7eb',
          boxShadow: 'none',
        },
      }}
      open={open}
      onClose={onClose}
    >
      {drawerContent}
    </Drawer>
  );
};

export default Sidebar;