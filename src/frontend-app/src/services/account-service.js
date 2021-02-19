import Http from "./base/http";
import authentication from './config/authentication';

/**
 * @typedef {AccountService}
 */

export class AccountService extends Http {

    /**
     * @type {String}
     */
    static resource = '/api/account';

    static _instance = null;

    /**
     * @param {String} resource
     * @param {Object} options
     * @param {Object} http
     */
    constructor(resource, options = {}) {
        super(AccountService.resource, options, authentication);
    }

    static get Instance() {
        return this._instance || (this._instance = AccountService.build());
    }

    login(username, password) {
        return this.post('login', { username, password });
    }

    refreshToken(refreshToken) {
        return this.post('refresh-token', { refreshToken });
    }

    logout() {
        return this.post('logout');
    }
}
