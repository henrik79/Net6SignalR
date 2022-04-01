<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
    <RouterView />
</template>

<style>
@import "@/assets/base.css";
    nav {
        width: 100vw;        
        display: flex;
    }

        nav .menu-item {
            color: #FFF;
            padding: 25px 40px;
            position: relative;
            text-align: center;
            border-bottom: 5px solid transparent;
            display: flex;
            transition: 0.4s;
        }

            nav .menu-item.active,
            nav .menu-item:hover {
                background-color: #0d0d0e;
                border-bottom-color: #bb3030;
            }

            nav .menu-item a {
                color: inherit;
                text-decoration: none;
            }

</style>
