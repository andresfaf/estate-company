export interface PropertyImage {
  id: string;
  file: string;
  enabled: boolean;
  idProperty: string;
}

export interface PropertyImageBase64 {
  image: string;
  enable: boolean;
};

export interface PropertyImageFile {
  file: File;
  enabled: boolean;
};