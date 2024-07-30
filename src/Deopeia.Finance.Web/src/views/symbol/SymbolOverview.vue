<template>
  <div id="canvas"></div>
</template>

<script setup lang="ts">
import type { Ohlcv } from '@/models/quote/ohlcv';
import { usePreferencesStore } from '@/stores/preferences';
import { useQuoteStore } from '@/stores/quote';
import { dayjs } from 'element-plus';
import { ColorType, createChart } from 'lightweight-charts';

const props = defineProps<{
  symbol: string;
}>();

const { locale } = storeToRefs(usePreferencesStore());
const { realTimeQuotes } = storeToRefs(useQuoteStore());
const ohlcvs = ref([] as Ohlcv[]);

onMounted(async () => {
  const color = useCssVar('--el-bg-color-overlay', ref(null)).value;
  const chartOptions = {
    layout: {
      textColor: 'white',
      background: { type: ColorType.Solid, color: color },
    },
    localization: {
      locale: locale.value.key,
    },
  };
  const chart = createChart('canvas', chartOptions);
  const areaSeries = chart.addAreaSeries({
    lineColor: '#2962FF',
    topColor: '#2962FF',
    bottomColor: 'rgba(41, 98, 255, 0.28)',
  });

  // const candlestickSeries = chart.addCandlestickSeries({
  //   upColor: useCssVar('--el-color-error', ref(null)).value,
  //   downColor: useCssVar('--el-color-success', ref(null)).value,
  //   borderVisible: false,
  //   wickUpColor: useCssVar('--el-color-error', ref(null)).value,
  //   wickDownColor: useCssVar('--el-color-success', ref(null)).value,
  // });

  // const aa = await ohlcvApi.getHistory(props.symbol);
  // ohlcvs.value = aa.data.quotes.map((quote) => {
  //   return {
  //     time: dayjs(quote.date).unix(),
  //     open: quote.open,
  //     high: quote.high,
  //     low: quote.low,
  //     close: quote.close,
  //   };
  // });

  // candlestickSeries.setData(ohlcvs.value);
  chart.timeScale().fitContent();

  watch(realTimeQuotes.value, (quotes) => {
    const data = quotes
      .filter((x) => x.symbol == props.symbol)
      .map((x) => {
        return {
          value: x.lastTradedPrice,
          time: dayjs(x.lastTradedAt).unix(),
        };
      });

    data.forEach((x) => areaSeries.update(x));
  });
});
</script>

<style scoped lang="scss">
#canvas {
  height: 300px;
}
</style>
