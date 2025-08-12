import { Box, Card, CardContent, Grid, IconButton, ListItem, ListItemIcon, ListItemText, Tooltip } from '@mui/material';
import type Property from '../types/Property';
import { Delete, History, Search, LocationOn, AttachMoney, Home } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';

type Props = {
    property: Property,
    onDelete: () => void
}

export const PropertyCard = ({ property, onDelete }: Props) => {
    const navigate = useNavigate();
    return (
        <Grid item xs={12} sm={6} md={4} lg={4} key={property.id}>
            <Card elevation={5} sx={{ borderRadius: 3, boxShadow: 3, position: 'relative' }}>
                <Box sx={{ position: "relative", display: "inline-block", width: '100%', height: 200, }}>
                    <img src={property.imageEnabled} alt={property.name}
                        style={{ width: "100%", height: "100%", objectFit: "cover" }}
                    />

                    <Tooltip title='Detalle' arrow placement='left'>
                        <IconButton
                            size="medium"
                            sx={{
                                position: "absolute",
                                top: 10,
                                right: 10,
                                backgroundColor: "rgba(0,0,0,0.5)",
                                "&:hover": { backgroundColor: "rgba(0,0,0,1)" },
                            }}
                            onClick={() => navigate(`/property/detail/${property.id}`)}
                        >
                            <Search fontSize="small" sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>

                    <Tooltip title='Agregar rastro de propiedad' arrow placement='left'>
                        <IconButton
                            size="medium"
                            sx={{
                                position: "absolute",
                                top: 60,
                                right: 10,
                                backgroundColor: "rgba(0,0,0,0.5)",
                                "&:hover": { backgroundColor: "rgba(0,0,0,1)" },
                            }}
                            onClick={() => navigate(`/property/trace/${property.id}`)}
                        >
                            <History fontSize="small" sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>

                    <Tooltip title='Eliminar' arrow placement='left'>
                        <IconButton
                            size="medium"
                            sx={{
                                position: "absolute",
                                top: 110,
                                right: 10,
                                backgroundColor: "rgba(0,0,0,0.5)",
                                "&:hover": { backgroundColor: "rgba(0,0,0,1)" },
                            }}
                            onClick={onDelete}
                        >
                            <Delete fontSize="small" sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>
                </Box>

                <CardContent>
                    <ListItem>
                        <ListItemIcon>
                            <Home />
                        </ListItemIcon>
                        <ListItemText id="switch-list-label-wifi" primary={property.name.length > 10 ? `${property.name.substring(0, 15)}...` : property.name} />
                    </ListItem>

                    <ListItem>
                        <ListItemIcon>
                            <LocationOn />
                        </ListItemIcon>
                        <ListItemText id="switch-list-label-wifi" primary={property.address.length > 10 ? `${property.address.substring(0, 18)}...` : property.address} />
                    </ListItem>

                    <ListItem>
                        <ListItemIcon>
                            <AttachMoney />
                        </ListItemIcon>
                        <ListItemText id="switch-list-label-wifi" primary={property.price?.toLocaleString('en-US')} />
                    </ListItem>
                </CardContent>
            </Card>
        </Grid>
    );
};
