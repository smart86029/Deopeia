<template>
  <div v-for="session in sessions" :key="session.dayOfWeek">
    <div class="session-text">
      <el-text type="info">{{ weekday(session.dayOfWeek) }}</el-text>
      <el-space v-if="session.bars.length">
        <el-text v-for="bar in session.bars" :key="bar.openMinutes">
          {{ bar.openTime }}-{{ bar.closeTime }}
        </el-text>
      </el-space>
      <el-text v-else>No Trading</el-text>
    </div>
    <div class="el-slider">
      <div class="el-slider__runway is-disabled">
        <div
          v-for="bar in session.bars"
          :key="bar.openMinutes"
          class="el-slider__bar"
          :style="{ width: barWidth(bar), left: barLeft(bar) }"
        ></div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { DayOfWeek } from '@/models/day-of-week';
import { weekday } from '@/plugins/dayjs';
import { useTradingStore } from '@/stores/trading';
import { dayjs } from 'element-plus';

interface Bar {
  openTime: string;
  openMinutes: number;
  closeTime: string;
  closeMinutes: number;
}

const { n } = useI18n();
const { instrument } = storeToRefs(useTradingStore());
const maxMinutes = 24 * 60;

const sessions = computed(() => {
  const result = Object.keys(DayOfWeek)
    .slice(0, 7)
    .map((x) => ({ dayOfWeek: +x, bars: [] as Bar[] }));

  instrument.value.sessions.forEach((session) => {
    const openTimeUtc = dayjs(session.openTime, 'HH:mm:ss').utc(true);
    const openTime = openTimeUtc.local();
    const openMinutes = openTime.hour() * 60 + openTime.minute();
    const closeTimeUtc = dayjs(session.closeTime, 'HH:mm:ss').utc(true);
    const closeTime = closeTimeUtc.local();
    const closeMinutes = closeTime.hour() * 60 + closeTime.minute();
    const openDay =
      (session.openDay + openTime.dayOfYear() - openTimeUtc.dayOfYear()) % 7;
    const closeDay =
      (session.closeDay + closeTime.dayOfYear() - closeTimeUtc.dayOfYear()) % 7;

    if (openDay === closeDay) {
      result[openDay].bars.push({
        openTime: openTime.format('HH:mm'),
        openMinutes,
        closeTime: closeTime.format('HH:mm'),
        closeMinutes,
      });
    } else {
      result[openDay].bars.push({
        openTime: openTime.format('HH:mm'),
        openMinutes,
        closeTime: '24:00',
        closeMinutes: 24 * 60,
      });
      if (closeMinutes !== 0) {
        result[closeDay].bars.push({
          openTime: '00:00',
          openMinutes: 0,
          closeTime: closeTime.format('HH:mm'),
          closeMinutes,
        });
      }
    }
  });

  result.forEach((x) => {
    if (x.bars.length < 2) {
      return;
    }
    x.bars.sort((a, b) => a.openMinutes - b.openMinutes);
    for (let i = x.bars.length - 1; i > 0; i--) {
      if (x.bars[i].openMinutes === x.bars[i - 1].closeMinutes) {
        x.bars[i - 1].closeTime = x.bars[i].closeTime;
        x.bars[i - 1].closeMinutes = x.bars[i].closeMinutes;
        x.bars.splice(i, 1);
      }
    }
  });

  return result;
});

const barWidth = (bar: Bar) =>
  n((bar.closeMinutes - bar.openMinutes) / maxMinutes, 'percent');

const barLeft = (bar: Bar) => n(bar.openMinutes / maxMinutes, 'percent');
</script>

<style lang="scss" scoped>
.session-text {
  display: flex;
  justify-content: space-between;
}

:deep(.el-slider__runway.is-disabled) {
  .el-slider__bar {
    background-color: var(--el-slider-main-bg-color);
    border-radius: var(--el-slider-border-radius);
  }
}
</style>
