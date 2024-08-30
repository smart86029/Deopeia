<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('finance.marketIdentifierCode')">
      <el-input v-model="form.mic" />
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
    <LocaleTabs
      v-model="culture"
      labelWidth="200px"
      @update="addLocale"
      @tab-remove="removeLocale"
    >
      <el-tab-pane
        v-for="(locale, index) in form.locales"
        :key="locale.culture"
        :label="locale.culture"
        :name="locale.culture"
      >
        <el-form-item :label="$t('common.name')">
          <el-input v-model="form.locales[index].name" />
        </el-form-item>
      </el-tab-pane>
    </LocaleTabs>
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
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  mic: string;
}>();
const loading = ref(false);
const form = reactive({
  mic: '',
  timeZone: '',
  openingTime: '',
  closingTime: '',
  locales: [] as ExchangeLocale[],
});
const culture = ref('en-US');

if (props.action === 'edit') {
  exchangeApi
    .get(props.mic)
    .then((x) => {
      Object.assign(form, x.data);
    })
    .finally(() => (loading.value = false));
}

const addLocale = (locale: string) => {
  if (form.locales.findIndex((x) => x.culture === locale) < 0) {
    form.locales.push({ culture: locale, name: '' });
    culture.value = locale;
  }
};

const removeLocale = (locale: string) => {
  const index = form.locales.findIndex((x) => x.culture === locale);
  form.locales.splice(index, 1);
  culture.value = form.locales.length > 0 ? form.locales[0].culture : '';
};

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
