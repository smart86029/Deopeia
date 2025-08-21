import router from '@/router';
import type { MessageOptions } from 'element-plus';
import { ElMessage } from 'element-plus';
import i18n from './i18n';

const { t } = i18n.global;

// Type-safe action types
export type ActionType = 'create' | 'edit' | 'delete';

// Enhanced success message handler with better error handling
export const success = (action: ActionType): void => {
  try {
    const messageOptions: MessageOptions = {
      message: t(`common.message.${action}Success`),
      type: 'success',
      duration: 3000,
    };

    ElMessage.success(messageOptions);

    // Navigate back with error handling
    if (router.currentRoute.value.query.from) {
      // If there's a 'from' query parameter, use it for navigation
      router.push(router.currentRoute.value.query.from as string);
    } else {
      // Fallback to going back in history
      router.go(-1);
    }
  } catch (error) {
    // Fallback error handling
    console.error('Error in success handler:', error);
    ElMessage.error(t('common.message.unexpectedError'));
  }
};

// Enhanced error message handler
export const error = (message?: string): void => {
  try {
    const errorMessage = message || t('common.message.operationFailed');
    const messageOptions: MessageOptions = {
      message: errorMessage,
      type: 'error',
      duration: 5000,
    };

    ElMessage.error(messageOptions);
  } catch (err) {
    // Fallback to console if ElMessage fails
    console.error('Error in error handler:', err);
    console.error('Original error message:', message);
  }
};

// Warning message handler
export const warning = (message: string): void => {
  try {
    const messageOptions: MessageOptions = {
      message,
      type: 'warning',
      duration: 4000,
    };

    ElMessage.warning(messageOptions);
  } catch (err) {
    console.error('Error in warning handler:', err);
  }
};
