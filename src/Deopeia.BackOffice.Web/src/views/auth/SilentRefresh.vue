<template>Processing...</template>

<script setup lang="ts">
const checks = [
  /[\?|&|#]code=/,
  /[\?|&|#]error=/,
  /[\?|&|#]token=/,
  /[\?|&|#]id_token=/,
];

const isResponse = (text: string) => {
  if (!text) {
    return false;
  }

  for (let i = 0; i < checks.length; i++) {
    if (text.match(checks[i])) {
      return true;
    }
  }

  return false;
};

const message = isResponse(location.hash)
  ? location.hash
  : '#' + location.search;

console.log(message);

(window.opener || window.parent).postMessage(message, location.origin);
</script>
