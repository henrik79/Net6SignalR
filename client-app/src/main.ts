import { createApp } from "vue";
import App from "@/App.vue";
import router from "./router";
import { VueSignalR } from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';
import moment from 'moment';

const connection = new HubConnectionBuilder()
    .withUrl('https://localhost:5001/processing')
    .build();

const app = createApp(App);

app.config.globalProperties.$filters = {
    'formatDate'(value) {
        if (value) {
            return moment(String(value)).format('YYYY-MM-DD HH:mm:ss');
        }
    }
};

app.use(router);
app.use(VueSignalR, { connection });
app.mount("#app");
