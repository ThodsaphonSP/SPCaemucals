import React from 'react';
import {createRoot} from 'react-dom/client';
import {Provider} from 'react-redux';
import {store} from './app/store';

import reportWebVitals from './reportWebVitals';
import './index.css';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {Login} from "./page/Login";
import RequireAuth from "./component/RequireAuth";
import {AdminPage} from "./page/admin/AdminPage";
import App from "./App";
import {AdminEditPage} from "./page/admin/AdminEditPage";

const container = document.getElementById('root')!;
const root = createRoot(container);

function ErrorPage() {
    return null;
}


const router = createBrowserRouter([
    {
        path: '/',
        element: <RequireAuth>
            <App/>
        </RequireAuth>,
        errorElement: <ErrorPage/>,
        // You can add child routes here
        children: [
            {path: 'admin', element: <RequireAuth><AdminPage></AdminPage></RequireAuth>},
            {path: 'admin/:id', element: <RequireAuth><AdminEditPage></AdminEditPage></RequireAuth>},
            // ... other child routes
        ],
    },
    {
        path: '/login',
        element: <Login/>
    }
    // ... other top-level routes
]);
root.render(
    <React.StrictMode>
        <Provider store={store}>
            <RouterProvider router={router}/>
        </Provider>
    </React.StrictMode>
);


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
