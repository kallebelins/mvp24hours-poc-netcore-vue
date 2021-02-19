<template>
  <v-container>
    <v-row cols="12">
      <v-col :sm="6" :xs="12" cols="12">
        <h2 class="secondary--text">
          <v-btn class="mx-2 elevation-0" fab small @click="reload()">
            <v-icon dark>mdi-reload</v-icon>
          </v-btn>
          Products
        </h2>
      </v-col>
    </v-row>
    <v-btn
      v-scroll="onScroll"
      v-show="fab"
      fab
      dark
      fixed
      bottom
      right
      color="primary"
      @click="toTop"
    >
      <v-icon>mdi-arrow-up</v-icon>
    </v-btn>
    <v-row cols="12">
      <v-col
        v-for="item in items"
        :key="item.id"
        cols="12"
        :xl="3"
        :md="4"
        :sm="6"
        :xs="12"
      >
        <v-card max-width="344" class="mx-auto elevation-2">
          <v-img
            v-if="item.imageLink"
            height="250"
            :lazy-src="item.imageLink"
            :src="item.imageLink"
            :alt="item.name"
          ></v-img>
          <v-card-title class="headline" v-text="item.name"></v-card-title>
          <v-card-text>
            <div>{{ item.category }}</div>
            <div v-if="item.description">{{ item.description }}</div>
          </v-card-text>
          <v-card-actions v-if="!item.description">
            <v-btn text @click="getById(item.id)"> More... </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
    <v-row cols="12">
      <v-col cols="12" class="text-center">
        <v-btn large class="elevation-0" @click="getBy()">Load more...</v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data: () => ({
    fab: false,
  }),
  created() {
    if (this.items.length == 0) {
      this.getBy();
    }
  },
  methods: {
    ...mapActions("product", ["getBy", "getById", "reload"]),
    onScroll(e) {
      if (typeof window === "undefined") return;
      const top = window.pageYOffset || e.target.scrollTop || 0;
      this.fab = top > 20;
    },
    toTop() {
      this.$vuetify.goTo(0);
    },
  },
  computed: mapGetters("product", ["items"]),
};
</script>