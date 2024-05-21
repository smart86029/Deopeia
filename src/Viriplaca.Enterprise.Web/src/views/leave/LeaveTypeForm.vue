<template>
  <el-form
    label-width="200px"
    autocomplete="off"
    :model="form"
    @submit.prevent="save"
  >
    <el-form-item :label="$t('leave.canCarryForward')">
      <el-switch v-model="form.canCarryForward" />
    </el-form-item>
    <el-tabs v-model="locale">
      <el-tab-pane
        v-for="(locale, index) in form.locales"
        :key="locale.culture"
        :label="locale.culture"
        :name="locale.culture"
      >
        <el-form-item
          :prop="`details.${index}.name`"
          :label="$t('common.name')"
        >
          <el-input v-model="form.locales[index].name" />
        </el-form-item>
        <el-form-item :label="$t('common.description')">
          <el-input
            v-model="form.locales[index].description"
            :autosize="{ minRows: 8 }"
            type="textarea"
          />
        </el-form-item>
      </el-tab-pane>
    </el-tabs>

    <el-form-item>
      <ButtonBack />
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import type { LeaveType, LeaveTypeLocale } from '@/api/leave/leave-type-api';
import leaveTypeApi from '@/api/leave/leave-type-api';
import { Guid } from '@/models/guid';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(true);
const form = reactive({
  id: Guid.empty,
  locales: [] as LeaveTypeLocale[],
  canCarryForward: false,
});
const locale = ref('en-US');

if (props.action === 'edit') {
  leaveTypeApi
    .get(props.id)
    .then((x) => {
      Object.assign(form, x.data);
    })
    .finally(() => (loading.value = false));
}

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? leaveTypeApi.create : leaveTypeApi.update;
  post(form as LeaveType).then(() => {
    loading.value = false;
    success(props.action);
  });
};
</script>
