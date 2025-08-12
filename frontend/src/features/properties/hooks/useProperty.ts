import { useEffect, useState } from 'react';
import type Property from '../features/properties/types/Property';

export const useProperties = () => {
  const [properties, setProperties] = useState<Property[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchData = async () => {
    try {
      const response = await fetch(`${import.meta.env.VITE_API_URL}/Property`);
      if (!response.ok) {
        throw new Error('No se pudo obtener la información');
      }

      const data = await response.json();
      setProperties(data);
    } catch (err) {
      console.error('Error al obtener propiedades:', err);
      setError('Error al obtener las propiedades.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return {
    properties,
    loading,
    error,
    setProperties,
    fetchData
  };
};
