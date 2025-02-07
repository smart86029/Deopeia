export default [
  {
    path: 'asset',
    name: 'asset.view',
    component: () => import('../views/asset/AssetView.vue'),
    redirect: { name: 'asset.overview' },
    children: [
      {
        path: '',
        name: 'asset.overview',
        component: () => import('../views/asset/AssetOverview.vue'),
      },
    ],
  },
];
