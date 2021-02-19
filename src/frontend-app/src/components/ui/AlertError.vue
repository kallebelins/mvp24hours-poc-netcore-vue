<template>
    <v-snackbar
            :value="dialog"
            color="red"
            multi-line
            top
    >
        {{ text }}
        <v-btn @click="onClose()" dark text>
            OK
        </v-btn>
    </v-snackbar>
</template>

<script>
    export default {
        name: 'AlertError',
        methods: {
            onClose() {
                this.$store.dispatch('clearError');
            }
        },
        computed: {
            text() {
                if (this.error) {
                    //return this.error.detail || this.error.message;
                    return this.error;
                } else {
                    return null;
                }
            },
            dialog() {
                return this.error && !!this.text;
            },
            error() {
                return this.$store.getters.error;
            }
        }
    }
</script>
