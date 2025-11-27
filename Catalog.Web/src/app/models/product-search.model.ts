export class ProductSearch {
    constructor(
        public searchText: string,
        public pageSize: number,
        public pageIndex: number,
        public sort: string
    ) { }
}
