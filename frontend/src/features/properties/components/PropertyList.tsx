import { CircularProgress, Alert, Grid, Typography } from '@mui/material';
import { PropertyCard } from '../components/PropertyCard';
import type Property from '../types/Property';

type Props = {
  properties: Property[];
  loading: boolean;
  error: string | null;
  handleDelete: (id: string) => void | Promise<void>;
}

const PropertyList = ({ properties, loading, error, handleDelete }: Props) => {
  if (loading) return (
    <Grid container xs={12} sm={12} md={12} lg={12} justifyContent='center' alignItems='center' sx={{ marginTop: '5cm', marginLeft: '2cm' }} >
      <CircularProgress />

    </Grid>
  );
  if (error) return (
    <Grid container xs={12} sm={12} md={12} lg={12} justifyContent='center' alignItems='center' sx={{marginTop: '5cm'}} >
      <Alert severity="error">{error}</Alert>

    </Grid>
   
  );

  return (
    <>
      <Grid item container xs={12} sm={8} md={9} lg={9} spacing={1}>
        <Grid item xs={12} sm={12} md={12} lg={12}>
          <Alert icon={false} color="info">
            <Typography variant="subtitle1" sx={{ fontWeight: 'bold' }}>
              Total elementos encontrados: {properties.length}
            </Typography>
          </Alert>
        </Grid>
        {properties.map((property) => (
          <PropertyCard property={property} onDelete={() => handleDelete(property.id)} />
        ))}
      </Grid>
    </>
  );
};

export default PropertyList;
