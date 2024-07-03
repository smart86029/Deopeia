import { symbol } from './props';

export default [
  {
    path: 'symbols/:symbol',
    name: 'symbol.view',
    component: () => import('../views/symbol/SymbolView.vue'),
    redirect: { name: 'symbol.default' },
    props: symbol,
    children: [
      {
        path: '',
        name: 'symbol.default',
        component: () => import('../views/symbol/SymbolOverview.vue'),
        props: symbol,
      },
      {
        path: 'financials',
        name: 'symbol.financials',
        component: () => import('../views/symbol/SymbolFinancials.vue'),
        props: symbol,
      },
      {
        path: 'news',
        name: 'symbol.news',
        component: () => import('../views/symbol/SymbolNews.vue'),
        props: symbol,
      },
    ],
  },
];
