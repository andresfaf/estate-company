import { useEffect, useState } from 'react';
import type Owner from '../features/owners/types/Owner';

export const useOwners = () => {
  const [owners, setOwners] = useState<Owner[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const fetchData = async () => {
    try {
      const response = await fetch(`${import.meta.env.VITE_API_URL}/Owner`);
      if (!response.ok) {
        throw new Error('No se pudo obtener la información');
      }

      const data = await response.json();
      setOwners(data);
    } catch (err) {
      console.error('Error al obtener propietarios:', err);
      setError('Error al obtener las propietarios.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return {
    owners,
    loading,
    error,
    fetchData
  };
};
