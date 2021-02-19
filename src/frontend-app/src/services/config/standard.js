import axios from 'axios';
import router from '../../router';
import {store} from '../../store';

const standard = axios.create({
    baseURL: process.env.VUE_APP_BASE_API_URL,
    timeout: 100000,
    transformResponse: [
        function (data) {
            return data
        }
    ]
});

// standard.interceptors.response.use(..., ...)
standard.interceptors.request.use(async (config) => {
    // /* eslint-disable no-debugger */
    // debugger;
    const accessToken = store.getters['account/accessToken'];
    if (accessToken != null) {
        config.headers['Authorization'] = `Bearer ${accessToken}`;
    }
    return config;
}, (error) => {
    // eslint-disable-next-line no-console
    console.log('error request', error);
    return Promise.reject(error);
});

standard.interceptors.response.use((response) => {
    if (response.hasErrors) {
        store.commit('setError', response, { root: true });
    }
    // console.log('response', response);
    return response;
}, (error) => {
    // eslint-disable-next-line no-console
    console.log('error response', error);
    if (error && error.response && error.response.status === 401) {
        router.push({name: 'login'});
        const requestConfig = error.config;
        return axios(requestConfig);
    }
    return Promise.reject(error);
});

export default standard
