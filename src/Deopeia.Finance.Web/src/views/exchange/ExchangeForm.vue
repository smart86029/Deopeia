<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('finance.code')">
      <el-input v-model="form.code" />
    </el-form-item>
    <el-form-item :label="$t('common.timeZone')">
      <el-input v-model="form.timeZone" />
    </el-form-item>
    <el-form-item :label="$t('finance.openingTime')">
      <TimePicker v-model="form.openingTime" />
    </el-form-item>
    <el-form-item :label="$t('finance.closingTime')">
      <TimePicker v-model="form.closingTime" />
    </el-form-item>
    <el-tabs v-model="culture">
      <el-tab-pane
        v-for="(locale, index) in form.locales"
        :key="locale.culture"
        :label="locale.culture"
        :name="locale.culture"
      >
        <el-form-item
          :prop="`locales.${index}.name`"
          :label="$t('common.name')"
        >
          <el-input v-model="form.locales[index].name" />
        </el-form-item>
      </el-tab-pane>
    </el-tabs>
    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import exchangeApi, {
  type Exchange,
  type ExchangeLocale,
} from '@/api/exchange-api';
import { Guid } from '@/models/guid';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const form = reactive({
  id: Guid.empty,
  code: '',
  timeZone: '',
  openingTime: '',
  closingTime: '',
  locales: [] as ExchangeLocale[],
});
const culture = ref('en-US');

if (props.action === 'edit') {
  exchangeApi
    .get(props.id)
    .then((x) => {
      Object.assign(form, x.data);
    })
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? exchangeApi.create : exchangeApi.update;
  post(form as Exchange)
    .then(() => success(props.action))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
