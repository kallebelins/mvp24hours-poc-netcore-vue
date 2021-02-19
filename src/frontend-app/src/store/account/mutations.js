
import defaultState from './state.js';

export default {
    clearStore(state){
        Object.assign(state, defaultState);
    },
    setUser(state, payload) {
        state.user = payload;
    },
    setToken(state, payload) {
        state.accessToken = payload;
    },
    setRefreshToken(state, payload) {
        state.refreshToken = payload;
    }
}
