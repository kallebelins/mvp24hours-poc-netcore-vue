import defaultState from './state.js';

export default {
    clearStore(state){
        Object.assign(state, defaultState);
    },
    setItems(state, payload) {
        state.items = payload;
    },
    setItem(state, payload) {
        state.item = payload;
    },
    setFilter(state, payload) {
        state.filter = payload;
    },
    setPaging(state, payload) {
        if (payload.paging) {
            state.criteria = payload.paging;
        }
        if (payload.summary) {
            state.paging.summary = payload.summary;
        }
    },
    setPagingLimit(state, payload) {
        state.paging.criteria.limit = payload;
    },
    setPagingOffset(state, payload) {
        state.paging.criteria.offset = payload;
    }
}