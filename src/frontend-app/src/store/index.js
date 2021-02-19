import Vue from 'vue';
import Vuex from 'vuex';
import createPersistedState from 'vuex-persistedstate';
import shared from './shared';
import account from './account';
import customer from './customer';
import product from './product';

Vue.use(Vuex);

export const store = new Vuex.Store({
    plugins: [createPersistedState({
        storage: window.sessionStorage,
    })],
    modules: {
        shared: shared,
        account: account,
        customer: customer,
        product: product
    }
});
