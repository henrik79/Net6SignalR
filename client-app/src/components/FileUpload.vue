<script setup lang="ts">
    import { ref, reactive } from 'vue';
    import { useSignalR } from '@dreamonkey/vue-signalr';
    import moment from 'moment';

    const signalr = useSignalR();

    type Message = {
        message: string,
        time: string
    };
            
    const file = ref(null);
    const serverMessages = reactive<Message[]>([]);

    function handleFileUpload() {

        if (file == null || file.value == null)
            return;

        function stopListening() {
            console.log('unsubscribing');
            signalr.off('Receive');
        }
        const appendMessage = (message: ServerMessage) => {
            console.log(message);
            serverMessages.push(message);
            if (message.message === 'done') {
                stopListening();
            }
        };

        signalr.on('Receive', appendMessage);

        serverMessages.splice(0);

        signalr.invoke('GetConnectionId')
            .then(function (connectionId) {

                const formData = new FormData()
                formData.append('file', file.value.files[0]);
                formData.append('connectionId', connectionId);

                const requestOptions = {
                    method: 'POST',
                    body: formData
                };

                appendMessage(<Message>({ message: 'Uploading file to server...', time: moment().toISOString() }));

                fetch('home', requestOptions)
                    .then(async response => {
                        console.log(response);
                    })
                    .catch(error => {
                        console.error('There was an error!', error);
                        stopListening();
                    });
            });
    }
</script>
<template>
    <div>
        <h1>Upload file</h1>
        <input ref="file" type="file" />
        <button v-on:click="handleFileUpload()">Upload</button>
        
        <div v-if="serverMessages" class="content">
            <ul>
                <li v-for="message in serverMessages" :key="message.message">
                    <span class="message-time">{{ $filters.formatDate(message.time)}}:</span><span :class="{ warning: message.type === 1,error: message.type === 2 }"> {{message.message}}</span><div class="svg-i-16x16-error" v-if="message.type === 2"></div>
                </li>
            </ul>

        </div>
    </div>
</template>
<style>
    ul {
        list-style: none;
        padding-left: 0;
    }

    .message-time {font-weight: bold; margin-right: 5px}

    .warning {
        color: orange
    }

    .error {
        color: red
    }

    .svg-i-16x16-error {
        background-image: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4NCjxzdmcgd2lkdGg9IjE2cHgiIGhlaWdodD0iMTZweCIgdmlld0JveD0iMCAwIDE2IDE2IiBmaWxsPSJub25lIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPg0KPGcgY2xpcC1wYXRoPSJ1cmwoI2NsaXAwKSI+DQo8cGF0aCBkPSJNOCAxNkM5LjU4MjI1IDE2IDExLjEyOSAxNS41MzA4IDEyLjQ0NDYgMTQuNjUxOEMxMy43NjAyIDEzLjc3MjcgMTQuNzg1NSAxMi41MjMzIDE1LjM5MSAxMS4wNjE1QzE1Ljk5NjUgOS41OTk2NiAxNi4xNTUgNy45OTExMyAxNS44NDYzIDYuNDM5MjhDMTUuNTM3NiA0Ljg4NzQzIDE0Ljc3NTcgMy40NjE5NyAxMy42NTY5IDIuMzQzMTVDMTIuNTM4IDEuMjI0MzMgMTEuMTEyNiAwLjQ2MjQwMyA5LjU2MDcyIDAuMTUzNzIxQzguMDA4ODcgLTAuMTU0OTYgNi40MDAzNCAwLjAwMzQ2NjI1IDQuOTM4NTMgMC42MDg5NjdDMy40NzY3MiAxLjIxNDQ3IDIuMjI3MjkgMi4yMzk4NSAxLjM0ODI0IDMuNTU1NDRDMC40NjkxOTIgNC44NzEwMyAwIDYuNDE3NzUgMCA4QzAgMTAuMTIxNyAwLjg0Mjg1NSAxMi4xNTY2IDIuMzQzMTUgMTMuNjU2OUMzLjg0MzQ0IDE1LjE1NzEgNS44NzgyNyAxNiA4IDE2WiIgZmlsbD0iI0UwMEIxQyIvPg0KPHBhdGggZD0iTTEyLjU5NDEgNC42MjgwNUwxMS4xNzIxIDMuMjA2MDVMNy45MDAwNSA2LjQ3ODA1TDQuNjI4MDUgMy4yMDYwNUwzLjIwNjA1IDQuNjI4MDVMNi40NzgwNSA3LjkwMDA1TDMuMjA2MDUgMTEuMTcyMUw0LjYyODA1IDEyLjU5NDFMNy45MDAwNSA5LjMyMjA1TDExLjE3MjEgMTIuNTk0MUwxMi41OTQxIDExLjE3MjFMOS4zMjIwNSA3LjkwMDA1TDEyLjU5NDEgNC42MjgwNVoiIGZpbGw9IiNGRkZGRkYiLz4NCjwvZz4NCjxkZWZzPg0KPGNsaXBQYXRoIGlkPSJjbGlwMCI+DQo8cmVjdCB3aWR0aD0iMTYiIGhlaWdodD0iMTYiIGZpbGw9IndoaXRlIi8+DQo8L2NsaXBQYXRoPg0KPC9kZWZzPg0KPC9zdmc+);
        background-repeat: no-repeat;
        background-position: center;
        width: 16px;
        height: 16px;
        display: inline-block;
        margin-left: 5px;
    }
</style>
