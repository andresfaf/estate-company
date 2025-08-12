import ImageList from '@mui/material/ImageList';
import ImageListItem from '@mui/material/ImageListItem';
import ImageListItemBar from '@mui/material/ImageListItemBar';
import IconButton from '@mui/material/IconButton';
import StarBorderIcon from '@mui/icons-material/StarBorder';
import StarIcon from '@mui/icons-material/Star';
import type { PropertyImageBase64 } from '../types/PropertyImage';
import { useEffect, useState } from 'react';
import { Skeleton, Tooltip } from '@mui/material';

type Props = {
    propertyImagesBase64: PropertyImageBase64[],
    setPreviewUrl: (files: PropertyImageBase64[]) => void;
};

export default function CustomImageList({ propertyImagesBase64, setPreviewUrl }: Props) {

    const [images, setImages] = useState<PropertyImageBase64[]>(propertyImagesBase64);
    const setEnabled = (index: number) => {
        setImages(prevImages =>
            prevImages.map((img, i) =>
                i === index ? { ...img, enable: true } : { ...img, enable: false }
            )
        );
    }

    useEffect(() => {
        setPreviewUrl(images)
    }, [images]);

    useEffect(() => {
        setImages(propertyImagesBase64);
    }, [propertyImagesBase64])

    return (
        <>
            {
                images.length ?
                    <ImageList sx={{ width: 500, height: 450 }} cols={3} rowHeight={164}>
                        {images.map((item, index) => (

                            <ImageListItem key={item.image} sx={{ marginTop: '20px' }}>
                                <img
                                    srcSet={`${item.image}`}
                                    src={`${item.image}?w=164&h=164&fit=crop&auto=format`}
                                    alt={item.image}
                                    loading="lazy"
                                />
                                <Tooltip title={item.enable ? 'Imagen marcada como favorita' : 'Marcar esta imagen como favorita'} arrow placement="top">
                                    <ImageListItemBar
                                        sx={{
                                            background:
                                                'linear-gradient(to bottom, rgba(0,0,0,0.7) 0%, ' +
                                                'rgba(0,0,0,0.3) 70%, rgba(0,0,0,0) 100%)',
                                        }}
                                        onClick={() => setEnabled(index)}
                                        position="top"
                                        actionIcon={
                                            <IconButton sx={{ color: 'white' }}>
                                                {item.enable ? <StarIcon /> : <StarBorderIcon />}
                                            </IconButton>
                                        }
                                        actionPosition="left"
                                    />
                                </Tooltip>
                            </ImageListItem>
                        ))}
                    </ImageList>
                    : <Skeleton variant="rectangular" width={500} height={500} />
            }
        </>
    );
}

