import { AccountService } from "../../services/account-service";

export default {
    login: async ({ commit }, payload) => {
        commit('setLoading', true, { root: true });
        commit('clearError', null, { root: true });
        try {
            console.log(payload);
            const bo = await AccountService.Instance.login(payload.username, payload.password);
            commit('setLoading', false, { root: true });

            if (bo.hasErrors)
                return;

            if (!bo.accessToken) {
                commit('setError', 'A failure occurred while attempting to login.', { root: true });
                return;
            }

            const user = {
                username: bo.username,
                role: bo.role
            };

            commit('setUser', user);
            commit('setToken', bo.accessToken);
            commit('setRefreshToken', bo.refreshToken);

        } catch (error) {
            commit('setLoading', false, { root: true });
            commit('setError', error, { root: true });
            console.log(error);
        }
    },
    getToken: async ({ commit, state }) => {
        try {
            const bo = await AccountService.Instance.refreshToken(state.refreshToken);
            if (bo.accessToken) {
                commit('setToken', bo.accessToken);
                commit('setRefreshToken', bo.refreshToken);
            }
        } catch (error) {
            console.log(error);
        }
    },
    logout: async ({ commit }) => {
        try {
            await AccountService.Instance.logout();
            commit('setUser', null);
            commit('setToken', null);
        } catch (error) {
            commit('setUser', null);
            console.log(error);
        }
    }
}
