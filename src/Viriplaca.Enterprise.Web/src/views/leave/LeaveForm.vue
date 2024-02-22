<template>
  <el-form label-width="200px" :model="form" @submit.prevent="onSubmit">
    <el-form-item :label="$t('common.type')">
      <el-input v-model="form.type" />
    </el-form-item>
    <el-form-item :label="$t('common.time')">
      <DateTimeRangePicker v-model="form.range" />
    </el-form-item>
    <el-form-item :label="$t('common.reason')">
      <el-input
        type="textarea"
        :autosize="{ minRows: 2, maxRows: 4 }"
        v-model="form.reason"
      />
    </el-form-item>
    <el-form-item>
      <ButtonBack />
      <ButtonSave />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import { dayjs } from 'element-plus';

const form = reactive({
  type: 0,
  range: undefined as dayjs.Dayjs[] | undefined,
  reason: '',
});

const onSubmit = () => {
  // operatorApi.create(form as Operator).then(() => {
  //   ElNotification.success({
  //     message: t(`message.${props.action}Success`),
  //     position: 'bottom-left',
  //   });
  // });
};
</script>
