<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('trading.accountNumber')">
      <el-input v-model="form.accountNumber" />
    </el-form-item>
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>
    <el-form-item :label="$t('common.currency')">
      <SelectOption v-model="form.currencyCode" :options="currencies" />
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import optionApi from '@/api/option-api';
import accountApi, { type Account } from '@/api/trading/account-api';
import { emptyGuid, type Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const currencies: Ref<OptionResult<string>[]> = ref([]);
const form: Account = reactive({
  id: emptyGuid,
  accountNumber: '',
  isEnabled: true,
  currencyCode: '',
});

optionApi.getCurrencies().then((x) => (currencies.value = x.data));

if (props.action === 'edit') {
  accountApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? accountApi.create : accountApi.update;
  post(form)
    .then(() => success(props.action))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
