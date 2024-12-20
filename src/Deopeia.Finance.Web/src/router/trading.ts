import { symbol } from './props';

export default [
  {
    path: 'trading/:symbol',
    name: 'trading.view',
    component: () => import('../views/trading/TradingView.vue'),
    redirect: { name: 'trading.chart' },
    props: symbol,
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
