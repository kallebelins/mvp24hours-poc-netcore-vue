<template>
  <v-container>
    <v-row cols="12">
      <v-col :sm="6" :xs="12" cols="12">
        <h2 class="secondary--text">Customer</h2>
      </v-col>
      <v-col :sm="6" :xs="12" class="text-center text-sm-right px-2" cols="12">
        <v-btn
          class="secondary lighten-3 ma-1"
          @click="clearFilter()"
          :disabled="hasFilter ? null : true"
        >
          <v-icon left>mdi-close</v-icon>Clear Filter
        </v-btn>
        <v-btn class="primary ma-1" to="/customer/0/edit">
          <v-icon left>mdi-account-plus</v-icon>New Customer
        </v-btn>
      </v-col>
    </v-row>
    <v-card class="elevation-2 mt-4">
      <v-card-title>
        <v-select
          :value="paging.criteria.limit"
          :items="paging.limitItems"
          label="Items per page"
          @change="handleLimitChange"
        ></v-select>
        <v-spacer></v-spacer>
        <v-text-field
          :value="filter.name"
          append-icon="mdi-magnify"
          label="Search customer by name"
          single-line
          hide-details
          @change="filterByName($event)"
        ></v-text-field>
      </v-card-title>
      <v-data-table
        :headers="headers"
        :items="items"
        :sort-by="['name']"
        :sort-desc="[true]"
        hide-default-footer
        disable-pagination
      >
        <template v-slot:item.actions="{ item }">
          <v-btn
            fab
            small
            class="mr-4 elevation-0"
            :to="`/customer/${item.id}`"
          >
            <v-icon>mdi-account-details</v-icon>
          </v-btn>
        </template>
      </v-data-table>
      <div class="text-center pt-2">
        <v-row>
          <v-col>
            <v-pagination
              :value="paging.criteria.offset+1"
              :length="paging.summary.totalPages"
              total-visible="7"
              next-icon="mdi-menu-right"
              prev-icon="mdi-menu-left"
              @input="handleOffsetChange"
            ></v-pagination>
          </v-col>
        </v-row>
      </div>
    </v-card>
  </v-container>
</template>

<script>
import { mapGetters, mapActions, mapMutations } from "vuex";

export default {
  data() {
    return {
      headers: [
        {
          text: "Id",
          value: "id",
          align: "center",
          class: "primary--text subtitle-2",
        },
        {
          text: "Name",
          value: "name",
          class: "primary--text subtitle-2",
        },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          class: "primary--text subtitle-2",
          width: "50px",
        },
      ],
    };
  },
  methods: {
    ...mapActions("customer", ["getBy"]),
    ...mapMutations("customer", [
      "setFilter",
      "setPagingLimit",
      "setPagingOffset",
    ]),
    clearFilter() {
      this.setFilter({});
      this.getBy();
    },
    filterByName(text) {
      this.setFilter({ name: text });
      this.getBy();
    },
    handleLimitChange(value) {
      this.setPagingLimit(value);
      this.getBy();
    },
    handleOffsetChange(value) {
      this.setPagingOffset(value == 0 ? 0 : value-1);
      this.getBy();
    },
  },
  created() {
    if (this.items.length == 0) {
      this.getBy();
    }
  },
  computed: {
    ...mapGetters("customer", ["items", "paging", "filter"]),
    hasFilter() {
      return !!this.filter.name;
    },
  },
};
</script>
