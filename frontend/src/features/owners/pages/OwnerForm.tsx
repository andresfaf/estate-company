import {
    Container,
    Typography,
    TextField,
    Grid,
    Button,
    CircularProgress,
    Divider
} from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import type Owner from '../types/Owner';
import { ImageUploader } from '../components/ImageUploader';
import SendIcon from '@mui/icons-material/Send';
import base64ToFile from '../../../utils/Base64ToFile';

const defaultOwner: Owner = {
    id: '',
    name: '',
    address: '',
    photo: '',
    birthday: null
};

const OwnerForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEditMode = Boolean(id);

    const [owner, setOwner] = useState<Owner>(defaultOwner);
    const [loading, setLoading] = useState(false);
    const [submitting, setSubmitting] = useState(false);
    const [selectedFile, setSelectedFile] = useState<File | null>(null);

    useEffect(() => {
        if (isEditMode) {
            setLoading(true);
            fetch(`${import.meta.env.VITE_API_URL}/owner/${id}`)
                .then(res => res.json())
                .then(data => {
                    setOwner(data);
                    setLoading(false);
                })
                .catch(() => setLoading(false));
        }
    }, [id, isEditMode]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setOwner(prev => ({
            ...prev,
            [name]: value,
        }));
    };
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSubmitting(true);

        const formData = new FormData();

        formData.append("name", owner.name);
        formData.append("address", owner.address);
        formData.append("imageFile", selectedFile ? selectedFile : base64ToFile(owner.photo, owner.name));
        formData.append("selectedFile", selectedFile ? 'true' : 'false');
        formData.append("birthday", owner.birthday?.toString() ?? '');
        /* eslint-disable no-debugger */
        debugger
        if (isEditMode)
            formData.append("id", owner.id);

        const url = `${import.meta.env.VITE_API_URL}/Owner`;

        const method = isEditMode ? 'PUT' : 'POST';

        await fetch(url, {
            method,
            body: formData,
        });

        setSubmitting(false);
        navigate('/owners');
    };

    if (loading) return <CircularProgress sx={{ m: 4 }} />;

    return (
        <Container maxWidth="sm">
            <Typography variant="h5" gutterBottom align='center' sx={{ marginTop: '15px' }}>
                {isEditMode ? 'Editar Propietario' : 'Crear Propietario '}
            </Typography>
            <Divider sx={{ margin: '20px 0px' }} />
            
            <Grid container spacing={2}>
                <Grid item container xs={12} sm={6} md={6}>
                    <ImageUploader onImageSelect={(file) => setSelectedFile(file)} initialPreview={owner} />
                </Grid>
                <Grid item container xs={12} sm={6} md={6}>
                    <form onSubmit={handleSubmit}>
                        <Grid container spacing={2} sx={{ marginTop: '10px' }}>
                            <Grid item xs={12} md={12}>
                                <TextField
                                    label="Nombre"
                                    fullWidth
                                    name="name"
                                    value={owner.name}
                                    onChange={handleChange}
                                    required
                                />
                            </Grid>

                            <Grid item xs={12} md={12}>
                                <TextField
                                    label="Dirección"
                                    fullWidth
                                    name="address"
                                    value={owner.address}
                                    onChange={handleChange}
                                    required
                                />
                            </Grid>
                            <Grid item xs={12} md={12}>
                                <TextField
                                    label="Fecha de cumpleaños"
                                    type='date'
                                    fullWidth
                                    name="birthday"
                                    value={owner.birthday}
                                    onChange={handleChange}
                                    required
                                    InputLabelProps={{ shrink: true }} 
                                />
                            </Grid>

                            <Grid item xs={12} md={12}>
                                <Button
                                    sx={{ width: '100%' }}
                                    type="submit"
                                    variant="contained"
                                    disabled={submitting}
                                    startIcon={<SendIcon />}
                                >
                                    {isEditMode ? 'Actualizar' : 'Crear'}
                                </Button>
                            </Grid>
                        </Grid>
                    </form>
                </Grid>
            </Grid>
        </Container>
    );
};

export default OwnerForm;