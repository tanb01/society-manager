<template>
  <v-alert :type="notificationTypeClass">
    {{ notification.message }}
  </v-alert>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  props: {
    notification: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      timeout: null
    }
  },
  mounted() {
    this.timeout = setTimeout(
      () => this.removeNotification(this.notification),
      5000
    )
  },
  beforeDestroy() {
    clearTimeout(this.timeout)
  },
  computed: {
    notificationTypeClass() {
      return `${this.notification.type}`
    }
  },
  methods: mapActions('notification', ['removeNotification'])
}
</script>

<style scoped>
.notification-bar {
  margin: 1em 0 1em;
}
</style>
