export default [
  {
    path: 'asset',
    name: 'asset.view',
    component: () => import('../views/asset/AssetView.vue'),
    redirect: { name: 'asset.account' },
    children: [
      {
        path: 'account',
        name: 'asset.account',
        component: () => import('../views/asset/AccountList.vue'),
      },
      {
        path: 'position',
        name: 'asset.position',
        component: () => import('../views/asset/PositionList.vue'),
      },
    ],
  },
];
