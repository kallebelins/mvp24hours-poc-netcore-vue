import Vue from 'vue';
import moment from 'moment';

Vue.filter("realFormat", function (value) {
    if (value == null) return '0,00';
    let val = (value / 1).toFixed(2).replace('.', ',');
    return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
});

Vue.filter("dmFormat", function (value) {
    if (value == null) return '0|00';
    return (value / 1).toFixed(2).replace('.', '|');
});

Vue.filter("cpfCnpjFormat", function (value) {
    if (value == null) return '';
    const cnpjCpf = value.replace(/\D/g, '');
    if (cnpjCpf.length === 11) {
        /* eslint-disable-next-line */
        return cnpjCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3-\$4");
    }
    /* eslint-disable-next-line */
    return cnpjCpf.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, "\$1.\$2.\$3/\$4-\$5");
});

Vue.filter("dateFormat", function (value) {
    if (value == null) return '';
    return moment(value).format('DD/MM/YYYY');
});

Vue.filter("dateFormatComplete", function (value) {
    if (value == null) return '';
    return moment(value).format('DD/MM/YYYY HH:mm:ss');
});

Vue.filter("hourFormat", function (value) {
    if (value == null) return '';
    return moment(value).format('HH:mm:ss');
});