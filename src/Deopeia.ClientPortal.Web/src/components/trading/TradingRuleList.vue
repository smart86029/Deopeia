<template>
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
</template>

<script setup lang="ts">
import { useOptionStore } from '@/stores/option';
import { useTradingStore } from '@/stores/trading';

const { currencies, units } = storeToRefs(useOptionStore());
const { instrument } = storeToRefs(useTradingStore());
</script>

<style lang="scss" scoped>
.rules {
  display: flex;
  flex-direction: column;
}

.rule {
  display: flex;
  justify-content: space-between;
  line-height: 1.5lh;
}
</style>
