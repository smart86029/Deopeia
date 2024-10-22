import { create, edit } from './props';

export default [
  {
    path: 'accounts',
    name: 'account.list',
    redirect: { name: 'account.default' },
    children: [
      {
        path: '',
        name: 'account.default',
        component: () => import('../views/trading/AccountList.vue'),
      },
      {
        path: 'create',
        name: 'account.create',
        component: () => import('../views/trading/AccountForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'account.edit',
        component: () => import('../views/trading/AccountForm.vue'),
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
  {
    path: 'positions',
    name: 'position.list',
    redirect: { name: 'position.default' },
    children: [
      {
        path: '',
        name: 'position.default',
        component: () => import('../views/trading/PositionList.vue'),
      },
      {
        path: ':id/close',
        name: 'position.close',
        component: () => import('../views/trading/PositionClose.vue'),
        props: edit,
      },
    ],
  },
];
