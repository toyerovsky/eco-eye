import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import './registerServiceWorker';
import 'vue-awesome/icons';

import axios from 'axios';
import VueAxios from 'vue-axios';
// @ts-ignore
import Icon from 'vue-awesome/components/Icon';

Vue.use(VueAxios, axios);

Vue.component('v-icon', Icon);


Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
