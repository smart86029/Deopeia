export function useDeferredLoading(source: Ref<boolean>, delay = 200) {
  const isLoading = ref(false);
  let timer: number | null = null;

  watch(source, (val) => {
    if (val) {
      timer = window.setTimeout(() => {
        isLoading.value = true;
      }, delay);
    } else {
      if (timer) {
        clearTimeout(timer);
        timer = null;
      }
      isLoading.value = false;
    }
  });

  onUnmounted(() => {
    if (timer) {
      clearTimeout(timer);
    }
  });

  return { isLoading };
}
