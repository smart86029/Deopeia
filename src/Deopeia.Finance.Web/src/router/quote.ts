import type { RouteLocationNormalized } from 'vue-router';
import { create, edit } from './props';

export default [
  {
    path: 'assets',
    name: 'asset.list',
    redirect: { name: 'asset.default' },
    children: [
      {
        path: '',
        name: 'asset.default',
        component: () => import('../views/quote/AssetList.vue'),
      },
      {
        path: 'create',
        name: 'asset.create',
        component: () => import('../views/quote/AssetForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'asset.edit',
        component: () => import('../views/quote/AssetForm.vue'),
        props: edit,
      },
    ],
  },
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
