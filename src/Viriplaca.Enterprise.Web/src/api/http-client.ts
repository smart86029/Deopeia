import { useAuthStore } from '@/stores/auth';
import { usePreferencesStore } from '@/stores/preferences';
import axios from 'axios';
import { ElMessageBox, dayjs } from 'element-plus';
import 'element-plus/theme-chalk/index.css';
import i18n from '../plugins/i18n';

const { t } = i18n.global;
const instance = axios.create({
  baseURL: '/api',
});

instance.interceptors.request.use(
  async (config) => {
    const preferencesStore = usePreferencesStore();
    config.headers.set('Accept-Language', preferencesStore.locale.key);

    const authStore = useAuthStore();
    const user = await authStore.getUser();
    if (user?.access_token) {
      config.headers.setAuthorization(`Bearer ${user.access_token}`);
    } else if (user) {
      await authStore.refresh().then((user) => {
        config.headers.setAuthorization(`Bearer ${user!.access_token}`);
      });
    }

    return config;
  },
  (error) => Promise.reject(error),
);

instance.interceptors.response.use(
  (response) => {
    switch (response.status) {
      case 200:
        handleDates(response.data);
    }
    return response;
  },
  ({ response }) => {
    switch (response.status) {
      case 400:
      case 500:
        ElMessageBox.close();
        ElMessageBox.alert(response.data.title || t('common.message.error'));
        return Promise.reject();
    }
    return Promise.reject();
  },
);

const handleDates = (body: any) => {
  if (body === null || body === undefined || typeof body !== 'object') {
    return body;
  }

  for (const key of Object.keys(body)) {
    const value = body[key];
    if (dayjs(value, 'YYYY-MM-DDTHH:mm:ss', true).isValid()) {
      body[key] = dayjs(value);
    } else if (typeof value === 'object') {
      handleDates(value);
    }
  }
};

export default instance;
