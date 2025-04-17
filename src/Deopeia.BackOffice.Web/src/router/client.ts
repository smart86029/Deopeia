import { createId, editId } from './props';

export default [
  {
    path: 'client',
    name: 'client.module',
    redirect: { name: 'client.traders.list' },
    children: [
      {
        path: 'traders',
        name: 'client.trader.list',
        redirect: { name: 'client.trader.default' },
        children: [
          {
            path: '',
            name: 'client.trader.default',
            component: () => import('../views/client/TraderList.vue'),
          },
          {
            path: 'create',
            name: 'client.trader.create',
            component: () => import('../views/client/TraderForm.vue'),
            props: createId,
          },
          {
            path: ':id',
            name: 'client.trader.edit',
            component: () => import('../views/client/TraderForm.vue'),
            props: editId,
          },
        ],
      },
    ],
  },
];
