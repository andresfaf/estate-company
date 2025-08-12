import React, { useEffect, type Dispatch, type SetStateAction } from "react";
import { useNavigate } from 'react-router-dom';
import { Add, Tune } from '@mui/icons-material';
import { Box, Button, Divider, Grid, TextField, Typography, Slider } from '@mui/material';
import { getByFilters } from '../services/PropertyService';
import type Property from "../types/Property";

type Props = {
    setProperties: Dispatch<SetStateAction<Property[]>>;
    cleanFilters: boolean;
};

const Filters = ({ setProperties, cleanFilters }: Props) => {
    const navigate = useNavigate();

    const [name, setName] = React.useState<string>('');
    const [address, setAddress] = React.useState<string>('');
    const [range, setRange] = React.useState<number[]>([100000, 4000000]);

    const handleChangeRange = (event: Event, newRange: number | number[], activeThumb: number) => {
        if (!Array.isArray(newRange)) {
            return;
        }

        if (activeThumb === 0) {
            setRange([Math.min(newRange[0], range[1]), range[1]]);
        } else {
            setRange([range[0], Math.max(newRange[1], range[0])]);
        }
    };

    const valueLabelFormat = (value: number) => {
        if (value >= 1000000)
            return `${value / 1000000} M`;
        else
            return `${value / 1000} K`;
    };

    const handleApplyFilters = () => {

        const fetchPropertiesByFilters = async () => {
            try {
                const properties = await getByFilters(name, address, range);
                setProperties(properties);
            } catch (error) {
                console.error("Error al aplicar filtros:", error);
            }
        };
        fetchPropertiesByFilters();
    };

    useEffect(() => {
        setName('');
        setAddress('');
        setRange([100000, 4000000]);
    }, [cleanFilters]);

    return (
        <Grid container xs={12} sm={4} md={3} lg={3} spacing={2}>
            <Grid item xs={12}>
                <Box
                    sx={{
                        position: 'fixed',
                        display: 'flex',
                        flexDirection: 'column',
                        justifyContent: 'flex-start',
                        alignItems: 'stretch',
                        height: '100%', // si el panel tiene altura completa
                        gap: 2, // espacio entre elementos
                        p: 2, // padding interno
                    }}
                >
                    <Typography align='center'>
                        <Button
                            sx={{ marginTop: '10px', width: '100%' }}
                            startIcon={<Add />} variant="contained" color="primary"
                            onClick={() => navigate(`/property/create`)} >Agregar propiedad
                        </Button>
                    </Typography>
                    <Typography variant="h6" align='left' sx={{ marginBottom: '-15px' }}>Filtros</Typography>
                    <Divider />
                    <TextField
                        label="Nombre"
                        fullWidth
                        name="name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                    <TextField
                        label="Dirección"
                        fullWidth
                        name="name"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                        required
                    />

                    <Typography variant="subtitle1" sx={{ marginBottom: '-20px' }}>Precio USD</Typography>
                    <Typography variant="subtitle2">Rango {`$${range[0].toLocaleString('en-US')} - $${range[1].toLocaleString('en-US')}`}</Typography>
                    <Slider
                        min={100000}
                        step={100000}
                        max={4000000}
                        value={range}
                        onChange={handleChangeRange}
                        valueLabelDisplay="auto"
                        disableSwap
                        valueLabelFormat={valueLabelFormat}
                    />

                    <Typography align='center'>
                        <Button
                            sx={{ marginTop: '10px', width: '90%' }}
                            startIcon={<Tune />} variant="contained" color="primary"
                            onClick={handleApplyFilters} >Filtrar
                        </Button>
                    </Typography>

                </Box>
            </Grid>

        </Grid>

    );
}

export default Filters;