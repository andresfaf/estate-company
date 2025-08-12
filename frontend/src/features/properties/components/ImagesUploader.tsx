import React, { useEffect, useRef, useState } from 'react';
import { Box, Fab, Stack, Tooltip } from '@mui/material';
import CustomImageList from './CustomImageList';
import type { PropertyImageBase64, PropertyImageFile } from '../types/PropertyImage';
import CropOriginalIcon from '@mui/icons-material/CropOriginal';

type Props = {
    setSelectedFiles: (files: PropertyImageFile[]) => void;
    selectedFiles: PropertyImageFile[];
    initialPreview?: PropertyImageBase64[];
};

export const ImagesUploader = ({ setSelectedFiles, selectedFiles, initialPreview }: Props) => {
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [previewUrl, setPreviewUrl] = useState<PropertyImageBase64[] | null>(initialPreview || null);


    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (!files || files.length === 0) return;

        const previews: PropertyImageBase64[] = [];
        const realFiles: PropertyImageFile[] = [];

        Array.from(files).forEach((file) => {
            const reader = new FileReader();

            reader.onloadend = () => {
                previews.push({
                    image: reader.result as string,
                    enable: false
                });

                realFiles.push({
                    file,
                    enabled: false
                });

                // Cuando se hayan leído todos los archivos
                if (previews.length === files.length) {
                    setPreviewUrl(previews); // Asumiendo que ahora es un array
                    setSelectedFiles(realFiles); // Enviar todos los archivos al padre
                }
            };

            reader.readAsDataURL(file);
        });
    };

    useEffect(() => {
        if (previewUrl !== null) {
            const indexImageEnabled = previewUrl.findIndex(f => f.enable);
            const updatedFiles: PropertyImageFile[] = selectedFiles.map((file, i) =>
                i === indexImageEnabled
                    ? { ...file, enabled: true }
                    : { ...file, enabled: false }
            );
            setSelectedFiles(updatedFiles);
        }
    }, [previewUrl]);



    return (
        <Box>
            <Stack direction="row" spacing={2} alignItems="center">
                <CustomImageList propertyImagesBase64={previewUrl ?? []} setPreviewUrl={setPreviewUrl} />
            </Stack>
            <Tooltip title='Adjuntar imagenes' arrow placement='left'>
                <Fab
                    color="primary"
                    aria-label="add"
                    sx={{
                        position: "fixed",
                        bottom: 16, // distancia desde abajo
                        right: 16, // distancia desde la derecha
                    }}
                    onClick={() => fileInputRef.current?.click()}
                >
                    <CropOriginalIcon />
                </Fab>
            </Tooltip>
            <input
                ref={fileInputRef}
                type="file"
                multiple
                accept="image/*"
                style={{ display: 'none' }}
                onChange={handleFileChange}
            />
        </Box>
    );
};
