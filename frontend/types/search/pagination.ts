export type PaginationType ={
    offset: number;
    limit: number;
    count: number;
    loading: boolean;
    onChange: (newOffset: number) => void;
}