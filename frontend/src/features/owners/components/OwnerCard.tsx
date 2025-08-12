import { useNavigate } from 'react-router-dom';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import CakeIcon from '@mui/icons-material/Cake';
import LocationOn from '@mui/icons-material/LocationOn';
import { Box, Card, CardContent, Grid, IconButton, ListItem, ListItemIcon, ListItemText, Tooltip } from "@mui/material";
import type Owner from '../types/Owner';

type Props = {
    owner: Owner
    onDelete: () => void;
}

const OwnerCard = ({ owner, onDelete }: Props) => {
    const navigate = useNavigate();

    return (
        <Grid item xs={12} sm={6} md={4} lg={4} key={owner.id}>
            <Card elevation={5} sx={{ borderRadius: 3, boxShadow: 3, position: 'relative' }}>
                <Box sx={{ position: "relative", display: "inline-block", width: '100%', height: 350, }}>
                    <img src={owner.photo} alt={owner.name}
                        style={{ width: "100%", height: "100%", objectFit: "cover" }}
                    />

                    <Tooltip title='Editar propietario' arrow placement='left'>
                        <IconButton
                            size="medium"
                            sx={{
                                position: "absolute",
                                top: 10,
                                right: 10,
                                backgroundColor: "rgba(0,0,0,0.5)",
                                "&:hover": { backgroundColor: "rgba(0,0,0,1)" },
                            }}
                            onClick={() => navigate(`/owner/edit/${owner.id}`)}
                        >
                            <EditIcon fontSize="small" sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>

                    <Tooltip title='Eliminar propietario' arrow placement='left'>
                        <IconButton
                            size="medium"
                            sx={{
                                position: "absolute",
                                top: 60,
                                right: 10,
                                backgroundColor: "rgba(0,0,0,0.5)",
                                "&:hover": { backgroundColor: "rgba(0,0,0,1)" },
                            }}
                            onClick={onDelete}
                        >
                            <DeleteIcon fontSize="small" sx={{ color: 'white' }} />
                        </IconButton>
                    </Tooltip>
                </Box>

                <CardContent>
                    <ListItem>
                        <ListItemText id="switch-list-label-wifi" primary={owner.name} />
                    </ListItem>

                    <ListItem>
                        <ListItemIcon>
                            <LocationOn />
                        </ListItemIcon>
                        <ListItemText id="switch-list-label-wifi" primary={owner.address} />
                    </ListItem>

                    <ListItem>
                        <ListItemIcon>
                            <CakeIcon />
                        </ListItemIcon>
                        <ListItemText id="switch-list-label-wifi" primary={owner.birthday?.toString()} />
                    </ListItem>
                </CardContent>
            </Card>
        </Grid>
    );
}

export default OwnerCard;