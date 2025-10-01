import { ElMessageBox } from 'element-plus';

export function useConfirm() {
  const { t } = useI18n();

  const confirmDelete = (itemKey: string, name: string) => {
    ElMessageBox.close();
    return ElMessageBox.confirm(
      t('confirm.delete.message', { name }),
      t('confirm.delete.title', { item: t(itemKey) }),
      {
        type: 'warning',
        confirmButtonText: t('action.delete'),
        confirmButtonClass: 'el-button--danger',
      },
    );
  };

  return {
    confirmDelete,
  };
}
