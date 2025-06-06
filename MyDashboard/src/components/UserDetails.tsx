import {
  Box,
  Typography,
  Collapse,
  Grid,
  Paper,
  Skeleton
} from '@mui/material';
import {
  LocationOn as LocationIcon,
  Person as PersonIcon,
  Cake as CakeIcon,
  AccessTime as AccessTimeIcon,
  Home as HomeIcon
} from '@mui/icons-material';

const UserDetails = ({ user, expanded, isLoading = false }) => {
  // Skeleton for user details when loading
  const UserDetailsSkeleton = () => (
    <Grid container spacing={3}>
      {[...Array(5)].map((_, index) => (
        <Grid item xs={12} sm={6} lg={index === 4 ? 2 : index === 3 ? 2 : index === 2 ? 2 : index === 1 ? 3 : 3} key={index}>
          <Box display="flex" alignItems="flex-start">
            <Skeleton variant="circular" width={24} height={24} sx={{ mr: 1 }} />
            <Box sx={{ width: '100%' }}>
              <Skeleton variant="text" width={80} />
              <Skeleton variant="text" width="100%" />
              {index === 1 && (
                <>
                  <Skeleton variant="text" width="80%" sx={{ mt: 0.5 }} />
                  <Skeleton variant="text" width="80%" sx={{ mt: 0.5 }} />
                </>
              )}
            </Box>
          </Box>
        </Grid>
      ))}
    </Grid>
  );

  return (
    <Collapse in={expanded} timeout="auto" unmountOnExit>
      <Paper sx={{ 
        p: 2, 
        mx: 2, 
        mb: 2, 
        backgroundColor: '#f9fafb', 
        border: '1px solid #e5e7eb',
        borderRadius: 1,
      }}>
        {isLoading ? (
          <UserDetailsSkeleton />
        ) : (
          <Grid container spacing={3}>
            <Grid item xs={12} sm={6} lg={3}>
              <Box display="flex" alignItems="center">
                <LocationIcon sx={{ color: '#6b7280', mr: 1 }} />
                <Box>
                  <Typography variant="subtitle2" color="textSecondary">Office Location</Typography>
                  <Typography variant="body2">
                    {user.details.officeLocation}
                  </Typography>
                </Box>
              </Box>
            </Grid>
            
            <Grid item xs={12} sm={6} lg={3}>
              <Box display="flex" alignItems="flex-start">
                <PersonIcon sx={{ color: '#6b7280', mr: 1, mt: 0.5 }} />
                <Box>
                  <Typography variant="subtitle2" color="textSecondary">Team Mates</Typography>
                  {user.details.teamMates.map((teammate, index) => (
                    <Typography key={index} variant="body2" sx={{ display: 'flex', alignItems: 'center', mb: 0.5 }}>
                      <PersonIcon fontSize="small" sx={{ color: '#9ca3af', mr: 0.5 }} />
                      {teammate}
                    </Typography>
                  ))}
                </Box>
              </Box>
            </Grid>
            
            <Grid item xs={12} sm={6} lg={2}>
              <Box display="flex" alignItems="center">
                <CakeIcon sx={{ color: '#6b7280', mr: 1 }} />
                <Box>
                  <Typography variant="subtitle2" color="textSecondary">Birthday</Typography>
                  <Typography variant="body2">
                    {user.details.birthday}
                  </Typography>
                </Box>
              </Box>
            </Grid>
            
            <Grid item xs={12} sm={6} lg={2}>
              <Box display="flex" alignItems="center">
                <AccessTimeIcon sx={{ color: '#6b7280', mr: 1 }} />
                <Box>
                  <Typography variant="subtitle2" color="textSecondary">HR Year</Typography>
                  <Typography variant="body2">
                    {user.details.hrYear}
                  </Typography>
                </Box>
              </Box>
            </Grid>
            
            <Grid item xs={12} lg={2}>
              <Box display="flex" alignItems="center">
                <HomeIcon sx={{ color: '#6b7280', mr: 1 }} />
                <Box>
                  <Typography variant="subtitle2" color="textSecondary">Address</Typography>
                  <Typography variant="body2">
                    {user.details.address}
                  </Typography>
                </Box>
              </Box>
            </Grid>
          </Grid>
        )}
      </Paper>
    </Collapse>
  );
};

export default UserDetails;