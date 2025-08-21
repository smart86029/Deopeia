import { createCode, createId, editCode, editId } from './props';

export default [
  {
    path: 'identity',
    name: 'identity.module',
    redirect: { name: 'identity.user.list' },
    children: [
      {
        path: 'users',
        name: 'identity.user.list',
        redirect: { name: 'identity.user.default' },
        children: [
          {
            path: '',
            name: 'identity.user.default',
            component: () => import('../views/identity/UserList.vue'),
          },
          {
            path: 'create',
            name: 'identity.user.create',
            component: () => import('../views/identity/UserForm.vue'),
            props: createId,
          },
          {
            path: ':id',
            name: 'identity.user.edit',
            component: () => import('../views/identity/UserForm.vue'),
            props: editId,
          },
        ],
      },
      {
        path: 'roles',
        name: 'identity.role.list',
        redirect: { name: 'identity.role.default' },
        children: [
          {
            path: '',
            name: 'identity.role.default',
            component: () => import('../views/identity/RoleList.vue'),
          },
          {
            path: 'create',
            name: 'identity.role.create',
            component: () => import('../views/identity/RoleForm.vue'),
            props: createCode,
          },
          {
            path: ':code',
            name: 'identity.role.edit',
            component: () => import('../views/identity/RoleForm.vue'),
            props: editCode,
          },
        ],
      },
      {
        path: 'permissions',
        name: 'identity.permission.list',
        redirect: { name: 'identity.permission.default' },
        children: [
          {
            path: '',
            name: 'identity.permission.default',
            component: () => import('../views/identity/PermissionList.vue'),
          },
          {
            path: 'create',
            name: 'identity.permission.create',
            component: () => import('../views/identity/PermissionForm.vue'),
            props: createCode,
          },
          {
            path: ':code',
            name: 'identity.permission.edit',
            component: () => import('../views/identity/PermissionForm.vue'),
            props: editCode,
          },
        ],
      },
    ],
  },
];
