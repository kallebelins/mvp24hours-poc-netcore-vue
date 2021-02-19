<template>
  <v-container>
    <v-form ref="form">
      <v-row cols="12">
        <v-col :sm="6" :xs="12" cols="12">
          <h2 class="secondary--text">
            <v-btn
              icon
              small
              class="elevation-0"
              :to="`/customer/${id}`"
              v-if="id"
            >
              <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <v-btn icon small class="elevation-0" to="/customer" v-else>
              <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <span class="pl-2">Customer</span>
          </h2>
        </v-col>
        <v-col :sm="6" :xs="12" cols="12" class="text-center text-sm-right">
          <v-btn class="primary ma-1" @click="save()">
                <v-icon left>mdi-content-save</v-icon>Save Customer
              </v-btn>
        </v-col>
      </v-row>
      <v-card class="elevation-2 mt-4" v-if="item != null">
        <v-card-text class="px-4 py-1">
          <v-row cols="12" no-gutters v-if="this.id">
            <v-col
              :lg="4"
              :sm="6"
              :xs="12"
              cols="12"
              class="px-1 py-3"
            >
              <strong>Id:</strong>
              {{ id }}
            </v-col>
            <v-col
              :lg="3"
              :sm="6"
              :xs="12"
              cols="12"
              class="px-1 py-3"
            >
              <strong>Created:</strong>
              {{ item.created | dateFormat }}
            </v-col>
          </v-row>
          <v-row cols="12" no-gutters>
            <v-col :lg="3" :md="6" :sm="12" cols="12" class="px-1 py-3">
              <v-text-field
                v-model="item.name"
                label="Name"
                outlined
                dense
                hide-details
                :rules="nameRules"
                required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row cols="12" no-gutters>
            <v-col :sm="12" cols="12" class="px-1 py-3">
              <v-textarea
                v-model="item.note"
                outlined
                dense
                clearable
                rows="2"
                name="note"
                label="Note"
                hide-details
                no-resize
              ></v-textarea>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-form>
  </v-container>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";

export default {
  data() {
    return {
      nameRules: [(v) => !!v || "Name is required."],
    };
  },
  methods: {
    ...mapActions("customer", ["getById", "create", "update"]),
    ...mapMutations("customer", ["setItem"]),
    save() {
      if (this.$refs.form.validate()) {
        const self = this;
        this.$confirm("Do you want to save this customer?").then((res) => {
          if (res) {
            if (self.id) {
              self.update(self.item);
            } else {
              self.create(self.item);
            }
          }
        });
      }
    },
  },
  mounted() {
    if (!this.id) {
      this.setItem({});
    } else if (this.item == null || this.id != this.item.id) {
      this.getById({ id: this.id });
    }
  },
  computed: {
    ...mapGetters("customer", ["item"]),
    id() {
      return this.$route.params.id > 0 ? this.$route.params.id : null;
    },
  },
};
</script>
