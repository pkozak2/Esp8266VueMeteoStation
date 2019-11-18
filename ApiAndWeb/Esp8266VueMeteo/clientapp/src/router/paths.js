function loadView(view) {
  return () =>
    import(/* webpackChunkName: "view-[request]" */ `@/views/${view}.vue`);
}

export default [
  {
    path: "*",
    meta: {
      name: "",
    },
    redirect: {
      path: "/dashboard"
    }
  },
  {
    path: "/dashboard",
    meta: {
      name: "Dashboard View",
    },
    component: loadView("HelloWorld")//() => import(`@/views/DashboardView.vue`)
  }
];
