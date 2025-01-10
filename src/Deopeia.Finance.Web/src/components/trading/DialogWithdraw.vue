<template>
  <el-dialog
    v-model="dialogVisible"
    :title="$t('finance.withdraw')"
    @submit.prevent
  >
    <el-form :model="form" label-width="200">
      <el-form-item :label="$t('common.currency')">
        <SelectOption v-model="form.currencyCode" :options="currencies" />
      </el-form-item>
      <el-form-item :label="$t('finance.amount')">
        <InputNumber v-model="form.amount" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="cancel">{{ $t('operation.cancel') }}</el-button>
      <el-button type="primary" @click="save">
        {{ $t('finance.withdraw') }}
      </el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { traderApi } from '@/api/setting/trader-api';
import { type Guid } from '@/models/guid';
import type { Money } from '@/models/trading/money';
import { useOptionStore } from '@/stores/option';
import { ElMessage } from 'element-plus';
import InputNumber from '../form/InputNumber.vue';

const dialogVisible = defineModel<boolean>();

const props = defineProps<{
  trader: {
    id: Guid;
  };
}>();

const { t } = useI18n();
const loading = ref(false);
const { currencies } = storeToRefs(useOptionStore());
const form: Money = reactive({
  currencyCode: '',
  amount: 0,
});

const cancel = () => (dialogVisible.value = false);

const save = () => {
  loading.value = true;
  traderApi
    .withdraw(props.trader.id, form)
    .then(() => {
      ElMessage.success({
        message: t('common.message.success', { action: t('finance.withdraw') }),
      });
      dialogVisible.value = false;
    })
    .finally(() => (loading.value = false));
};
</script>
