const base64ToFile = (base64: string, filename: string): File => {
    // Extraer el tipo MIME desde el Base64 (ej. "image/png", "image/jpeg")
    const mimeMatch = base64.match(/:(.*?);/);
    const mime = mimeMatch ? mimeMatch[1] : "application/octet-stream";

    // Extraer la extensión (ej. "png", "jpeg")
    const ext = mime.split("/")[1];

    // Decodificar la parte de datos (después de la coma)
    const bstr = atob(base64.split(",")[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    // Crear el objeto File con el nombre y tipo correctos
    return new File([u8arr], `${filename}.${ext}`, { type: mime });
}

export default base64ToFile