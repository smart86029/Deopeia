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
  {
    path: 'roles',
    name: 'role.list',
    redirect: { name: 'role.default' },
    children: [
      {
        path: '',
        name: 'role.default',
        component: () => import('../views/identity/RoleList.vue'),
      },
      {
        path: 'create',
        name: 'role.create',
        component: () => import('../views/identity/RoleForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'role.edit',
        component: () => import('../views/identity/RoleForm.vue'),
        props: edit,
      },
    ],
  },
  {
    path: 'permissions',
    name: 'permission.list',
    redirect: { name: 'permission.default' },
    children: [
      {
        path: '',
        name: 'permission.default',
        component: () => import('../views/identity/PermissionList.vue'),
      },
      {
        path: 'create',
        name: 'permission.create',
        component: () => import('../views/identity/PermissionForm.vue'),
        props: create,
      },
      {
        path: ':id',
        name: 'permission.edit',
        component: () => import('../views/identity/PermissionForm.vue'),
        props: edit,
      },
    ],
  },
];
