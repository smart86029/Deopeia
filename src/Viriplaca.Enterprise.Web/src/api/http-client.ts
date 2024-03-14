import { usePreferencesStore } from '@/stores/preferences';
import axios from 'axios';
import { ElMessageBox, dayjs } from 'element-plus';
import 'element-plus/theme-chalk/index.css';

const instance = axios.create({
  baseURL: '/api',
});

instance.interceptors.request.use(
  (config) => {
    const preferencesStore = usePreferencesStore();
    config.headers.set('Accept-Language', preferencesStore.locale.key);
    return config;
  },
  (error) => Promise.reject(error),
);

instance.interceptors.response.use(
  (response) => {
    switch (response.status) {
      case 200:
        handleDates(response.data.data);
        return response.data;
    }
    return response;
  },
  ({ response }) => {
    switch (response.status) {
      case 400:
      case 500:
        ElMessageBox.close();
        ElMessageBox.alert(response.data.message);
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
