export default [
  {
    path: 'organization',
    name: 'organization',
    redirect: { name: 'employee.list' },
    children: [
      {
        path: 'employees',
        name: 'employee.list',
        component: () => import('../views/organization/EmployeeList.vue'),
      },
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
