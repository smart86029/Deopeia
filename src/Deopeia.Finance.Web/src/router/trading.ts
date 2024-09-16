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
        component: () => import('../views/trading/AssetList.vue'),
      },
      {
        path: 'create',
        name: 'asset.create',
        component: () => import('../views/trading/AssetForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'asset.edit',
        component: () => import('../views/trading/AssetForm.vue'),
        props: edit,
      },
    ],
  },
  {
    path: 'strategies',
    name: 'strategy.list',
    redirect: { name: 'strategy.default' },
    children: [
      {
        path: '',
        name: 'strategy.default',
        component: () => import('../views/trading/StrategyList.vue'),
      },
      {
        path: 'create',
        name: 'strategy.create',
        component: () => import('../views/trading/StrategyForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'strategy.edit',
        component: () => import('../views/trading/StrategyForm.vue'),
        props: edit,
      },
    ],
  },
];
