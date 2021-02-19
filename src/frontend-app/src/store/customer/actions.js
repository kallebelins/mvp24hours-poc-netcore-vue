import { CustomerService } from "../../services/customer-service";
import router from "../../router";

export default {
    getBy: async ({ commit, state }) => {
        commit('setLoading', true, { root: true });
        commit('clearError', null, { root: true });
        try {
            const response = await CustomerService.Instance.getBy({ ...state.filter, ...state.paging.criteria });
            console.log(response);
            commit('setLoading', false, { root: true });

            if (response.hasErrors)
                return;

            commit('setItems', response.data);
            commit('setPaging', response);

        } catch (error) {
            commit('setLoading', false, { root: true });
            commit('setError', error, { root: true });
            console.log(error);
        }
    },
    getById: async ({ commit }, payload) => {
        commit('setLoading', true, { root: true });
        commit('clearError', null, { root: true });
        commit('setItem', null);
        try {
            const response = await CustomerService.Instance.getById(payload);
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
    },
    create: async ({ commit, dispatch }, payload) => {
        commit('setLoading', true, { root: true });
        commit('clearSuccess', true, { root: true });
        commit('clearError', null, { root: true });
        try {
            const response = await CustomerService.Instance.create(payload);
            commit('setLoading', false, { root: true });

            if (response.hasErrors && response.data)
                return;

            commit('setSuccess', true, { root: true });

            const _id = response.data[0].id;
            commit('setItem', {});
            router.push({ name: "customerdetail", params: { id: _id } });

            dispatch('getBy');
        } catch (error) {
            commit('setLoading', false, { root: true });
            commit('setError', error, { root: true });
            console.log(error);
        }
    },
    update: async ({ commit, dispatch }, payload) => {
        commit('setLoading', true, { root: true });
        commit('clearSuccess', true, { root: true });
        commit('clearError', null, { root: true });
        try {
            console.log(payload);
            const response = await CustomerService.Instance.update(payload);
            commit('setLoading', false, { root: true });

            if (response.hasErrors && response.data)
                return;

            commit('setSuccess', true, { root: true });
            
            const _id = payload.id;
            commit('setItem', {});
            router.push({ name: "customerdetail", params: { id: _id } });

            dispatch('getBy');
        } catch (error) {
            commit('setLoading', false, { root: true });
            commit('setError', error, { root: true });
            console.log(error);
        }
    },
}