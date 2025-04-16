export default [
  {
    path: '/auth',
    children: [
      {
        path: 'callback',
        component: () => import('../views/auth/SignInCallback.vue'),
      },
      {
        path: 'silent-refresh',
        component: () => import('../views/auth/SilentRefresh.vue'),
      },
    ],
  },
];
