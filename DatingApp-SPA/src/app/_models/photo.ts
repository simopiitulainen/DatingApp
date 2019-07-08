export interface Photo {
    id: number;
    url: string;
    isApproved: boolean;
    description?: string;
    dateAdded?: Date;
    isMain?: boolean;

}
