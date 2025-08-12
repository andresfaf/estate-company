import React, { useRef, useState } from 'react';
import {
    Box,
    Button,
    Stack,
    Avatar,
    Typography
} from '@mui/material';
import UploadIcon from '@mui/icons-material/Upload';
import type Owner from '../types/Owner';

type Props = {
    onImageSelect: (file: File) => void;
    initialPreview?: Owner;
};

export const ImageUploader = ({ onImageSelect, initialPreview }: Props) => {
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [previewUrl, setPreviewUrl] = useState<string | null>(initialPreview?.photo || null);

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (!file) return;
        // Leer imagen como Base64 solo para previsualizar
        const reader = new FileReader();
        reader.onloadend = () => {
            setPreviewUrl(reader.result as string);
        };
        reader.readAsDataURL(file);

        // Retorna el archivo real al padre
        onImageSelect(file);
    };

    return (
        <Box>
            <Stack direction="row" spacing={2} alignItems="center">
                <Avatar
                    sx={{ margin: '0px auto', width: '200px', height: '200px' }}
                    alt={initialPreview?.name}
                    src={previewUrl || ''} />

            </Stack>
            <Button
                sx={{ width: '100%', marginTop: '20px' }}
                variant="contained"
                startIcon={<UploadIcon />}
                onClick={() => fileInputRef.current?.click()}
            >
                Subir imagen
            </Button>

            <input
                ref={fileInputRef}
                type="file"
                accept="image/*"
                style={{ display: 'none' }}
                onChange={handleFileChange}
            />

            {previewUrl && (
                <Typography variant="body2" mt={1} align='center'>
                    Imagen seleccionada
                </Typography>
            )}
        </Box>
    );
};
