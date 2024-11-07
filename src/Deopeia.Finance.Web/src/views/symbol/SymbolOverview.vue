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

const props = defineProps<{
  symbol: string;
}>();

const { locale } = storeToRefs(usePreferencesStore());
const { realTimeQuotes } = storeToRefs(useQuoteStore());

dayjs.extend(utc);

const toUTCTimestamp = (date: Date) =>
  dayjs(date.toLocaleString()).utc(true).unix() as UTCTimestamp;

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
    timeScale: {
      timeVisible: true,
    },
  };
  const chart = createChart('canvas', chartOptions);
  const areaSeries = chart.addAreaSeries({
    lineColor: '#f23645',
    topColor: '#f23645',
    bottomColor: 'rgba(242,	54,	69, 0.28)',
  });

  const history = await candleApi.getHistory(props.symbol);
  const candles = history.data.quotes
    .map((quote) => {
      return {
        time: toUTCTimestamp(quote.date),
        value: quote.open,
      };
    })
    .filter(
      (value, index, array) =>
        array.findIndex((x) => x.time === value.time) === index,
    );
  areaSeries.setData(candles);

  chart.timeScale().fitContent();
  chart.timeScale().scrollToPosition(5, true);

  const time = ref(0);
  watch(realTimeQuotes.value, (quotes) => {
    const data = quotes
      .filter((x) => x.symbol == props.symbol)
      .map((x) => {
        return {
          time: toUTCTimestamp(x.lastTradedAt),
          value: x.lastTradedPrice,
        };
      })
      .filter((x) => x.time > time.value);

    data.forEach((x) => areaSeries.update(x));
    if (data.length > 0) {
      time.value = data[data.length - 1].time;
    }
  });
});
</script>

<style scoped lang="scss">
#canvas {
  height: 300px;
}
</style>
