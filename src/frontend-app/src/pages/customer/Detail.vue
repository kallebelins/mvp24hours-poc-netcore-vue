<template>
  <v-container>
    <v-row cols="12">
      <v-col :sm="6" :xs="12" cols="12">
        <h2 class="secondary--text">
          <v-btn icon small class="elevation-0" to="/customer">
            <v-icon>mdi-arrow-left</v-icon>
          </v-btn>
          <span class="pl-2">Customer</span>
        </h2>
      </v-col>
      <v-col :sm="6" :xs="12" class="text-center text-sm-right px-2" cols="12">
        <v-btn class="primary ma-1" :to="`/customer/${id}/edit`">
          <v-icon left>mdi-account-edit</v-icon>Edit Customer
        </v-btn>
      </v-col>
    </v-row>
    <v-card class="elevation-2 mt-4" v-if="item != null">
      <v-card-text class="px-4 py-2">
        <v-row cols="12" no-gutters>
          <v-col :lg="4" :sm="6" :xs="12" cols="12" class="px-1 py-2">
            <strong>Id:</strong>
            {{ id }}
          </v-col>
          <v-col :lg="4" :sm="6" :xs="12" cols="12" class="px-1 py-2">
            <strong>Created:</strong>
            {{ item.created | dateFormat }}
          </v-col>
          <v-col :lg="6" :sm="6" :xs="12" cols="12" class="px-1 py-2">
            <strong>Name:</strong>
            {{ item.name }}
          </v-col>
        </v-row>
        <v-row v-if="item.note">
          <v-col :sm="12" cols="12" class="px-3 py-2">
            <strong>Note:</strong>
            {{ item.note }}
          </v-col>
        </v-row>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {};
  },
  methods: {
    ...mapActions("customer", ["getById"]),
  },
  mounted() {
    this.getById({ id: this.id });
  },
  computed: {
    ...mapGetters("customer", ["item"]),
    id() {
      return this.$route.params.id;
    }
  },
};
</script>
