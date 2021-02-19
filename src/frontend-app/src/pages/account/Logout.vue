<template>
  <v-main>
    <v-container class="fill-height" fluid>
      <v-row align="center" justify="center">
        <v-col cols="12" sm="8" md="4">
          <v-card class="elevation-12">
            <v-card-text>
              <v-row class="text-center py-10" cols="12">
                <v-col class="py-4" cols="12">
                  <v-img
                    class="image-center"
                    max-height="222"
                    max-width="222"
                    :src="require('@/assets/logo.png')"
                  ></v-img>
                </v-col>
                <v-col class="text-center display-1" cols="12"
                  >We look forward to your return!</v-col
                >
                <v-col class="text-center subtitle-2" cols="12"
                  >We are curious to know your opinion about our tool. Contact
                  us! ;)</v-col
                >
                <v-col class="text-center subtitle-2 py-6" cols="12"
                  >test@gmail.com</v-col
                >
                <v-col class="text-center" cols="12">
                  <v-btn
                    :to="{ name: 'login' }"
                    class="mr-4 text-none"
                    color="primary"
                    >Back Home</v-btn
                  >
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
import { mapMutations } from "vuex";

export default {
  name: "Logout",
  created() {
    this.sharedClearStore();
    this.accountClearStore();
    this.customerClearStore();
    this.productClearStore();
  },
  computed: {
    ...mapMutations("shared", { sharedClearStore: "clearStore" }),
    ...mapMutations("account", { accountClearStore: "clearStore" }),
    ...mapMutations("customer", { customerClearStore: "clearStore" }),
    ...mapMutations("product", { productClearStore: "clearStore" }),
  },
};
</script>

<style scoped>
.image-center {
  margin: auto;
}
</style>
