import Vue from 'vue';
// layouts
import CleanLayout from '../layouts/CleanLayout';
import UserLayout from '../layouts/UserLayout';
// ui
import AlertError from '../components/ui/AlertError';
import AlertSuccess from '../components/ui/AlertSuccess';
import Loading from '../components/ui/Loading';

// layouts
Vue.component('user-layout', UserLayout);
Vue.component('clean-layout', CleanLayout);
//ui
Vue.component('app-alert-error', AlertError);
Vue.component('app-alert-success', AlertSuccess);
Vue.component('app-loading', Loading);