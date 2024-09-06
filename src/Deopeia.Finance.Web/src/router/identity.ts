import { create, edit } from './props';

export default [
  {
    path: 'users',
    name: 'user.list',
    redirect: { name: 'user.default' },
    children: [
      {
        path: '',
        name: 'user.default',
        component: () => import('../views/identity/UserList.vue'),
      },
      {
        path: 'create',
        name: 'user.create',
        component: () => import('../views/identity/UserForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'user.edit',
        component: () => import('../views/identity/UserForm.vue'),
        props: edit,
      },
    ],
  },
];
