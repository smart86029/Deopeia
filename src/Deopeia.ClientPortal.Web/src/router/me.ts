export default [
  {
    path: 'me',
    name: 'me.view',
    component: () => import('../views/me/MeView.vue'),
    redirect: { name: 'me.profile' },
    children: [
      {
        path: 'profile',
        name: 'me.profile',
        component: () => import('../views/me/ProfilePage.vue'),
      },
      {
        path: 'password',
        name: 'me.password',
        component: () => import('../views/me/ChangePassword.vue'),
      },
      {
        path: '2fa',
        name: 'me.twoFactorAuthentication',
        component: () => import('../views/me/TwoFactorAuthentication.vue'),
      },
    ],
  },
];
