<template>
  <el-tabs v-model="model" addable @tab-add="openDialog" @tab-remove="removeLocale">
    <slot></slot>

    <el-dialog v-model="dialogVisible" width="500" :title="$t('common.message.selectLocale')">
      <el-form label-width="120">
        <el-form-item :label="$t('common.locale')">
          <SelectOption v-model="selectedLocale" :options="availableLocales" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button type="primary" @click="addLocale">
          {{ $t('operation.add') }}
        </el-button>
      </template>
    </el-dialog>
  </el-tabs>
</template>

<script setup lang="ts" generic="TLocale extends Locale">
import type { Locale } from '@/models/localization';
import { useOptionStore } from '@/stores/option';

const model = defineModel<string>({ default: 'en' });
const locales = defineModel<TLocale[]>('locales', { default: () => [] });

const props = withDefaults(
  defineProps<{
    /** Width of form labels in pixels or CSS unit string */
    labelWidth?: string | number;
    /** Factory function to create new locale instance */
    add(localeCode: string): TLocale;
  }>(),
  { labelWidth: 200 },
);

const dialogVisible = ref(false);
const selectedLocale = ref('');

const { cultures: availableLocales } = storeToRefs(useOptionStore());

const labelWidth = computed(() =>
  typeof props.labelWidth === 'number' ? `${props.labelWidth}px` : props.labelWidth,
);

const openDialog = (): void => {
  selectedLocale.value = '';
  dialogVisible.value = true;
};

const addLocale = (): void => {
  if (!selectedLocale.value) {
    return;
  }

  const existingLocale = locales.value.find((locale) => locale.culture === selectedLocale.value);
  if (existingLocale) {
    console.warn(`Locale ${selectedLocale.value} already exists`);
    return;
  }

  const newLocale = props.add(selectedLocale.value);
  locales.value.push(newLocale);
  model.value = selectedLocale.value;
  dialogVisible.value = false;
  selectedLocale.value = '';
};

const removeLocale = (localeCode: string): void => {
  const index = locales.value.findIndex((locale) => locale.culture === localeCode);

  if (index === -1) {
    console.warn(`Locale ${localeCode} not found`);
    return;
  }

  locales.value.splice(index, 1);

  if (locales.value.length > 0) {
    model.value = model.value === localeCode ? locales.value[0].culture : model.value;
  } else {
    model.value = '';
  }
};
</script>

<style scoped lang="scss">
:deep(.el-tabs__header) {
  padding-left: v-bind('labelWidth');
}
</style>
