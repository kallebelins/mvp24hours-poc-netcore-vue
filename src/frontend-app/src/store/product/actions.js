import { ProductService } from "../../services/product-service";

export default {
    reload: async ({ commit, dispatch }) => {
        commit('clearItems');
        return dispatch('getBy');
    },
    getBy: async ({ commit, state }) => {
        commit('setLoading', true, { root: true });
        commit('clearError', null, { root: true });
        try {
            const response = await ProductService.Instance.getBy({ limit: 10, offset: state.offset });
            console.log(response);
            commit('setLoading', false, { root: true });

            if (response.hasErrors)
                return;

            commit('addItems', response.data || []);
        } catch (error) {
            commit('setLoading', false, { root: true });
            commit('setError', error, { root: true });
            console.log(error);
        }
    },
    getById: async ({ commit }, payload) => {
        commit('setLoading', true, { root: true });
        commit('clearError', null, { root: true });
        try {
            const response = await ProductService.Instance.getById(payload);
            console.log(response);
            commit('setLoading', false, { root: true });

            if (response.hasErrors && response.data)
                return;

            commit('setItem', response.data[0]);
        } catch (error) {
            commit('setLoading', false, { root: true });
            commit('setError', error, { root: true });
            console.log(error);
        }
    }
}