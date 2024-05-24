import React from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import AdminLayout from "layouts/AdminLayout";
import Login from 'pages/Login/Login';
import Dashboard from 'pages/Dashboard/Dashboard';
import Tenant from 'pages/Tenant/Tenant';
import Account from 'pages/Account/Account';
import Category from 'pages/Category/Category';
import Product from 'pages/Product/Product';
import Orders from 'pages/Orders/Order';
import Sell from 'pages/Sell/Sell';

import { message } from 'antd';
const App = () => {
  const [messageApi, contextHolder] = message.useMessage();
    const router = createBrowserRouter([
        {
          path: "/",
          element: <AdminLayout />,
          children: [
            {
              path: "/",
              index: true,
              element: <Dashboard />
            },
            {
              path: "/tenant-management",
              index: false,
              element: <Tenant />
            }, 
            {
              path: "/account-management",
              index: false,
              element: <Account />
            },
            {
              path: '/category-management',
              index: false,
              element: <Category />
            },
            {
              path: '/product-management',
              index: false,
              element: <Product />
            },
            {
              path: '/order-management',
              index: false,
              element: <Orders />
            },
            { 
              path: '/sell-management',
              index: false,
              element: <Sell />
            }
          ]
        },
        {
          path: '/login',
          element:  <Login />,
        },
        // {
        //   path: '/register',
        //   element: <Register />
        // }

      ]);
    return <div className="App min-h-screen">
        {contextHolder}
        <RouterProvider router={router} />
    </div>;
}
export default App;
