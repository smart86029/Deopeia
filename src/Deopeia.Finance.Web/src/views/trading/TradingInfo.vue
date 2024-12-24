<template>
  <div class="trading-info">
    <el-card>
      <template #header>Trading Rules</template>
      <el-config-provider size="large">
        <div class="rules">
          <div class="rule">
            <el-text type="info">Quote Currency</el-text>
            <el-text>{{
              currencies.find((x) => x.value === instrument.currencyCode)?.name
            }}</el-text>
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
      <div v-for="session in sessions" :key="session.dayOfWeek">
        <div class="session-text">
          <el-text type="info">{{ DayOfWeek[session.dayOfWeek] }}</el-text>
          <el-text v-if="session.hasValue">
            {{ session.openTime }} - {{ session.closeTime }}
          </el-text>
          <el-text v-else>No Trading</el-text>
        </div>
        <el-slider v-model="session.values" range disabled :max="1440" />
      </div>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { DayOfWeek } from '@/models/day-of-week';
import { useOptionStore } from '@/stores/option';
import { useTradingStore } from '@/stores/trading';
import utc from 'dayjs/plugin/utc';
import { dayjs } from 'element-plus';

const { currencies, units } = storeToRefs(useOptionStore());
const { instrument } = storeToRefs(useTradingStore());

dayjs.extend(utc);

const sessions = computed(() =>
  Object.keys(DayOfWeek)
    .slice(0, 7)
    .map((x) => {
      const session = instrument.value.sessions?.find(
        (y) => y.dayOfWeek === +x,
      );
      if (session === undefined) {
        return { dayOfWeek: +x, values: [0, 0] };
      }

      const openTime = dayjs(session.openTime, 'HH:mm:ss').utc(true);
      const openMinutes = openTime.hour() * 60 + openTime.minute();
      const closeTime = dayjs(session.closeTime, 'HH:mm:ss').utc(true);
      const closeMinutes = closeTime.hour() * 60 + closeTime.minute();
      return {
        dayOfWeek: +x,
        values: [openMinutes, closeMinutes],
        openTime: session.openTime,
        closeTime: session.closeTime,
        hasValue: true,
      };
    }),
);
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

.session-text {
  display: flex;
  justify-content: space-between;
}

:deep(.el-slider__runway.is-disabled) {
  .el-slider__bar {
    background-color: var(--el-slider-main-bg-color);
  }

  .el-slider__button,
  .el-slider__button-wrapper {
    display: none;
  }
}
</style>
