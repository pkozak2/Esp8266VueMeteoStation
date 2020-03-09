/**
 * Define all of your application routes here
 * for more information on routes, see the
 * official documentation https://router.vuejs.org/en/
 */
//import store from "../store";
//export default [
//  {
//    path: "*",
//    meta: {
//      name: "",
//      requiresAuth: true
//    },
//    redirect: {
//      path: "/dashboard"
//    }
//  },
//  // This  allows you to have pages apart of the app but no rendered inside the dash
//  {
//    path: "/",
//    meta: {
//      name: "",
//      requiresAuth: false
//    },
//    component: () =>
//      import(/* webpackChunkName: "routes" */ `@/views/DefaultView.vue`),
//    // redirect if already signed in
//      beforeEnter: (to, from, next) => {
//          debugger;
//      if (store.getters.authorized) {
//        next("/dashbaord");
//      } else {
//        next("/dashboard");
//        //next();
//      }
//    },
//    children: [
//      {
//        path: "",
//        component: () => import(`@/components/LoginForm.vue`)
//      }
//    ]
//  },
//  // add any extra routes that you want rendered in the dashboard as a child below. Change toolbar names here
//  {
//    path: "/dashboard",
//    meta: {
//      name: "Dashboard View",
//      requiresAuth: false
//    },
//    // component: () => import(`@/views/DashboardView.vue`),
//    component: () => import(`@/views/DefaultView.vue`),
//    children: [
//      {
//        path: ":smog",
//        name: "Dashboard",
//        props: true,
//        component: () => import(`@/components/DashViews/Dashboard.vue`)
//      }
//      // {
//      //   path: "user-profile",
//      //   meta: {
//      //     name: "User Profile",
//      //     requiresAuth: true
//      //   },
//      //   component: () => import(`@/components/DashViews/UserProfile.vue`)
//      // },
//      // {
//      //   path: "table-list",
//      //   meta: {
//      //     name: "Table List",
//      //     requiresAuth: true
//      //   },
//      //   component: () => import(`@/components/DashViews/SimpleTables.vue`)
//      // },
//      // {
//      //   path: "user-tables",
//      //   meta: {
//      //     name: "User Table",
//      //     requiresAuth: true
//      //   },
//      //   component: () => import(`@/components/DashViews/UsersTable.vue`)
//      // },
//      // {
//      //   path: "tablestest",
//      //   meta: {
//      //     name: "Complex Tables test",
//      //     requiresAuth: true
//      //   },
//      //   component: () => import(`@/components/DashViews/TableList.vue`)
//      // },
//      // {
//      //   path: "typography",
//      //   meta: {
//      //     name: "Typography",
//      //     requiresAuth: true
//      //   },
//      //   component: () => import(`@/components/DashViews/Typography.vue`)
//      // },
//      // {
//      //   path: "icons",
//      //   meta: {
//      //     name: "Icons",
//      //     requiresAuth: true
//      //   },
//      //   component: () => import(`@/components/DashViews/Icons.vue`)
//      // },
//      //   {
//      //     path: "maps",
//      //     meta: {
//      //       name: "Maps",
//      //       requiresAuth: true
//      //     },
//      //     component: () => import(`@/components/DashViews/Maps.vue`)
//      //   },
//      //   {
//      //     path: "notifications",
//      //     meta: {
//      //       name: "Notifications",
//      //       requiresAuth: true
//      //     },
//      //     component: () => import(`@/components/DashViews/Notifications.vue`)
//      //   }
//    ]
//  }
//];

// export default [
//   {
//     path: "*",
//     meta: {
//       name: ""
//     },
//     redirect: {
//       path: "/dashboard"
//     }
//   },
//   {
//     path: "/dashboard",
//     meta: {
//       name: "Dashboard View",
//       requiresAuth: false
//     },
//     // component: () => import(`@/views/DashboardView.vue`),
//     component: () => import(`@/views/DefaultView.vue`),
//     children: [
//       {
//         path: "",
//         name: "Dashboard",
//         props: true,
//         component: () => import(`@/components/DashViews/Dashboard.vue`)
//         }

//     ]
//     },
//     {
//     path: "/dashboard1",
//     meta: {
//         name: "Dashboard View",
//         requiresAuth: false
//     },
//     // component: () => import(`@/views/DashboardView.vue`),
//     component: () => import(`@/views/DefaultView.vue`),
//     children: [
//         {
//             path: "",
//             name: "Dashboard1",
//             component: () => import(`@/components/DashViews/Dashboard.vue`)
//         },
//     ]
//   }
// ];
export default [
  {
    path: "*",
    meta: {
      name: "",
      requiresAuth: false
    },
    redirect: {
      path: "/pkozak/smogowy"
    }
  },
  {
    path: "/:userName",
    props: true,
    component: () => import("@/views/DashboardView.vue"),
    children: [
      {
        path: "",
        name: "UserDash",
        props: true,
        component: () => import("@/components/DashViews/NewDashboard.vue")
      },
      {
        path: "all",
        name: "UserDashAll",
        props: true,
        component: () => import("@/components/DashViews/Dashboard.vue")
      },
      {
        path: ":deviceName",
        name: "UserDeviceDash",
        props: true,
        component: () => import("@/components/DashViews/Dashboard.vue")
      },
      {
        path: ":deviceName/graphs",
        name: "UserDeviceGraphs",
        props: true,
        component: () => import("@/components/DashViews/Graphs.vue")
      },
      {
        path: ":deviceName/annualgraphs",
        name: "UserDeviceAnnualGraphs",
        props: true,
        component: () => import("@/components/DashViews/AnnualGraphs.vue")
      }
    ]
  }
];
