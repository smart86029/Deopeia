import router from '@/router';
import { ElMessage } from 'element-plus';
import i18n from './i18n';

const { t } = i18n.global;

export const success = (action: 'create' | 'edit', back: boolean) => {
  ElMessage.success({
    message: t(`common.message.${action}Success`),
  });
  if (back) {
    router.go(-1);
  }
};
