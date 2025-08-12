import { useNavigate } from 'react-router-dom';
import { Box, Button } from '@mui/material';
import OwnerList from '../components/OwnerList';
import AddIcon from '@mui/icons-material/Add';

const Owners = () => {
  const navigate = useNavigate();

  return (
    <>
      <Box display="flex" justifyContent="flex-end" mb={2}>
        <Button startIcon={<AddIcon />} variant="contained" color="primary"
          onClick={() => navigate(`/owner/create`)} >Agregar propietario</Button>
      </Box>
      <OwnerList />
    </>
  );
}

export default Owners;