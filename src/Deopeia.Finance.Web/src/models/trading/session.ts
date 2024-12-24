import type { DayOfWeek } from '../day-of-week';

export interface Session {
  dayOfWeek: DayOfWeek;
  openTime: string;
  closeTime: string;
}
