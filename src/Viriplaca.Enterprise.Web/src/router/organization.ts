export default [
  {
    path: 'organization',
    name: 'organization',
    redirect: { name: 'department.list' },
    children: [
      {
        path: 'departments',
        name: 'department.list',
        component: () => import('../views/organization/DepartmentList.vue'),
      },
      {
        path: 'jobs',
        name: 'job.list',
        component: () => import('../views/organization/JobList.vue'),
      },
    ],
  },
];
