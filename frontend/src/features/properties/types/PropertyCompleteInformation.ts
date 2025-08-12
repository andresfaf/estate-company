import type Owner from "../../owners/types/Owner";
import type Property from "./Property";
import type { PropertyImage } from "./PropertyImage";
import type { PropertyTrace } from "./PropertyTrace";

export interface PropertyCompleteInformation {
    owner: Owner;
    property: Property;
    propertyImages: PropertyImage[];
    propertyTraces: PropertyTrace[]
}