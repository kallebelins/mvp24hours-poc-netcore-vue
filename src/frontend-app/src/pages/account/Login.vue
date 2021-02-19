<template>
  <v-main>
    <v-container fill-height fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="4">
          <v-card class="elevation-12">
            <v-card-text>
              <v-row class="py-3" cols="12">
                <v-col class="pa-0" cols="12">
                  <v-img
                    class="image-center"
                    max-height="222"
                    max-width="222"
                    :src="require('@/assets/logo.png')"
                  ></v-img>
                </v-col>
              </v-row>

              <v-row class="px-2 mt-2 justify-center" cols="12">
                <v-col class="pb-4 pt-2" cols="12" :lg="6">
                  <v-form
                    @submit.prevent="onSignin"
                    lazy-validation
                    ref="form"
                    v-model="valid"
                  >
                    <v-row cols="12">
                      <v-col class="py-0" cols="12">
                        <v-text-field
                          :rules="usernameRules"
                          append-icon="mdi-account-circle"
                          dense
                          id="username"
                          label="Login"
                          name="username"
                          outlined
                          required
                          rounded
                          type="text"
                          v-model="username"
                        ></v-text-field>
                      </v-col>

                      <v-col class="py-0" cols="12">
                        <v-text-field
                          :rules="passwordRules"
                          append-icon="mdi-lock"
                          dense
                          id="password"
                          label="Senha"
                          name="password"
                          outlined
                          required
                          rounded
                          type="password"
                          v-model="password"
                        ></v-text-field>
                      </v-col>

                      <v-col class="py-0" cols="12">
                        <v-btn
                          :disabled="loading"
                          :loading="loading"
                          block
                          color="primary"
                          rounded
                          type="submit"
                          >Connect</v-btn
                        >
                      </v-col>

                      <v-col cols="12">
                        <v-btn
                          :disabled="loading"
                          :loading="loading"
                          @click.prevent="onResetPassword"
                          block
                          class="text-none"
                          color="primary"
                          dark
                          small
                          text
                          >I forgot my password</v-btn
                        >
                      </v-col>

                      <v-col cols="12" class="text-center">
                        <dl>
                          <dd>User: admin / Pass: admin</dd>
                          <dd>User: test1 / Pass: test1</dd>
                          <dd>User: test2 / Pass: test2</dd>
                        </dl>
                      </v-col>
                      
                    </v-row>
                  </v-form>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Login",
  data() {
    return {
      valid: true,
      username: "",
      usernameRules: [
        (v) => !!v || "User login is required.",
        (v) =>
          (v && v.length <= 255) ||
          "The user login must be less than 255 characters.",
      ],
      password: "",
      passwordRules: [(v) => !!v || "Password is required."],
    };
  },
  computed: {
    ...mapGetters("account", ["accessToken"]),
    ...mapGetters(["error", "loading"]),
  },
  watch: {
    user(value) {
      if (value !== null && value !== undefined) {
        this.$router.push({ name: "home" });
      }
    },
  },
  methods: {
    ...mapActions("account", ["login"]),
    onSignin() {
      if (this.$refs.form.validate()) {
        const self = this;
        this.login({
          username: this.username,
          password: this.password,
        }).then(() => {
          if (self.accessToken) {
            self.$router.push({ name: "home" });
          }
        });
      }
    },
  },
};
</script>

<style scoped>
.image-center {
  margin: auto;
}
</style>
