import dayjs, { Dayjs } from 'dayjs';
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

import { usePreferencesStore } from '@/stores/preferences';

export type DurationUnit =
  | 'milliseconds'
  | 'seconds'
  | 'minutes'
  | 'hours'
  | 'days'
  | 'months'
  | 'years';
export type DateInput = Dayjs | string | Date | null | undefined;

dayjs.extend(customParseFormat);
dayjs.extend(dayOfYear);
dayjs.extend(duration);
dayjs.extend(localeData);
dayjs.extend(localizedFormat);
dayjs.extend(relativeTime);
dayjs.extend(updateLocale);
dayjs.extend(utc);

const updateLocaleFormats = (localeKey: string) => {
  dayjs.updateLocale(localeKey, {
    formats: {
      L: 'YYYY-MM-DD',
      LLLL: 'YYYY-MM-DD HH:mm:ss',
    },
  });
};

updateLocaleFormats('en');
updateLocaleFormats('zh-tw');

const { locale } = storeToRefs(usePreferencesStore());
dayjs.locale(locale.value.dayjsCode);

export const weekday = (dayOfWeek: number): string => {
  try {
    const weekdays = dayjs.weekdays();
    if (dayOfWeek < 0 || dayOfWeek >= weekdays.length) {
      throw new Error(`Invalid day of week: ${dayOfWeek}`);
    }
    return weekdays[dayOfWeek];
  } catch (error) {
    console.error('Error getting weekday:', error);
    return '';
  }
};

export const rangeDay = (): Date[] => {
  try {
    const now = dayjs();
    const today = dayjs(now.format('L'));
    return [today.toDate(), today.add(1, 'day').toDate()];
  } catch (error) {
    console.error('Error creating day range:', error);
    return [new Date(), new Date()];
  }
};

export const rangeWeek = (): Date[] => {
  try {
    const now = dayjs();
    const today = dayjs(now.format('L'));
    return [today.subtract(6, 'day').toDate(), today.add(1, 'day').toDate()];
  } catch (error) {
    console.error('Error creating week range:', error);
    return [new Date(), new Date()];
  }
};

export const formatDate = (value: DateInput): string => {
  if (!value) {
    return '';
  }

  try {
    const date = dayjs(value);
    return date.isValid() ? date.format('L') : String(value);
  } catch (error) {
    console.error('Error formatting date:', error);
    return String(value);
  }
};

export const formatDateTime = (value: DateInput): string => {
  if (!value) {
    return '';
  }

  try {
    const date = dayjs(value);
    return date.isValid() ? date.format('LLLL') : String(value);
  } catch (error) {
    console.error('Error formatting datetime:', error);
    return String(value);
  }
};

export const humanizeDuration = (amount: number, unit: DurationUnit): string => {
  try {
    if (!Number.isFinite(amount)) {
      throw new Error(`Invalid amount: ${amount}`);
    }
    return dayjs.duration(amount, unit).humanize();
  } catch (error) {
    console.error('Error humanizing duration:', error);
    return '';
  }
};

export const formatDuration = (startedAt: DateInput, endedAt: DateInput): string => {
  try {
    if (!startedAt || !endedAt) {
      throw new Error('Both startedAt and endedAt are required');
    }

    const start = dayjs(startedAt);
    const end = dayjs(endedAt);

    if (!start.isValid() || !end.isValid()) {
      throw new Error('Invalid date inputs');
    }

    const diff = end.diff(start);
    return dayjs.duration(diff).humanize();
  } catch (error) {
    console.error('Error formatting duration:', error);
    return '';
  }
};

export { dayjs };
