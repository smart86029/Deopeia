export default [
  {
    path: '/auth',
    children: [
      {
        path: 'sign-in-callback',
        component: () => import('../views/auth/SignInCallback.vue'),
      },
    ],
  },
];
