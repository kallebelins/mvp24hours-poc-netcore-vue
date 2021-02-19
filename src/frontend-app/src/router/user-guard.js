import { store } from '../store'

let entryUrl = null;

export default async (to, from, next) => {
    // /* eslint-disable no-debugger */
    // debugger;
    if (store.getters['account/accessToken']) {
        if (entryUrl) {
            const url = entryUrl;
            entryUrl = null;
            next(url);
        } else {
            next();
        }
    } else {
        entryUrl = to.path;
        next({ name: 'login' });
    }
}
