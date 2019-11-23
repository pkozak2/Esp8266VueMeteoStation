<template>
  <main>
    <transition mode="out-in">
      <router-view />
    </transition>
  </main>
</template>

<script>
export default {
  name: "App",

  data: () => ({
    //
  })
};
</script>
