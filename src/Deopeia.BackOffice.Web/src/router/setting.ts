import type { RouteLocationNormalized } from 'vue-router';

export default [
  {
    path: 'setting',
    name: 'setting.module',
    redirect: { name: 'setting.traders.list' },
    children: [
      {
        path: 'contract',
        name: 'setting.contract.list',
        redirect: { name: 'setting.contract.default' },
        children: [
          {
            path: '',
            name: 'setting.contract.default',
            component: () => import('../views/setting/ContractList.vue'),
          },
          {
            path: 'create',
            name: 'setting.contract.create',
            component: () => import('../views/setting/ContractForm.vue'),
            props: () => ({ default: true, action: 'create', symbol: '' }),
          },
          {
            path: ':symbol',
            name: 'setting.contract.edit',
            component: () => import('../views/setting/ContractForm.vue'),
            props: (route: RouteLocationNormalized) => ({
              default: true,
              action: 'edit',
              symbol: route.params.symbol,
            }),
          },
        ],
      },
    ],
  },
];
