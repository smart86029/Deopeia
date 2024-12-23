<template>
  <div class="trading-info">
    <div class="rule">
      Trading Rules

      <div>Quote Currency</div>
      <div>Price Precision</div>
      <div>{{ $t('finance.minimumPriceFluctuation') }}</div>
      <div>{{ $t('finance.contractSize') }}</div>
      <div>Minimum Volume</div>
      <div>Maximum Volume</div>
      <div>Volume Step</div>
      <div>{{ $t('trading.leverage') }}</div>
    </div>
    <div class="sessions">
      Trading Sessions

      <div v-for="item in 7" :key="item">
        week{{ item }}
        <el-slider
          v-model="value"
          range
          disabled
          :max="1440"
          :format-tooltip="time"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { dayjs } from 'element-plus';

const value = ref([4 * 60, 8 * 60]);

const time = (minutes: number) => dayjs(minutes * 60 * 1000).format('HH:mm:ss');
</script>

<style lang="scss" scoped>
.trading-info {
  display: flex;
}

.rule {
  flex: 1;
}

.sessions {
  flex: 1;
}

:deep(.el-slider__runway.is-disabled) {
  .el-slider__bar {
    background-color: var(--el-slider-main-bg-color);
  }

  .el-slider__button {
    border-color: var(--el-slider-main-bg-color);
  }

  .el-slider__button:hover,
  .el-slider__button-wrapper:hover {
    cursor: default;
  }
}
</style>
