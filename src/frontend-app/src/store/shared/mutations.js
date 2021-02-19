import defaultState from './state.js';

export default {
    clearStore(state){
        Object.assign(state, defaultState);
    },
    setLoading(state, payload) {
        if (payload) {
            state.loadingCount++;
        } else {
            state.loadingCount--;
        }
        if (state.loadingCount < 0) state.loadingCount = 0;
    },
    setError(state, payload) {
        console.log('payload error', payload);
        if (payload && payload.hasErrors) {
            state.error = payload.messages[0].message;
        } else {
            state.error = payload;
        }
    },
    clearError(state) {
        state.error = null;
    },
    setSuccess(state, payload) {
        state.success = payload;
    },
    clearSuccess(state) {
        state.success = null;
    }
}
