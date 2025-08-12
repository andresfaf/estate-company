import { useParams } from 'react-router-dom';
import { usePropertyCompleteInformation } from '../hooks/usePropertyCompleteInformation';
import { Alert, Avatar, CircularProgress, Grid, Typography } from '@mui/material';
import Timeline from '@mui/lab/Timeline';
import TimelineItem from '@mui/lab/TimelineItem';
import TimelineSeparator from '@mui/lab/TimelineSeparator';
import TimelineConnector from '@mui/lab/TimelineConnector';
import TimelineContent from '@mui/lab/TimelineContent';
import TimelineDot from '@mui/lab/TimelineDot';
import TimelineOppositeContent from '@mui/lab/TimelineOppositeContent';

const PropertyDetail: React.FC = () => {
    const { id } = useParams();

    const { completeInformation: data, loading, error } = usePropertyCompleteInformation({ id: id! });


    if (loading) return (
        <Grid container spacing={2} mt={3}>
            <Grid item container xs={12} sm={12} md={12} lg={12} justifyContent="center" alignItems="center" >
                <CircularProgress sx={{ m: 4 }} />
            </Grid>
        </Grid>
    )

    if (error) return (
        <Grid container spacing={2} mt={3}>
            <Grid item container xs={12} sm={12} md={12} lg={12} justifyContent="center" alignItems="center" >
                <Alert severity="error">{error}</Alert>
            </Grid>
        </Grid>
    )

    return (
        <>
            <Grid container spacing={2} mt={3}>

                <Grid item container xs={12} sm={6} md={6} lg={6} justifyContent="start" alignItems="center" direction="column">

                    <Grid item>
                        <Typography fontSize={16} fontWeight={600}>Información del propietario</Typography>
                    </Grid>
                    <Grid item>
                        <Avatar alt={data?.owner.name} src={data?.owner.photo} sx={{ width: '200px', height: '200px' }} />
                    </Grid>
                    <Grid item mt={1}>
                        <Typography variant='h6'>{data?.owner.name}</Typography>
                    </Grid>
                    <Grid item>
                        <Typography variant='subtitle1' sx={{ color: 'grey' }}>{data?.owner.address}</Typography>
                    </Grid>


                    <Grid item mt={5}>
                        <Typography fontSize={16} fontWeight={600}>Imagenes de la propiedad</Typography>
                    </Grid>
                    <Grid item container mt={1} spacing={1} justifyContent="center" alignItems="center">

                        {
                            data?.propertyImages.length == 0 ?
                                <Typography>No hay imagenes para esta propiedad</Typography>
                                : data?.propertyImages.filter(i => !i.enabled).map((image, index) => (
                                    <Grid item xs={12} sm={6} md={6} lg={6} key={index}>
                                        <img src={image.file} alt={`Propiedad ${index}`} style={{ width: '100%', height: '100%', borderRadius: '10px' }} />
                                    </Grid>
                                ))}
                    </Grid>
                </Grid>

                <Grid item container xs={12} sm={6} md={6} lg={6} justifyContent="start" alignItems="center" direction="column">
                    <Grid item mb={1}>
                        <Typography fontSize={16} fontWeight={600}>Información de la propiedad</Typography>
                    </Grid>
                    {data?.propertyImages.filter(i => i.enabled).map((image, index) => (
                        <Grid item key={index} sx={{ width: '100%' }} >
                            <img src={image.file} alt={`Propiedad ${index}`} style={{ width: '100%', height: '300px', borderRadius: '10px' }} />
                        </Grid>
                    ))}

                    <Grid item container justifyContent="center" alignItems="center">
                        <Grid item xs={12} sm={12} md={12} lg={12}>
                            <Typography fontSize={16} fontWeight={600}>{data?.property.name} - codeInternal</Typography>
                        </Grid>
                        <Grid item xs={12} sm={12} md={12} lg={12}>
                            <Typography fontSize={16} fontWeight={500}>{data?.property.address}</Typography>
                        </Grid>
                        <Grid item xs={12} sm={6} md={6} lg={6}>
                            <Typography fontSize={16} fontWeight={500}>Valor ${data?.property.price?.toLocaleString('en-US')}</Typography>
                        </Grid>
                        <Grid item xs={12} sm={6} md={6} lg={6}>
                            <Typography fontSize={16} fontWeight={500}>Año {data?.property.year}</Typography>
                        </Grid>
                    </Grid>

                    <Grid item container mt={7} justifyContent="center" alignItems="center">
                        <Grid item mb={1}>
                            <Typography fontSize={16} fontWeight={600}>Historial de transacciones para esta propiedad</Typography>
                        </Grid>
                        {
                            data?.propertyTraces.length == 0 ?
                                <Typography>No hay datos de transacciones para esta propiedad</Typography>
                                : data?.propertyTraces.map((trace, index) => (
                                    <Grid item xs={12} sm={12} md={12} lg={12} key={index}>
                                        <Timeline position="alternate">
                                            <TimelineItem>
                                                <TimelineOppositeContent color="text.secondary">
                                                    <Typography >${trace.value?.toLocaleString('en-US')}</Typography>
                                                    <Typography>${trace.tax?.toLocaleString('en-US')}</Typography>

                                                </TimelineOppositeContent>
                                                <TimelineSeparator>
                                                    <TimelineDot />
                                                    <TimelineConnector />
                                                </TimelineSeparator>
                                                <TimelineContent>
                                                    <Typography variant="h6" component="span">{trace.name}</Typography>
                                                    <Typography>{trace.dateSale?.toString()}</Typography>
                                                </TimelineContent>
                                            </TimelineItem>
                                        </Timeline>
                                    </Grid>
                                ))}
                    </Grid>
                </Grid>
            </Grid>
        </>
    );
}

export default PropertyDetail;