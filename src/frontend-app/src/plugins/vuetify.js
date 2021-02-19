import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import colors from 'vuetify/lib/util/colors';
import VueI18n from 'vue-i18n';
import moment from 'moment';
import VuetifyConfirm from 'vuetify-confirm';
import VueTheMask from 'vue-the-mask';

Vue.use(Vuetify);
Vue.use(VueI18n);

Vue.use(VueTheMask);

moment.locale('pt-BR');
Vue.use(require('vue-moment'), { moment });

const vuetify = new Vuetify({
    theme: {
        themes: {
            light: {
                // primary: "#...",
                // secondary: "#...",
                // accent: "#...",
                error: colors.red.accent3,
            },
        },
    },
});

Vue.use(VuetifyConfirm, {
    vuetify,
    buttonTrueText: 'Ok',
    buttonFalseText: 'Cancelar',
    color: '#d3ba33',
    icon: 'mdi-alert',
    title: 'Warning',
    width: 350,
    property: '$confirm',
    persistent: true
});

export default vuetify;