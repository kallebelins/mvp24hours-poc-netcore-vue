<template>
    <v-snackbar
            :value="dialog"
            color="green"
            multi-line
            top
    >
        Tudo certo! Agora é só conferir.
        <v-btn @click="onClose()" dark text>
            OK
        </v-btn>
    </v-snackbar>
</template>

<script>
    export default {
        name: 'AlertSuccess',
        methods: {
            onClose() {
                this.$store.dispatch('clearSuccess');
            }
        },
        computed: {
            dialog() {
                return !!this.$store.getters.success;
            }
        }
    }
</script>
