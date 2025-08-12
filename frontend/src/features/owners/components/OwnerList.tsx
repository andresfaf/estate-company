import { useOwners } from '../hooks/useOwner';
import { Alert, CircularProgress, Grid } from '@mui/material';
import OwnerCard from './OwnerCard';
import { deleteOwner } from '../services/OwnerService';

export default function OwnerList() {
    const { owners, loading, error, fetchData } = useOwners();

    const handleDelete = async (id: string) => {
        if (!window.confirm("¿Seguro que quieres eliminar este propietario?")) return;
        await deleteOwner(id);
        fetchData();
    };

    if (loading) return <CircularProgress />;
    if (error) return <Alert severity="error">{error}</Alert>;
    
    return (
        <Grid container spacing={3}>
            {owners.map((owner) => (
                <OwnerCard owner={owner} onDelete={() => handleDelete(owner.id)} />
            ))}
        </Grid>
    );
}
