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
];
