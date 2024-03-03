import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import leave from './leave';
import department from './department';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      children: [...department, ...leave],
    },
  ],
});

export default router;
