export default [
  {
    path: 'me',
    name: 'me.view',
    component: () => import('../views/me/MeView.vue'),
    redirect: { name: 'me.twoFactorAuthentication' },
    children: [
      {
        path: '',
        name: 'me.twoFactorAuthentication',
        component: () => import('../views/me/TwoFactorAuthentication.vue'),
      },
    ],
  },
];
