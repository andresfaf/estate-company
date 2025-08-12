import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';

export default function ButtonAppBar() {
  return (
    <AppBar position="fixed">
      <Toolbar>
        <Typography variant="h6" sx={{ flexGrow: 0 }}>
          Real estate
        </Typography>
        <Button color="inherit" component={RouterLink} to="/">
          Propiedades
        </Button>
        <Button color="inherit" component={RouterLink} to="/owners">
          Propietarios
        </Button>
      </Toolbar>
    </AppBar>
  );
}
