<template>
  <div class="trading-info">
    <el-card>
      <template #header>Trading Rules</template>
      <el-config-provider size="large">
        <div class="rules">
          <div class="rule">
            <el-text type="info">Quote Currency</el-text>
            <el-text>{{ instrument.currencyCode }}</el-text>
          </div>
          <div class="rule">
            <el-text type="info">Price Precision</el-text>
            <TextNumber :value="instrument.pricePrecision" />
          </div>
          <div class="rule">
            <el-text type="info">
              {{ $t('finance.minimumPriceFluctuation') }}
            </el-text>
            <TextNumber :value="instrument.tickSize" />
          </div>
          <div class="rule">
            <el-text type="info">{{ $t('finance.contractSize') }}</el-text>
            <el-text>
              <TextNumber :value="instrument.contractSize.quantity" />
              {{
                units.find((x) => x.value === instrument.contractSize.unitCode)
                  ?.name
              }}
            </el-text>
          </div>
          <div class="rule">
            <el-text type="info">Minimum Volume</el-text>
            <TextNumber :value="instrument.volumeRestriction.min" />
          </div>
          <div class="rule">
            <el-text type="info">Maximum Volume</el-text>
            <TextNumber :value="instrument.volumeRestriction.max" />
          </div>
          <div class="rule">
            <el-text type="info">Volume Step</el-text>
            <TextNumber :value="instrument.volumeRestriction.step" />
          </div>
          <div class="rule">
            <el-text type="info">{{ $t('trading.leverage') }}</el-text>
            <TextNumber :value="instrument.leverages.findLast((x) => x > 0)" />
          </div>
        </div>
      </el-config-provider>
    </el-card>
    <el-card>
      <template #header>Trading Sessions</template>
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
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { useOptionStore } from '@/stores/option';
import { useTradingStore } from '@/stores/trading';
import { dayjs } from 'element-plus';

const { units } = storeToRefs(useOptionStore());
const { instrument } = storeToRefs(useTradingStore());
const value = ref([4 * 60, 8 * 60]);

const time = (minutes: number) => dayjs(minutes * 60 * 1000).format('HH:mm:ss');
</script>

<style lang="scss" scoped>
.trading-info {
  display: flex;
  gap: 16px;
}

.el-card {
  flex: 1;
}

.rules {
  display: flex;
  flex-direction: column;
}

.rule {
  display: flex;
  justify-content: space-between;
  line-height: 1.5lh;
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
