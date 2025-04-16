import router from '@/router';
import { ElMessage } from 'element-plus';
import i18n from './i18n';

const { t } = i18n.global;

export const success = (action: 'create' | 'edit') => {
  ElMessage.success({
    message: t(`common.message.${action}Success`),
  });
  router.go(-1);
};
