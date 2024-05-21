import { edit } from './props';

export default [
  {
    path: 'leave-management',
    name: 'leave.manage',
    redirect: { name: 'leave.default' },
    children: [
      {
        path: 'leave-entitlement',
        name: 'leave.entitlement',
        component: () => import('../views/leave/LeaveEntitlementList.vue'),
      },
      {
        path: 'leaves',
        name: 'leave.list',
        children: [
          {
            path: '',
            name: 'leave.default',
            component: () => import('../views/leave/LeaveList.vue'),
          },
          {
            path: 'apply',
            name: 'leave.apply',
            component: () => import('../views/leave/LeaveForm.vue'),
          },
          {
            path: ':id/approval',
            name: 'leave.approval',
            component: () => import('../views/leave/LeaveApproval.vue'),
            props: edit,
          },
        ],
      },
      {
        path: 'leave-types',
        name: 'leave.type.list',
        children: [
          {
            path: '',
            name: 'leave.type.default',
            component: () => import('../views/leave/LeaveTypeList.vue'),
          },
          {
            path: 'create',
            name: 'leave.type.create',
            component: () => import('../views/leave/LeaveTypeForm.vue'),
          },
          {
            path: ':id',
            name: 'leave.type.edit',
            component: () => import('../views/leave/LeaveTypeForm.vue'),
            props: edit,
          },
        ],
      },
    ],
  },
];
