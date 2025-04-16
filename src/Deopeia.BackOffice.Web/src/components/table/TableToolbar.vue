<template>
  <div class="toolbar">
    <slot></slot>
    <FlexDivider />
    <slot name="right"></slot>
  </div>
</template>

<style scoped lang="scss">
.toolbar {
  display: flex;
}
</style>
