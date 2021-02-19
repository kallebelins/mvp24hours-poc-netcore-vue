export default {
    clearError({commit}) {
        commit('clearError')
    },
    setError({commit}, payload) {
        commit('setError', payload)
    },
    clearSuccess({commit}) {
        commit('clearSuccess')
    },
    setSuccess({commit}, payload) {
        commit('setSuccess', payload)
    }
}
