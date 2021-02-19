<template>
  <div>
    <v-navigation-drawer
      v-model="drawer"
      :clipped="$vuetify.breakpoint.lgAndUp"
      app
      class="elevation-2"
    >
      <v-row cols="12">
        <v-col class="py-6 px-12" cols="12">
          <v-img
            class="image-center"
            max-height="100"
            max-width="100"
            :src="require('@/assets/logo.png')"
          ></v-img>
        </v-col>
      </v-row>
      <v-list dense>
        <template v-for="item in items">
          <v-list-item :key="item.text" @click="onRedirect(item)" link>
            <v-list-item-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{ item.text }} </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar
      :clipped-left="$vuetify.breakpoint.lgAndUp"
      app
      dark
      class="secondary elevation-2"
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Frontend App</v-toolbar-title>
      <v-spacer></v-spacer>
      <span class="px-4">
        <v-icon>mdi-account-circle</v-icon>
        {{ userName }}
      </span>
      <v-btn icon @click="onLogout()">
        <v-icon>mdi-logout</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <v-container class="fill-height" fluid>
        <slot></slot>
      </v-container>
    </v-main>
    <app-alert-error></app-alert-error>
    <app-alert-success></app-alert-success>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "UserLayout",
  components: {},
  data() {
    return {
      dialog: false,
      drawer: null,
      items: [
        { icon: "mdi-clipboard-check", text: "Home", to: "/" },
        { icon: "mdi-account-group", text: "Customers", to: "/customer" },
        { icon: "mdi-package", text: "Products", to: "/product" },
        { icon: "mdi-logout", text: "Exit", to: "/account/logout" },
      ],
    };
  },
  methods: {
    onRedirect(item) {
      if (item.icon == "mdi-logout") {
        this.onLogout();
      } else {
        this.$router.push(item.to);
      }
    },
    onLogout() {
      this.$confirm("Do you want to leave?").then((res) => {
        if (res) {
          this.$store.dispatch("account/logout");
          this.$router.push({ name: "logout" });
        }
      });
    },
  },
  computed: {
    ...mapGetters("account", ["user"]),
    userName() {
      return (this.user || {}).username;
    },
  },
};
</script>
<style scoped>
.image-center {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 50%;
}
</style>
