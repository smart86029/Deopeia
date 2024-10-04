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
  {
    path: 'contractSpecification',
    name: 'contractSpecification.list',
    redirect: { name: 'contractSpecification.default' },
    children: [
      {
        path: '',
        name: 'contractSpecification.default',
        component: () => import('../views/quote/ContractSpecificationList.vue'),
      },
      {
        path: 'create',
        name: 'contractSpecification.create',
        component: () => import('../views/quote/ContractSpecificationForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'contractSpecification.edit',
        component: () => import('../views/quote/ContractSpecificationForm.vue'),
        props: edit,
      },
    ],
  },
  {
    path: 'futures',
    name: 'futures.list',
    redirect: { name: 'futures.default' },
    children: [
      {
        path: '',
        name: 'futures.default',
        component: () => import('../views/quote/FuturesList.vue'),
      },
      {
        path: 'create',
        name: 'futures.create',
        component: () => import('../views/quote/FuturesForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'futures.edit',
        component: () => import('../views/quote/FuturesForm.vue'),
        props: edit,
      },
    ],
  },
];
