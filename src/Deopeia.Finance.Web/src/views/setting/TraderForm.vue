<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('common.name')">
      <el-input v-model="form.name" />
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
import { traderApi, type Trader } from '@/api/setting/trader-api';
import { emptyGuid, type Guid } from '@/models/guid';
import { success } from '@/plugins/element';
import { useOptionStore } from '@/stores/option';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const { currencies } = storeToRefs(useOptionStore());
const form: Trader = reactive({
  id: emptyGuid,
  name: '',
  isEnabled: true,
  currencyCode: '',
});

if (props.action === 'edit') {
  traderApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post = props.action === 'create' ? traderApi.create : traderApi.update;
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
