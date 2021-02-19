import Http from "./base/http";

/**
 * @typedef {CustomerService}
 */

export class CustomerService extends Http {

    /**
     * @type {String}
     */
    static resource = '/api/customer';

    static _instance = null;

    /**
     * @param {String} resource
     * @param {Object} options
     * @param {Object} http
     */
    constructor(resource, options = {}, http = null) {
        super(CustomerService.resource, options, http);
    }

    static get Instance() {
        return this._instance || (this._instance = CustomerService.build());
    }

    getBy(payload) {
        return this.get(CustomerService.buildQuery(payload));
    }

    getById(payload) {
        return this.get(`/${payload.id}`);
    }

    create(payload) {
        return this.post('', payload);
    }

    update(payload) {
        return this.put(`/${payload.id}`, payload);
    }

    delete(payload) {
        return this.delete(`/${payload.id}`, payload);
    }
}
