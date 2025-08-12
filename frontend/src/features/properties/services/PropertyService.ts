export const getByIdProperty = async (id: string) => {
  const response = await fetch(`${import.meta.env.VITE_API_URL}/Owner/${id}`);

  if (!response.ok) {
    throw new Error(`Error al eliminar: ${response.status}`);
  }

  return await response.json()
};

export const getByFilters = async (name?: string, address?: string, range?: number[]) => {
  const params = new URLSearchParams();

  if (name) params.append('name', name);
  if (address) params.append('address', address);
  if (range !== undefined) {
    params.append('minPrice', range[0].toString());
    params.append('maxPrice', range[1].toString());
  }

  const response = await fetch(`${import.meta.env.VITE_API_URL}/Property/filter?${params.toString()}`);

  if (!response.ok) {
    throw new Error(`Error al eliminar: ${response.status}`);
  }
  return await response.json();
}

export const deleteProperty = async (id: string) => {
  const response = await fetch(`${import.meta.env.VITE_API_URL}/Property/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(`Error al eliminar propiedad: ${response.status}`);
  }
};

export const getPropertyWithTraces = async (id: string) => {
  const response = await fetch(`${import.meta.env.VITE_API_URL}/PropertyTrace/property-with-traces/${id}`);

  if (!response.ok) {
    throw new Error(`Error al eliminar: ${response.status}`);
  }

  return await response.json()
}
