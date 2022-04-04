import { createRouter, createWebHistory } from "vue-router";
import Test from "../components/FileUpload.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            component: Test
        },
        {
            path: "/about",
            name: "about",
            component: () => import("../views/AboutView.vue")
        },
    ],
});

export default router;