export default [
  {
    path: 'leaves',
    name: 'leave.manage',
    redirect: { name: '' },
    children: [
      {
        path: '',
        name: 'leave.list',
        component: () => import('../views/leave/LeaveList.vue'),
      },
      {
        path: 'apply',
        name: 'leave.apply',
        component: () => import('../views/leave/LeaveForm.vue'),
      },
    ],
  },
];
