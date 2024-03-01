import en from 'dayjs/locale/en';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import localizedFormat from 'dayjs/plugin/localizedFormat';
import updateLocale from 'dayjs/plugin/updateLocale';
import { dayjs } from 'element-plus';

dayjs.locale(en);
dayjs.extend(localizedFormat);
dayjs.extend(updateLocale);
dayjs.extend(customParseFormat);
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
