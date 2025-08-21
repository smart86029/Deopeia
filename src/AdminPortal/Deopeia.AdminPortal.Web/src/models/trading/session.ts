import type { DayOfWeek } from '../day-of-week';

export interface Session {
  openDay: DayOfWeek;
  openTime: string;
  closeDay: DayOfWeek;
  closeTime: string;
}
