<template>
  <el-form v-loading="isLoading" :model="form" label-width="200" @submit.prevent="mutate">
    <el-form-item :label="$t('common.code')">
      <el-input v-if="action === 'create'" v-model="form.code" />
      <template v-else>{{ form.code }}</template>
    </el-form-item>

    <LocaleTabs v-model:locales="form.localizations" :add="add">
      <LocaleTabPane v-for="locale in form.localizations" :locale="locale" :key="locale.culture">
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
    <el-form-item :label="$t('identity.permission')">
      <el-checkbox-group v-model="form.permissionCodes">
        <el-checkbox
          v-for="permission in permissionOptions"
          :key="permission.value"
          :label="permission.name"
          :value="permission.value"
          :disabled="!permission.isEnabled"
        />
      </el-checkbox-group>
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="isPending" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import { roleApi, type Role, type RoleLocalization } from '@/api/identity/role-api';
import { usePermissionOptionsQuery } from '@/composables/identity/usePermissionOptionsQuery';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  code: string;
}>();

const { data: permissionOptions } = usePermissionOptionsQuery();
const form: Role = reactive({
  code: '',
  isEnabled: true,
  localizations: [{ culture: 'en', name: '' }],
  permissionCodes: [],
});

const { isFetching } = useQuery({
  queryKey: ['roleApi.get', props.code],
  queryFn: () => roleApi.get(props.code).then((x) => Object.assign(form, x)),
  enabled: props.action === 'edit',
});
const { isLoading } = useDeferredLoading(isFetching);

const { isPending, mutate } = useMutation({
  mutationFn: () => (props.action === 'create' ? roleApi.create(form) : roleApi.update(form)),
  onSuccess: () => success(props.action),
});

const add = (culture: string): RoleLocalization => ({
  culture: culture,
  name: '',
});
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
