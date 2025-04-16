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

export const defaultQuery: PageQuery = {
  pageIndex: 1,
  pageSize: 10,
};

export const defaultResult = <TItem>(): PageResult<TItem> => ({
  pageIndex: 1,
  pageSize: 10,
  itemCount: 0,
  items: [],
});

export const reassign = <TItem>(
  query: PageQuery,
  result: PageResult<TItem>,
  data: PageResult<TItem>,
): void => {
  query.pageIndex = data.pageIndex;
  query.pageSize = data.pageSize;
  Object.assign(result, data);
};
