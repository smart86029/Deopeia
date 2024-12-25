import { usePreferencesStore } from '@/stores/preferences';
import 'dayjs/locale/en';
import 'dayjs/locale/zh-tw';
import customParseFormat from 'dayjs/plugin/customParseFormat';
import dayOfYear from 'dayjs/plugin/dayOfYear';
import duration from 'dayjs/plugin/duration';
import localeData from 'dayjs/plugin/localeData';
import localizedFormat from 'dayjs/plugin/localizedFormat';
import relativeTime from 'dayjs/plugin/relativeTime';
import updateLocale from 'dayjs/plugin/updateLocale';
import utc from 'dayjs/plugin/utc';
import { dayjs } from 'element-plus';

dayjs.extend(customParseFormat);
dayjs.extend(dayOfYear);
dayjs.extend(duration);
dayjs.extend(localeData);
dayjs.extend(localizedFormat);
dayjs.extend(relativeTime);
dayjs.extend(updateLocale);
dayjs.extend(utc);

dayjs.updateLocale('en', {
  formats: {
    L: 'YYYY-MM-DD',
    LLLL: 'YYYY-MM-DD HH:mm:ss',
  },
});

const { locale } = storeToRefs(usePreferencesStore());
dayjs.locale(locale.value.dayjsCode);

export const weekday = (dayOfWeek: number): string => {
  return dayjs.weekdays()[dayOfWeek];
};

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

export const formatDate = (value: any): string => {
  const date = dayjs(value);
  return date.isValid() ? date.format('L') : value.toString();
};

export const formatDateTime = (value: any): string => {
  const date = dayjs(value);
  return date.isValid() ? date.format('LLLL') : value.toString();
};

export const humanizeDuration = (
  amount: number,
  unit:
    | 'milliseconds'
    | 'seconds'
    | 'minutes'
    | 'hours'
    | 'days'
    | 'months'
    | 'years',
): string => {
  return dayjs.duration(amount, unit).humanize();
};

export const formatDuration = (
  startedAt: dayjs.Dayjs | string | Date,
  endedAt: dayjs.Dayjs | string | Date,
): string => {
  const diff = dayjs(endedAt).diff(dayjs(startedAt));
  return dayjs.duration(diff).humanize();
};
