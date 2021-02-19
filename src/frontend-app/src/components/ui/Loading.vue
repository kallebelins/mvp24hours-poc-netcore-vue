<template>
    <div>
        <v-progress-linear
                :active="loading"
                :indeterminate="true"
                color="primary"
                height="10"
                style="top: -2px; position: absolute;z-index: 6;"
        ></v-progress-linear>
        <v-overlay :value="loading" color="secondary ligthen-1"></v-overlay>
    </div>
</template>

<script>
    export default {
        name: "Loading",
        computed: {
            loading() {
                return this.$store.getters.loading;
            }
        }
    }
</script>

<style scoped>

</style>
