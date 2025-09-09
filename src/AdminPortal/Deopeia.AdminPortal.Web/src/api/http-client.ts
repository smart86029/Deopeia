import axios from 'axios';
import { ElMessage, ElMessageBox, dayjs } from 'element-plus';
import 'element-plus/theme-chalk/index.css';

import { useAuthStore } from '@/stores/auth';
import { usePreferencesStore } from '@/stores/preferences';

const httpClient = axios.create({
  baseURL: '/api',
});

httpClient.interceptors.request.use(
  async (config) => {
    const preferencesStore = usePreferencesStore();
    config.headers.set('Accept-Language', preferencesStore.locale.key);
    return config;
  },
  (error) => Promise.reject(error),
);

httpClient.interceptors.response.use(
  (response) => {
    switch (response.status) {
      case 200:
        handleDates(response.data);
    }
    return response;
  },
  ({ response }) => {
    const authStore = useAuthStore();
    switch (response.status) {
      case 400:
      case 500:
        ElMessageBox.close();
        if (response.data.title) {
          ElMessageBox.alert(response.data.title?.replace('\r\n', '<br>'), {
            dangerouslyUseHTMLString: true,
          });
        } else if (response.data) {
          ElMessage.error({
            message: response.data,
          });
        }
        return Promise.reject();

      case 401:
        authStore.signIn();
        return Promise.reject();
    }
    return Promise.reject();
  },
);

const handleDates = (data: unknown) => {
  if (!data || typeof data !== 'object') {
    return;
  }

  if (Array.isArray(data)) {
    data.forEach((item) => handleDates(item));
    return;
  }

  const obj = data as Record<string, unknown>;
  for (const [key, value] of Object.entries(obj)) {
    if (typeof value === 'string' && dayjs(value, 'YYYY-MM-DDTHH:mm:ss', true).isValid()) {
      obj[key] = dayjs(value);
    } else if (value && typeof value === 'object') {
      handleDates(value);
    }
  }
};

export default httpClient;
