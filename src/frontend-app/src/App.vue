<template>
  <v-app>
    <component :is="layout">
      <transition mode="out-in" name="fade">
        <router-view></router-view>
      </transition>
    </component>
    <app-loading></app-loading>
  </v-app>
</template>

<script>
const default_layout = "clean";
export default {
  name: "App",
  computed: {
    layout() {
      return (this.$route.meta.layout || default_layout) + "-layout";
    }
  }
};
</script>
