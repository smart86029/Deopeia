export interface PagedRequest {
  pageIndex: number;
  pageSize: number;
}

export interface PagedResponse<TItem> {
  pageIndex: number;
  pageSize: number;
  pageCount: number;
  totalCount: number;
  items: TItem[];
}

export const defaultQuery: PagedRequest = {
  pageIndex: 1,
  pageSize: 10,
};

export const defaultResult = <TItem>(): PagedResponse<TItem> => ({
  pageIndex: 1,
  pageSize: 10,
  pageCount: 1,
  totalCount: 0,
  items: [],
});

export const reassign = <TItem>(query: PagedRequest, data: PagedResponse<TItem>): void => {
  query.pageIndex = data.pageIndex;
  query.pageSize = data.pageSize;
};
