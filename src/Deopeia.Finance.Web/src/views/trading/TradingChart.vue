<template>
  <div id="chart"></div>
</template>

<script setup lang="ts">
import candleApi from '@/api/quote/candle-api';
import type { Candle } from '@/models/quote/candle';
import { TimeFrame } from '@/models/quote/time-frame';
import { useQuoteStore } from '@/stores/quote';
import { dayjs } from 'element-plus';
import { dispose, init, type KLineData } from 'klinecharts';

const { symbol, candles } = storeToRefs(useQuoteStore());

const toKLineData = (candle: Candle): KLineData => ({
  timestamp: +dayjs(candle.timestamp),
  open: candle.open,
  high: candle.high,
  low: candle.low,
  close: candle.close,
  volume: candle.volume,
});

onMounted(async () => {
  const chart = init('chart');
  const history = await candleApi.getHistory(symbol.value, TimeFrame.M1);
  chart?.applyNewData(history.data.map((x) => toKLineData(x)));

  watch(candles.value, (candles) => {
    candles[symbol.value][TimeFrame.M1]
      .map((x) => toKLineData(x))
      .forEach((x) => chart?.updateData(x));
  });
});

onUnmounted(() => {
  dispose('chart');
});
</script>

<style scoped lang="scss">
#chart {
  height: 480px;
  width: 100%;
}
</style>
