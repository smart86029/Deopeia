<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('common.code')">
      <el-input v-if="action === 'create'" v-model="form.code" />
      <template v-else>{{ form.code }}</template>
    </el-form-item>

    <LocaleTabs v-model:locales="form.locales" :add="add">
      <LocaleTabPane
        v-for="locale in form.locales"
        :locale="locale"
        :key="locale.culture"
      >
        <el-form-item :label="$t('common.name')">
          <el-input v-model="locale.name" />
        </el-form-item>
        <el-form-item :label="$t('common.description')">
          <el-input v-model="locale.description" type="textarea" />
        </el-form-item>
      </LocaleTabPane>
    </LocaleTabs>

    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import type {
  Permission,
  PermissionLocale,
} from '@/api/identity/permission-api';
import { permissionApi } from '@/api/identity/permission-api';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  code: string;
}>();
const loading = ref(false);
const form: Permission = reactive({
  code: '',
  isEnabled: true,
  locales: [{ culture: 'en', name: '' }],
});

if (props.action === 'edit') {
  permissionApi
    .get(props.code)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const add = (culture: string): PermissionLocale => ({
  culture: culture,
  name: '',
});

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
