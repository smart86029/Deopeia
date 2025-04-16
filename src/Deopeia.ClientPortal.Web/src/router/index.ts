import { usePreferencesStore } from '@/stores/preferences';
import {
  createRouter,
  createWebHistory,
  type RouteLocationNormalized,
} from 'vue-router';
import HomeView from '../views/HomeView.vue';
import asset from './asset';
import auth from './auth';
import market from './market';
import me from './me';
import quote from './quote';
import trading from './trading';

const guard = async (to: RouteLocationNormalized) => {
  const { locale, locales } = storeToRefs(usePreferencesStore());
  const newLocale = locales.value.find(
    (x) => x.key === (to.params.locale as string),
  );
  if (newLocale) {
    locale.value = newLocale;
    return;
  }

  return { ...to, params: { ...to.params, locale: locale.value.key } };
};

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/:locale?',
      name: 'home',
      beforeEnter: guard,
      component: HomeView,
      redirect: { name: 'dashboard.default' },
      children: [
        {
          path: '',
          name: 'dashboard.default',
          // component: () => import('../views/dashboard/DashboardView.vue'),
        },
        ...asset,
        ...market,
        ...me,
        ...quote,
        ...trading,
      ],
    },
    ...auth,
    { path: '/:pathMatch(.*)*', name: '404', redirect: { name: 'home' } },
  ],
});

export default router;
