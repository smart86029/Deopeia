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

export const dateFormatter = (
  row: any,
  column: any,
  cellValue: any,
): string => {
  const date = dayjs(cellValue);
  return date.isValid() ? date.format('L') : cellValue.toString();
};

export const dateTimeFormatter = (
  row: any,
  column: any,
  cellValue: any,
): string => {
  const date = dayjs(cellValue);
  return date.isValid() ? date.format('LLLL') : cellValue.toString();
};

export const durationFormatter = (
  startedAt: dayjs.Dayjs | string,
  endedAt: dayjs.Dayjs | string,
): string => {
  const diff = dayjs(endedAt).diff(dayjs(startedAt));
  return dayjs.duration(diff).humanize();
};
