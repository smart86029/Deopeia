<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('common.code')">
      <el-input v-model="form.code" />
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
        <el-form-item :label="$t('common.description')">
          <el-input v-model="form.locales[index].description" type="textarea" />
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
import assetApi, { type Asset, type AssetLocale } from '@/api/quote/asset-api';
import { emptyGuid, type Guid } from '@/models/guid';
import { success } from '@/plugins/element';
import { usePreferencesStore } from '@/stores/preferences';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const { cultures } = storeToRefs(usePreferencesStore());
const form: Asset = reactive({
  id: emptyGuid,
  code: '',
  locales: [{ culture: 'en', name: '' }],
});

if (props.action === 'edit') {
  assetApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const add = (culture: string): AssetLocale => ({
  culture: culture,
  name: '',
});

const save = () => {
  loading.value = true;
  const post = props.action === 'create' ? assetApi.create : assetApi.update;
  post(form as Asset)
    .then(() => success(props.action))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
