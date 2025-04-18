export default [
  {
    path: 'fund',
    name: 'fund.module',
    redirect: { name: 'fund.deposit.list' },
    children: [
      {
        path: 'deposits',
        name: 'fund.deposit.list',
        redirect: { name: 'fund.deposit.default' },
        children: [
          {
            path: '',
            name: 'fund.deposit.default',
            component: () => import('../views/fund/DepositList.vue'),
          },
        ],
      },
      {
        path: 'withdrawals',
        name: 'fund.withdrawal.list',
        redirect: { name: 'fund.withdrawal.default' },
        children: [
          {
            path: '',
            name: 'fund.withdrawal.default',
            component: () => import('../views/fund/WithdrawalList.vue'),
          },
        ],
      },
    ],
  },
];
