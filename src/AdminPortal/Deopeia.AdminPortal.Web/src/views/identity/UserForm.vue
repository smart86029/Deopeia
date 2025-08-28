<template>
  <el-form :model="form" label-width="200" @submit.prevent="mutate">
    <el-form-item :label="$t('identity.userName')">
      <el-input v-model="form.userName" />
    </el-form-item>
    <el-form-item :label="$t('identity.password')">
      <el-input v-model="form.password" type="password" />
    </el-form-item>
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>
    <el-form-item :label="$t('identity.role')">
      <el-checkbox-group v-model="form.roleCodes">
        <el-checkbox
          v-for="role in roleOptions"
          :key="role.value"
          :label="role.name"
          :value="role.value"
          :disabled="!role.isEnabled"
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
import { userApi, type User } from '@/api/identity/user-api';
import { useRoleOptionsQuery } from '@/composables/identity/useRoleOptionsQuery';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();

const { data: roleOptions } = useRoleOptionsQuery();
const form: User = reactive({
  id: emptyGuid,
  userName: '',
  password: '',
  isEnabled: true,
  roleCodes: [],
});

useQuery({
  queryKey: ['userApi.get', props.id],
  queryFn: () => userApi.get(props.id).then((x) => Object.assign(form, x)),
  enabled: props.action === 'edit',
});

const { isPending, mutate } = useMutation({
  mutationFn: () => (props.action === 'create' ? userApi.create(form) : userApi.update(form)),
  onSuccess: () => success(props.action),
});
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
