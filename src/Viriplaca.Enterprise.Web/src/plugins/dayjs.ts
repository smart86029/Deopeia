import en from 'dayjs/locale/en';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import duration from 'dayjs/plugin/duration';
import localizedFormat from 'dayjs/plugin/localizedFormat';
import relativeTime from 'dayjs/plugin/relativeTime';
import updateLocale from 'dayjs/plugin/updateLocale';
import { dayjs } from 'element-plus';

dayjs.locale(en);

dayjs.extend(customParseFormat);
dayjs.extend(duration);
dayjs.extend(localizedFormat);
dayjs.extend(relativeTime);
dayjs.extend(updateLocale);

dayjs.updateLocale('en', {
  formats: {
    L: 'YYYY-MM-DD',
    LLLL: 'YYYY-MM-DD HH:mm:ss',
  },
});

export const rangeDay = (): Date[] => {
  const now = dayjs();
  const today = dayjs(now.format('L'));
  return [today.toDate(), today.add(1, 'day').toDate()];
};

export const rangeWeek = (): Date[] => {
  const now = dayjs();
  const today = dayjs(now.format('L'));
  return [today.subtract(6, 'day').toDate(), today.add(1, 'day').toDate()];
};

export const dateFormatter = (row: any, column: any, cellValue: any): string =>
  formatDate(cellValue);

export const formatDate = (value: any): string => {
  const date = dayjs(value);
  return date.isValid() ? date.format('L') : value.toString();
};

export const dateTimeFormatter = (
  row: any,
  column: any,
  cellValue: any,
): string => formatDateTime(cellValue);

export const formatDateTime = (value: any): string => {
  const date = dayjs(value);
  return date.isValid() ? date.format('LLLL') : value.toString();
};

export const formatDuration = (
  startedAt: dayjs.Dayjs | string | Date,
  endedAt: dayjs.Dayjs | string | Date,
): string => {
  const diff = dayjs(endedAt).diff(dayjs(startedAt));
  return dayjs.duration(diff).humanize();
};
