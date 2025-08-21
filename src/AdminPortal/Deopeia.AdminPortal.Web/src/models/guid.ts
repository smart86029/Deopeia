const validator = new RegExp(
  '^[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}$',
  'i',
);

export type Guid = string & { isGuid: true };

export const emptyGuid = '00000000-0000-0000-0000-000000000000' as Guid;

export const isGuid = (value: any): boolean =>
  typeof value === 'string' && validator.test(value);

export const parseGuid = (value: string | null): Guid =>
  isGuid(value) ? (value as Guid) : emptyGuid;
