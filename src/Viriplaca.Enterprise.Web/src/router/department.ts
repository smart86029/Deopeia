export default [
  {
    path: 'departments',
    name: 'department.manage',
    redirect: { name: '' },
    children: [
      {
        path: '',
        name: 'department.list',
        component: () => import('../views/department/DepartmentList.vue'),
      },
    ],
  },
];
