<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('finance.marketIdentifierCode')">
      <el-input v-model="form.mic" />
    </el-form-item>
    <el-form-item :label="$t('common.timeZone')">
      <SelectOption v-model="form.timeZone" :options="timeZones" />
    </el-form-item>

    <LocaleTabs v-model:locales="form.locales" :add="add">
      <el-tab-pane
        v-for="(locale, index) in form.locales"
        :key="locale.culture"
        :label="cultures.find((x) => x.value === locale.culture)?.name"
        :name="locale.culture"
      >
        <el-form-item :label="$t('common.name')">
          <el-input v-model="form.locales[index].name" />
        </el-form-item>
        <el-form-item :label="$t('common.abbreviation')">
          <el-input v-model="form.locales[index].abbreviation" />
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
} from '@/api/quote/exchange-api';
import { success } from '@/plugins/element';
import { usePreferencesStore } from '@/stores/preferences';

const props = defineProps<{
  action: 'create' | 'edit';
  mic: string;
}>();
const loading = ref(false);
const { cultures, timeZones } = storeToRefs(usePreferencesStore());
const form: Exchange = reactive({
  mic: '',
  timeZone: '',
  openingTime: '',
  closingTime: '',
  locales: [{ culture: 'en', name: '' }],
});

if (props.action === 'edit') {
  exchangeApi
    .get(props.mic)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const add = (culture: string): ExchangeLocale => ({
  culture: culture,
  name: '',
});

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
