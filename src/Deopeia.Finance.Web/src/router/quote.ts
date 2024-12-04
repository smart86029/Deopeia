import type { RouteLocationNormalized } from 'vue-router';

export default [
  {
    path: 'exchanges',
    name: 'exchange.list',
    redirect: { name: 'exchange.default' },
    children: [
      {
        path: '',
        name: 'exchange.default',
        component: () => import('../views/quote/ExchangeList.vue'),
      },
      {
        path: 'create',
        name: 'exchange.create',
        component: () => import('../views/quote/ExchangeForm.vue'),
        props: () => ({ default: true, action: 'create', mic: '' }),
      },
      {
        path: ':mic',
        name: 'exchange.edit',
        component: () => import('../views/quote/ExchangeForm.vue'),
        props: (route: RouteLocationNormalized) => ({
          default: true,
          action: 'edit',
          mic: route.params.mic,
        }),
      },
    ],
  },
];
