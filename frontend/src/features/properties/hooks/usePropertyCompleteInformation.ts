import { useEffect, useState } from "react";
import type { PropertyCompleteInformation } from "../types/PropertyCompleteInformation";

type Props = {
    id: string
}

export const usePropertyCompleteInformation = ({ id }: Props) => {
    const [completeInformation, setCompleteinformation] = useState<PropertyCompleteInformation | undefined>(undefined);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const fetchData = async () => {
        setLoading(true);
        try {
            const res = await fetch(`${import.meta.env.VITE_API_URL}/Property/complete-information/${id}`);
            const json = await res.json();
            setCompleteinformation(json);
        } catch (error) {
            setError(`${error as Error}`);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchData();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [id]);

    return { completeInformation, loading, error };

}