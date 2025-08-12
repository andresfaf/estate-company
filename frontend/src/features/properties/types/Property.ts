import type { PropertyImageBase64 } from "./PropertyImage";

export default interface Property {
  id: string;
  name: string;
  address: string;
  price: number | null;
  year: number | null;
  idOwner: string;
  images: PropertyImageBase64[];
  imageEnabled: string;
};