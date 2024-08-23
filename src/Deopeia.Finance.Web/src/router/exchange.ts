import { create, edit } from './props';

export default [
  {
    path: 'exchanges',
    name: 'exchange.list',
    redirect: { name: 'exchange.default' },
    children: [
      {
        path: '',
        name: 'exchange.default',
        component: () => import('../views/exchange/ExchangeList.vue'),
      },
      {
        path: 'create',
        name: 'exchange.create',
        component: () => import('../views/exchange/ExchangeForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'exchange.edit',
        component: () => import('../views/exchange/ExchangeForm.vue'),
        props: edit,
      },
    ],
  },
];
