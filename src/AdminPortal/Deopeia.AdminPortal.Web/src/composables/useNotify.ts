import router from '@/router';
import { ElMessage } from 'element-plus';

export type ActionType = 'create' | 'edit' | 'delete';

export function useNotify() {
  const { t } = useI18n();

  const success = (action: ActionType): void => {
    ElMessage.success({
      message: t(`common.message.${action}Success`),
      type: 'success',
      duration: 3000,
    });

    if (action === 'delete') {
      return;
    }

    if (router.currentRoute.value.query.from) {
      router.push(router.currentRoute.value.query.from as string);
    } else {
      router.go(-1);
    }
  };

  return {
    success,
  };
}
