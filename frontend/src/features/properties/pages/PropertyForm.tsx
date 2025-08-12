import { Alert, Autocomplete, Button, Container, Divider, Grid, Snackbar, TextField, Typography } from "@mui/material";
import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import type Property from "../types/Property";
import SendIcon from '@mui/icons-material/Send';
import type Owner from "../../owners/types/Owner";
import { getAllOwners } from "../../owners/services/OwnerService";
import { ImagesUploader } from "../components/ImagesUploader";
import base64ToFile from "../../../utils/Base64ToFile";
import type { PropertyImageFile } from "../types/PropertyImage";

const defaultProperty: Property = {
    id: '',
    name: '',
    address: '',
    price: null,
    year: null,
    idOwner: '',
    images: [],
    imageEnabled: ''
};

const PropertyForm: React.FC = () => {
    const navigate = useNavigate();

    const [property, setProperty] = useState<Property>(defaultProperty);
    const [owners, setOwners] = useState<Owner[]>([]);
    const [selectedOwner, setSelectedOwner] = useState<Owner | null>(null);

    const [selectedFiles, setSelectedFiles] = useState<PropertyImageFile[] | null>(null);
    const [open, setOpen] = useState<boolean>(false);
    const [message, setMessage] = useState<string>('');

    useEffect(() => {
        const fetchOwners = async () => {
            const owners = await getAllOwners();
            setOwners(owners);
        };
        fetchOwners();
    }, [])

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setProperty(prev => ({
            ...prev,
            [name]: value,
        }));
    };
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (selectedOwner === null) {
            setMessage('¡Seleccionar un propietario, de estar la lista vacia se debe crear uno!')
            setOpen(true);
            return;
        }
        if (selectedFiles === null || selectedFiles.length === 0) {
            setMessage('¡Se debe seleccionar por lo menos una imagen!')
            setOpen(true);
            return;
        }
        const imageEnabled = selectedFiles.filter(i => i.enabled === true);
        if (imageEnabled.length === 0 || imageEnabled === null) {
            setMessage('¡Se debe seleccionar una imagen como activa!')
            setOpen(true);
            return;
        }
        const formData = new FormData();

        formData.append("name", property.name);
        formData.append("address", property.address);
        formData.append("price", property.price ? property.price.toString() : '');
        formData.append("year", property.year ? property.year.toString() : '');
        formData.append("idOwner", selectedOwner ? selectedOwner.id : "");

        if (selectedFiles) {
            selectedFiles.forEach((selectedFile) => {
                formData.append("imageFiles", selectedFile.file);
                formData.append("imageFilesEnabled", selectedFile.enabled.toString());
            });
        } else {
            if (property.images.length !== 0) {
                property.images.forEach((file) => {
                    formData.append("imageFiles", base64ToFile(file.image, property.name));
                });
            }
        }

        const url = `${import.meta.env.VITE_API_URL}/Property`;
        const method = 'POST';

        await fetch(url, {
            method,
            body: formData,
        });
        navigate('/');
    };

    return (
        <>
            <Snackbar anchorOrigin={{ vertical: 'top', horizontal: 'right' }} open={open} onClose={() => setOpen(false)} autoHideDuration={4000}>
                <Alert severity="info">{message}</Alert>
            </Snackbar>
            <Container maxWidth="lg">
                <Typography variant="h5" gutterBottom align='center' sx={{ marginTop: '15px' }}>
                    Crear Propiedad
                </Typography>
                <Divider sx={{ margin: '20px 0px' }} />

                <Grid container spacing={2}>

                    <Grid item container xs={12} sm={6} md={6} lg={6}>
                        <ImagesUploader
                            setSelectedFiles={(files) => setSelectedFiles(files)}
                            selectedFiles={selectedFiles ?? []}
                            initialPreview={property.images}
                        />
                    </Grid>

                    <Grid item container xs={12} sm={6} md={6} lg={6}>
                        <form onSubmit={handleSubmit}>
                            <Grid container spacing={2} sx={{ marginTop: '10px' }}>
                                <Grid item xs={12} md={12}>
                                    <Autocomplete
                                        onChange={(event, newValue) => setSelectedOwner(newValue)}
                                        getOptionLabel={(option) => option.name}
                                        disablePortal
                                        options={owners}
                                        renderInput={(params) => <TextField {...params} label="Propietarios" />}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={12} md={12} lg={12}>
                                    <TextField
                                        label="Nombre"
                                        fullWidth
                                        name="name"
                                        value={property.name}
                                        onChange={handleChange}
                                        required
                                    />
                                </Grid>
                                <Grid item xs={12} md={12}>
                                    <TextField
                                        label="Dirección"
                                        fullWidth
                                        name="address"
                                        value={property.address}
                                        onChange={handleChange}
                                        required
                                    />
                                </Grid>
                                <Grid item xs={12} md={12}>
                                    <TextField
                                        type="number"
                                        label="Precio"
                                        fullWidth
                                        name="price"
                                        value={property.price}
                                        onChange={handleChange}
                                        required
                                    />
                                </Grid>
                                <Grid item xs={12} md={12}>
                                    <TextField
                                        type="number"
                                        label="Año"
                                        fullWidth
                                        name="year"
                                        value={property.year}
                                        onChange={handleChange}
                                        required
                                    />
                                </Grid>
                                <Grid item xs={12} md={12}>
                                    <Button
                                        sx={{ width: '100%' }}
                                        type="submit"
                                        variant="contained"
                                        startIcon={<SendIcon />}
                                    >
                                        Crear
                                    </Button>
                                </Grid>
                            </Grid>
                        </form>
                    </Grid>
                </Grid>
            </Container>
        </>
    );
}

export default PropertyForm;