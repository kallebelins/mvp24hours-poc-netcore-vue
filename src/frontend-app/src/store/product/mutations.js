import _ from 'lodash';
import defaultState from './state.js';

export default {
    clearStore(state){
        Object.assign(state, defaultState);
    },
    addItems(state, payload) {
        state.items = _.union(state.items, payload);
        state.offset++;
    },
    clearItems(state) {
        state.items = [];
        state.offset = 0;
    },
    setItem(state, payload) {
        if (payload.id) {
            state.items = state.items.map(item => item.id === payload.id ? payload : item);
        }
    }
}
