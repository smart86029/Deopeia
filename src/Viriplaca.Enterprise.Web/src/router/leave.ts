export default [
  {
    path: 'leave-management',
    name: 'leave.manage',
    redirect: { name: 'leave.default' },
    children: [
      {
        path: 'leaves',
        name: 'leave.list',
        children: [
          {
            path: '',
            name: 'leave.default',
            component: () => import('../views/leave/LeaveList.vue'),
          },
          {
            path: 'apply',
            name: 'leave.apply',
            component: () => import('../views/leave/LeaveForm.vue'),
          },
        ],
      },
    ],
  },
];
