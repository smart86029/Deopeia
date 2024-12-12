export default [
  {
    path: 'markets',
    name: 'market.list',
    component: () => import('../views/market/MarketView.vue'),
    redirect: { name: 'market.favorite' },
    children: [
      {
        path: 'favorite',
        name: 'market.favorite',
        component: () => import('../views/market/MarketFavorite.vue'),
      },
      {
        path: 'stock',
        name: 'market.stock',
        component: () => import('../views/market/MarketStock.vue'),
      },
      {
        path: 'commodity',
        name: 'market.commodity',
        component: () => import('../views/market/MarketCommodity.vue'),
      },
      {
        path: 'forex',
        name: 'market.forex',
        component: () => import('../views/market/MarketForex.vue'),
      },
      {
        path: 'cryptocurrency',
        name: 'market.cryptocurrency',
        component: () => import('../views/market/MarketCryptocurrency.vue'),
      },
    ],
  },
];
