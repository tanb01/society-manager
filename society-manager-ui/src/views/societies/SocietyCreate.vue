<template>
  <div class="page">
    <h1>Create a Society</h1>
    <form @submit.prevent="createSociety">
      <h3>Name and describe your society</h3>
      <div class="field">
        <v-text-field
          label="Name"
          v-model="society.name"
          type="text"
          required
        />
      </div>
      <div class="field">
        <v-text-field
          label="Description"
          v-model="society.description"
          type="text"
        />
      </div>
      <v-btn type="submit" class="blue white--text">Submit</v-btn>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      society: this.createSocietyObject()
    }
  },
  methods: {
    createSociety() {
      this.$store
        .dispatch('society/createSociety', this.society)
        .then((response) => {
          this.$router.push({
            name: 'society-show',
            params: { id: response.societyId }
          })
          this.society = this.createSocietyObject()
        })
    },
    createSocietyObject() {
      return {
        name: '',
        description: '',
        members: '',
        events: ''
      }
    }
  }
}
</script>

<style scoped>
.field {
  max-width: 300px;
  margin-bottom: 24px;
  margin: 0 auto;
}
</style>
