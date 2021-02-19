import Vue from 'vue';
import VueRouter from 'vue-router';
import UserGuard from './user-guard';
// pages
import Login from "../pages/account/Login";
import Logout from "../pages/account/Logout";

Vue.use(VueRouter);

const router = new VueRouter({
    scrollBehavior() {
        return window.scrollTo({top: 0, behavior: 'smooth'});
    },
    routes: [
        {
            path: '/',
            name: 'home',
            meta: {layout: 'user'},
            component: () => import('../pages/home/Index.vue'),
            beforeEnter: UserGuard
        },
        {
            path: '/account/login',
            name: 'login',
            meta: {layout: 'clean'},
            component: Login
        },
        {
            path: '/account/logout',
            name: 'logout',
            meta: {layout: 'clean'},
            component: Logout
        },
        {
            path: '/product',
            name: 'product',
            meta: {layout: 'user'},
            component: () => import('../pages/product/Index.vue'),
            beforeEnter: UserGuard
        },
        {
            path: '/customer',
            name: 'customer',
            meta: {layout: 'user'},
            component: () => import('../pages/customer/Index.vue'),
            beforeEnter: UserGuard
        },
        {
            path: '/customer/:id',
            name: 'customerdetail',
            meta: {layout: 'user'},
            component: () => import('../pages/customer/Detail.vue'),
            beforeEnter: UserGuard
        },
        {
            path: '/customer/:id/edit',
            name: 'customeredit',
            meta: {layout: 'user'},
            component: () => import('../pages/customer/Edit.vue'),
            beforeEnter: UserGuard
        }
    ]
});

export default router;
