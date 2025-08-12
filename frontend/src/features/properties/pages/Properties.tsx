import { Alert, Grid, Snackbar } from '@mui/material';
import PropertyList from '../components/PropertyList';
import Filters from '../components/Filters';
import { useProperties } from '../hooks/useProperty';
import { deleteProperty } from '../services/PropertyService';
import { useState } from 'react';

const Properties: React.FC = () => {
  const { properties, loading, error, setProperties, fetchData } = useProperties();
  const [cleanFilters, setCleanFilters] = useState<boolean>(false);

  const [open, setOpen] = useState<boolean>(false);

  const handleDelete = async (id: string) => {
    if (!window.confirm("¿Seguro que quieres eliminar esta propiedad?")) return;
    await deleteProperty(id);
    fetchData();
    setCleanFilters(true);
    setOpen(true);
  };

  return (
    <>
      <Snackbar anchorOrigin={{ vertical: 'top', horizontal: 'right' }} open={open} onClose={() => setOpen(false)} autoHideDuration={4000}>
        <Alert severity="success" >Propiedad eliminada correctamente</Alert>
      </Snackbar>
      <Grid container spacing={2}>
        <Filters setProperties={setProperties} cleanFilters={cleanFilters} />
        <PropertyList properties={properties} loading={loading} error={error} handleDelete={handleDelete} />
      </Grid>
    </>

  );
}

export default Properties;