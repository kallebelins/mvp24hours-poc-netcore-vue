import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import router from './router'
import { store } from './store';
import moment from 'moment';

import './plugins/filters.js';
import './plugins/components.js';
import './plugins/vuetify-number.js';

Vue.config.productionTip = false;
Vue.prototype.moment = moment;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
