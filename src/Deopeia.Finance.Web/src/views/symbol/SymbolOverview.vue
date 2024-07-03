<template>
  <div id="canvas"></div>
</template>

<script setup lang="ts">
import ohlcvApi from '@/api/ohlcv-api';
import type { Ohlcv } from '@/models/quote/ohlcv';
import { createChart } from 'lightweight-charts';

const props = defineProps<{
  symbol: string;
}>();

const ohlcvs = ref([] as Ohlcv[]);

onMounted(async () => {
  const color = useCssVar('--el-bg-color-overlay', ref(null)).value;
  const chartOptions = {
    layout: {
      textColor: 'white',
      background: { type: 'solid', color: color },
    },
  };
  const chart = createChart('canvas', chartOptions);
  // const areaSeries = chart.addAreaSeries({
  //   lineColor: '#2962FF',
  //   topColor: '#2962FF',
  //   bottomColor: 'rgba(41, 98, 255, 0.28)',
  // });
  // areaSeries.setData([
  //   { time: '2018-12-22', value: 32.51 },
  //   { time: '2018-12-23', value: 31.11 },
  //   { time: '2018-12-24', value: 27.02 },
  //   { time: '2018-12-25', value: 27.32 },
  //   { time: '2018-12-26', value: 25.17 },
  //   { time: '2018-12-27', value: 28.89 },
  //   { time: '2018-12-28', value: 25.46 },
  //   { time: '2018-12-29', value: 23.92 },
  //   { time: '2018-12-30', value: 22.68 },
  //   { time: '2018-12-31', value: 22.67 },
  // ]);

  const candlestickSeries = chart.addCandlestickSeries({
    upColor: useCssVar('--el-color-error', ref(null)).value,
    downColor: useCssVar('--el-color-success', ref(null)).value,
    borderVisible: false,
    wickUpColor: useCssVar('--el-color-error', ref(null)).value,
    wickDownColor: useCssVar('--el-color-success', ref(null)).value,
  });

  const aa = await ohlcvApi.getHistory(props.symbol);
  ohlcvs.value = aa.data.quotes.map((quote) => {
    return {
      time: quote.date,
      open: quote.open,
      high: quote.high,
      low: quote.low,
      close: quote.close,
    };
  });

  candlestickSeries.setData(ohlcvs.value);
  chart.timeScale().fitContent();
});

// console.log(props.ohlcvs);

// watch(props.ohlcvs, (ohlcvs) => {
//   console.log(1);
//   if (chart === undefined || candlestickSeries === undefined) {
//     return;
//   }

//   candlestickSeries.update(ohlcvs);
// });
</script>

<style scoped lang="scss">
#canvas {
  width: 1000px;
  height: 500px;
}
</style>
