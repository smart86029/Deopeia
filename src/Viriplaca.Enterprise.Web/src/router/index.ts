import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      children: [
        {
          path: 'leave',
          name: 'leave.manage',
          redirect: { name: '' },
          children: [
            {
              path: '',
              name: 'leave.list',
              component: () => import('../views/leave/LeaveList.vue'),
            },
            {
              path: 'apply',
              name: 'leave.apply',
              component: () => import('../views/leave/LeaveForm.vue'),
            },
          ],
        },
      ],
    },
  ],
});

export default router;
