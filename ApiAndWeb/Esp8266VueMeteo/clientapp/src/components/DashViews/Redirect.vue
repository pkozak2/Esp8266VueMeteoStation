<template>
    <div>Redirection!</div>
</template>
<script>
    export default {
        name: "redirect",
        mounted() {
            location.replace('http://pkozak2.aqi.eco');
        }
    }
</script>