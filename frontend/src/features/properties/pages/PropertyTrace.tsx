import { Button, CircularProgress, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Tooltip, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import type { PropertyTrace } from '../types/PropertyTrace';
import { Add } from '@mui/icons-material';
import { useTheme, useMediaQuery } from '@mui/material';
import { usePropertyTrace } from '../hooks/UsePropertyTrace';

const Propertytrace = () => {
    const { id } = useParams();

    const { propertyWithTrace, loading, fetchData } = usePropertyTrace({ id: id! });

    const [propertyTrace, setPropertyTrace] = useState<PropertyTrace>({
        id: '',
        name: '',
        dateSale: null,
        value: null,
        tax: null,
        idProperty: id
    });
    const theme = useTheme();
    const isLgUp = useMediaQuery(theme.breakpoints.up('lg'));
    const isMdUp = useMediaQuery(theme.breakpoints.up('md'));
    const isSmUp = useMediaQuery(theme.breakpoints.up('sm'));

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setPropertyTrace(prev => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/PropertyTrace`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(propertyTrace),
            });

            if (!response.ok) {
                throw new Error(`Error ${response.status}: ${response.statusText}`);
            }

            fetchData();
            setPropertyTrace({
                id: '',
                name: '',
                dateSale: null,
                value: null,
                tax: null,
                idProperty: id
            });
        } catch (error) {
            console.error(error);

        }
    };

    useEffect(() => {
        fetchData();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [id]);

    if (loading) return <CircularProgress sx={{ m: 4 }} />;

    return (
        <>
            <Typography variant='h5' align='center' sx={{ margin: '10px 0px -5px 0px ', color: 'grey' }}>{propertyWithTrace?.property?.name}</Typography>

            <Typography variant='h6' align='center' sx={{ marginTop: '15px', marginBottom: '10px' }}>Agregar rastro de propiedad</Typography>
            <form onSubmit={handleSubmit}>
                <Grid container spacing={2}>

                    <Grid item xs={12} sm={6} md={6} lg={3}>
                        <TextField
                            size='small'
                            label="Nombre"
                            fullWidth
                            name="name"
                            value={propertyTrace.name}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={6} md={6} lg={3}>
                        <TextField
                            size='small'
                            type='date'
                            label="Fecha de venta"
                            fullWidth
                            name="dateSale"
                            value={propertyTrace.dateSale}
                            onChange={handleChange}
                            required
                            InputLabelProps={{ shrink: true }}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6} md={6} lg={3}>
                        <TextField
                            size='small'
                            type='number'
                            label="Valor"
                            fullWidth
                            name="value"
                            value={propertyTrace.value}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={4} md={4} lg={2}>
                        <TextField
                            size='small'
                            type='number'
                            label="impuesto"
                            fullWidth
                            name="tax"
                            value={propertyTrace.tax}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid item xs={12} sm={2} md={2} lg={1}>
                        <Tooltip title='Agregar rastro para esta propiedad' arrow placement="top">
                            <Button
                                sx={{ width: '100%' }}
                                type="submit"
                                variant="contained"

                            >{isLgUp || isMdUp || isSmUp ? <Add /> : 'Agregar rastro'}
                            </Button>
                        </Tooltip>
                    </Grid>
                </Grid>
            </form>

            <Typography variant='h6' align='center' sx={{ marginTop: '1.5cm' }}>Historial de transacciones de esta propiedad</Typography>

            <TableContainer component={Paper} sx={{ marginTop: '30px' }}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead >
                        <TableRow sx={{ fontWeight: 'bold' }}>
                            <TableCell sx={{ fontWeight: 'bold', fontSize: '16px' }} >Nombre</TableCell>
                            <TableCell sx={{ fontWeight: 'bold', fontSize: '16px' }} align="center">Fecha de venta</TableCell>
                            <TableCell sx={{ fontWeight: 'bold', fontSize: '16px' }} align="right">Valor</TableCell>
                            <TableCell sx={{ fontWeight: 'bold', fontSize: '16px' }} align="right">Impuesto</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {propertyWithTrace?.propertyTraces.map((row) => (
                            <TableRow key={row.id}>
                                <TableCell component="th" scope="row">{row.name}</TableCell>
                                <TableCell align="center">{row.dateSale?.toString()}</TableCell>
                                <TableCell align="right">${row.value?.toLocaleString('en-US')}</TableCell>
                                <TableCell align="right">${row.tax?.toLocaleString('en-US')}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
}

export default Propertytrace;