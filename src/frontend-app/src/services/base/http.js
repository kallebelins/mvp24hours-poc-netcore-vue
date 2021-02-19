import standard from '../config/standard';
import Service from './service';

/**
 * @typedef {Http}
 */
export default class Http extends Service {
    /**
     * @param {String} path
     * @param {Object} options
     * @param {Object} http
     */
    constructor(path, options = {}, http = null) {
        super(options);
        this.path = path;
        this.http = http || standard;
    }

    /**
     * @param {Object} payload
     */
    static buildQuery(payload) {
        let str = '?' + Object.keys(payload).reduce(function(a, k){
            a.push(k + '=' + encodeURIComponent(payload[k]));
            return a;
        }, []).join('&');
        return str;
    }

    /**
     * @param {String} path
     * @param {Object} options
     */
    static build(path, options) {
        return new this(path, options)
    }

    /**
     * @param {Object} response
     * @returns {Object}
     */
    static then(response) {
        if (!response.data) {
            return {}
        }
        if (typeof response.data === 'string') {
            return JSON.parse(response.data)
        }
        return response.data
    }

    /**
     * @param {String} start
     * @param {String} end
     * @returns {String}
     */
    static normalize(start, end) {
        return `${start}/${end}`.replace(/([^:]\/)\/+/g, '$1')
    }

    /**
     * @param {String} url
     * @returns {*|Promise<any>}
     */
    get(url) {
        return this.http
            .get(this.constructor.normalize(this.path, url))
            .then(this.constructor.then)
    }

    /**
     * @param {String} url
     * @param {Object} data
     * @returns {*|Promise<any>}
     */
    post(url, data) {
        return this.http
            .post(this.constructor.normalize(this.path, url), data)
            .then(this.constructor.then)
    }

    /**
     * @param {String} url
     * @param {Object} data
     * @returns {*|Promise<any>}
     */
    put(url, data) {
        return this.http
            .put(this.constructor.normalize(this.path, url), data)
            .then(this.constructor.then)
    }

    /**
     * @param {String} url
     * @param {Object} data
     * @returns {*|Promise<any>}
     */
    patch(url, data) {
        return this.http
            .patch(this.constructor.normalize(this.path, url), data)
            .then(this.constructor.then)
    }

    /**
     * @param {String} url
     * @returns {*|Promise<any>}
     */
    delete(url) {
        return this.http
            .delete(this.constructor.normalize(this.path, url))
            .then(this.constructor.then)
    }
}
