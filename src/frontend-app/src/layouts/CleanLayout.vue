<template>
    <div>
        <v-main>
            <v-container class="fill-height" fluid>
                <slot></slot>
            </v-container>
        </v-main>
        <app-alert-error></app-alert-error>
        <app-alert-success></app-alert-success>
    </div>
</template>

<script>
    export default {
        name: "CleanLayout"
    }
</script>

<style scoped>

</style>
