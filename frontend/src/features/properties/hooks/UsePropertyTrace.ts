import { useEffect, useState } from "react";
import type { PropertyWithTraces } from "../types/PropertyTrace";
import { getPropertyWithTraces } from "../services/PropertyService";

type Props = {
    id: string
}

export const usePropertyTrace = ({ id }: Props) => {
    const [propertyWithTrace, setPropertyWithTrace] = useState<PropertyWithTraces | undefined>(undefined);
    const [loading, setLoading] = useState<boolean>(false);

    const fetchData = async () => {
        try {
            if (id) {
                setLoading(true);
                const data = await getPropertyWithTraces(id);
                setPropertyWithTrace(data);
            }
        } catch (error) {
            console.error("Error al obtener propiedad con rastro:", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    return {
        propertyWithTrace,
        loading,
        fetchData
    };
}