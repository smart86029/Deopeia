import router from '@/router';
// import { useAuthStore } from '@/stores/auth';
import axios from 'axios';
import { ElMessage, ElMessageBox, dayjs } from 'element-plus';
import 'element-plus/theme-chalk/index.css';

const instance = axios.create({
  baseURL: '/api',
});

instance.interceptors.request.use(
  (config) => {
    return config;
  },
  (error) => Promise.reject(error),
);

instance.interceptors.response.use(
  (response) => {
    // const authStore = useAuthStore();
    switch (response.data.status) {
      case 1:
        handleDates(response.data.data);
        return response.data;
      case 2:
        ElMessageBox.close();
        ElMessageBox.alert(response.data.message);
        return Promise.reject();
      case 3:
        ElMessageBox.close();
        ElMessage.closeAll();
        // authStore.initInfo();
        router.replace({ name: 'auth.signIn' });
        return Promise.reject();
      case 500:
        ElMessageBox.close();
        ElMessageBox.alert(response.data.message);
        return Promise.reject();
    }
    return response;
  },
  (error) => Promise.reject(error),
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
