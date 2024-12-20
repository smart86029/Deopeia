<template>
  <div class="quote">
    <div class="item">
      <span class="name">{{ instrument.name }}</span>
      <el-text type="info" class="symbol">{{ symbol }}</el-text>
    </div>

    <div class="item">
      <TextPrice :value="lastTradedPrice" />
      <TextPrice :value="priceChange" />
    </div>
    <div class="item">
      <el-text type="info" size="small">
        {{ $t('finance.priceChange') }}
      </el-text>
      <TextPrice :value="priceRateOfChange" percentage />
    </div>
  </div>
</template>

<script setup lang="ts">
import { useQuoteStore } from '@/stores/quote';
import { useTradingStore } from '@/stores/trading';

const { symbol, lastTradedPrice, priceChange, priceRateOfChange } =
  storeToRefs(useQuoteStore());
const { instrument } = storeToRefs(useTradingStore());
</script>

<style lang="scss" scoped>
.quote {
  display: flex;
  gap: 10px;
  align-items: center;

  :first-child {
    align-items: unset;
    width: 30%;
  }
}

.item {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 20%;
}

.name {
  font-size: var(--el-font-size-extra-large);
}

.symbol {
  text-align: left;
  align-self: start;
}
</style>
