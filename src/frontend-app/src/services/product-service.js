import Http from "./base/http";

/**
 * @typedef {ProductService}
 */

export class ProductService extends Http {

    /**
     * @type {String}
     */
    static resource = '/api/product';

    static _instance = null;

    /**
     * @param {String} resource
     * @param {Object} options
     * @param {Object} http
     */
    constructor(resource, options = {}, http = null) {
        super(ProductService.resource, options, http);
    }

    static get Instance() {
        return this._instance || (this._instance = ProductService.build());
    }

    getBy(payload) {
        return this.get(ProductService.buildQuery(payload));
    }

    getById(id) {
        return this.get(`/${id}`);
    }
}
