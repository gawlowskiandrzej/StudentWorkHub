export type PaginationType ={
    offset: number;
    limit: number;
    count: number;
    onChange: (newOffset: number) => void;
}