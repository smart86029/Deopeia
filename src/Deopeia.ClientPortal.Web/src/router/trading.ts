import { useQuoteStore } from '@/stores/quote';
import { useTradingStore } from '@/stores/trading';
import type { RouteLocationNormalized } from 'vue-router';

const guard = async (to: RouteLocationNormalized) => {
  const { symbol } = storeToRefs(useQuoteStore());
  if (!to.params.symbol) {
    return { ...to, params: { ...to.params, symbol: symbol.value } };
  }

  useTradingStore().getInstrument(symbol.value);
};

export default [
  {
    path: 'trading/:symbol?',
    name: 'trading.view',
    beforeEnter: guard,
    component: () => import('../views/trading/TradingView.vue'),
    redirect: { name: 'trading.chart' },
    children: [
      {
        path: '',
        name: 'trading.chart',
        component: () => import('../views/trading/TradingChart.vue'),
      },
      {
        path: 'info',
        name: 'trading.info',
        component: () => import('../views/trading/TradingInfo.vue'),
      },
    ],
  },
];
