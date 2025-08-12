import type Property from "./Property";

export interface PropertyTrace {
  id: string;
  name: string;
  dateSale: Date | null;
  value: number | null;
  tax: number | null;
  idProperty: string | undefined;
};

export interface PropertyWithTraces {
    property: Property;
    propertyTraces: PropertyTrace[]
}