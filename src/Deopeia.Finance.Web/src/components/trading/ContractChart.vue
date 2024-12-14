<template>
  <div id="canvas"></div>
</template>

<script setup lang="ts">
import candleApi from '@/api/quote/candle-api';
import { usePreferencesStore } from '@/stores/preferences';
import { useQuoteStore } from '@/stores/quote';
import utc from 'dayjs/plugin/utc';
import { dayjs } from 'element-plus';
import { ColorType, createChart, type UTCTimestamp } from 'lightweight-charts';

const { symbol, candles } = storeToRefs(useQuoteStore());
const { locale, positive, negative } = storeToRefs(usePreferencesStore());

dayjs.extend(utc);

const toUTCTimestamp = (date: Date) =>
  dayjs(date.toLocaleString()).utc(true).unix() as UTCTimestamp;

onMounted(async () => {
  const color = useCssVar('--el-bg-color-overlay').value;
  const chartOptions = {
    layout: {
      textColor: 'white',
      background: { type: ColorType.Solid, color: color },
    },
    localization: {
      locale: locale.value.key,
    },
    timeScale: {
      timeVisible: true,
    },
  };
  const chart = createChart('canvas', chartOptions);
  const candlestickSeries = chart.addCandlestickSeries({
    upColor: useCssVar(`--el-color-${positive.value}`).value,
    downColor: useCssVar(`--el-color-${negative.value}`).value,
    borderVisible: false,
  });

  const history = await candleApi.getHistory(symbol.value);
  const data = history.data.quotes
    .map((quote) => ({
      time: toUTCTimestamp(quote.date),
      open: quote.open,
      high: quote.high,
      low: quote.low,
      close: quote.close,
    }))
    .filter(
      (value, index, array) =>
        array.findIndex((x) => x.time === value.time) === index,
    );
  candlestickSeries.setData(data);

  chart.timeScale().fitContent();
  chart.timeScale().scrollToPosition(5, true);

  const time = ref(0);
  const key: [string, number] = [symbol.value, 0];
  watch(candles.value.get(key)!, (quotes) => {
    const data = quotes
      .map((x) => ({
        time: x.time,
        open: x.open,
        high: x.high,
        low: x.low,
        close: x.close,
        volume: x.volume,
      }))
      .filter((x) => x.time > time.value);

    data.forEach((x) => candlestickSeries.update(x));
    if (data.length > 0) {
      time.value = data[data.length - 1].time;
    }
  });
});
</script>

<style scoped lang="scss">
#canvas {
  height: 480px;
}
</style>
