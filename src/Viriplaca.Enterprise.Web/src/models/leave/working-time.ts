import { humanizeDuration } from '@/plugins/dayjs';

export interface WorkingTime {
  amount: number;
  days: number;
  hours: number;
}

export const formatWorkingTime = (workingTime: WorkingTime): string => {
  if (workingTime.amount === 0) {
    return humanizeDuration(workingTime.days, 'days');
  }

  let result = '';
  if (workingTime.days > 0) {
    result += humanizeDuration(workingTime.days, 'days');
  }
  if (workingTime.hours > 0) {
    if (result) {
      result += ' ';
    }
    result += humanizeDuration(workingTime.hours, 'hours');
  }

  return result;
};
