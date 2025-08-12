export const deleteOwner = async (id: string) => {
  const response = await fetch(`${import.meta.env.VITE_API_URL}/Owner/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(`Error al eliminar: ${response.status}`);
  }
};

export const getAllOwners = async () => {
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/Owner`);
    if (!response.ok) {
      throw new Error('No se pudo obtener la información');
    }

    return await response.json();
  } catch (err) {
    console.error('Error al obtener propietarios:', err);
  }
}