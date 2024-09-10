<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
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
import type { Permission } from '@/api/identity/permission-api';
import permissionApi from '@/api/identity/permission-api';
import { Guid } from '@/models/guid';
import { success } from '@/plugins/element';
import { usePreferencesStore } from '@/stores/preferences';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const { cultures } = storeToRefs(usePreferencesStore());
const culture = ref('en');
const form: Permission = reactive({
  id: Guid.empty,
  isEnabled: true,
  locales: [],
});

if (props.action === 'edit') {
  permissionApi
    .get(props.id)
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
    props.action === 'create' ? permissionApi.create : permissionApi.update;
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
