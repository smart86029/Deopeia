import { editId } from './props';

export default [
  {
    path: 'trading',
    name: 'trading.module',
    redirect: { name: 'trading.position.list' },
    children: [
      {
        path: 'positions',
        name: 'trading.position.list',
        redirect: { name: 'trading.position.default' },
        children: [
          {
            path: '',
            name: 'trading.position.default',
            component: () => import('../views/trading/PositionList.vue'),
          },
          {
            path: ':id/close',
            name: 'trading.position.close',
            component: () => import('../views/trading/PositionClose.vue'),
            props: editId,
          },
        ],
      },
    ],
  },
];
