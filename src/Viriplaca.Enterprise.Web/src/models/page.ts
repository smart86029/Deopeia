export interface PageQuery {
  pageIndex: number;
  pageSize: number;
}

export interface PageResult<TItem> {
  pageIndex: number;
  pageSize: number;
  itemCount: number;
  items: TItem[];
}
