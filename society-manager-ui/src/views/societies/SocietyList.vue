<template>
  <div class="page">
    <h1>Societies</h1>
    <div>
      <v-btn :to="{ name: 'society-create' }" text color="primary"
        ><v-icon left>mdi-plus-circle-outline</v-icon>
        <span>Create a Society</span>
      </v-btn>
    </div>
    <SocietyCard
      v-for="society in societies"
      :key="society.societyId"
      :society="society"
    />
    <template v-if="page != 1">
      <router-link
        :to="{ name: 'society-list', query: { page: page - 1 } }"
        rel="prev"
        >Previous Page</router-link
      >
      |
    </template>
    <router-link
      :to="{ name: 'society-list', query: { page: page + 1 } }"
      rel="next"
    >
      Next Page</router-link
    >
  </div>
</template>

<script>
import SocietyCard from '@/components/SocietyCard.vue'
import { mapState } from 'vuex'

export default {
  components: {
    SocietyCard
  },
  created() {
    this.$store.dispatch('society/fetchSocieties', {
      limit: this.limit,
      page: this.page
    })
  },
  computed: {
    limit() {
      return parseInt(this.$route.query.limit) || 3
    },
    page() {
      return parseInt(this.$route.query.page) || 1
    },
    ...mapState({
      societies: (state) => state.society.societies
    })
  }
}
</script>
