import { FC } from "react";
import Items from "./Items/Items";
import Home from "./Home/Home";
import Login from "./Login/Login";
import Basket from "./components/basket/Basket"

interface IRoute{
    key: string,
    title: string,
    path: string,
    enabled: boolean,
    component: FC<{}>
}

export const routes: Array<IRoute> = [
    {
        key: 'home-route',
        title: 'Home',
        path: '/',
        enabled: true,
        component: Home
    },
    {
        key: 'item-route',
        title: 'Item',
        path: '/item/:id',
        enabled: false,
        component: Items
    },
    {
        key: 'login-route',
        title: 'Login',
        path: '/login',
        enabled: true,
        component: Login
    },
    {
        key: 'basket-route',
        title: 'Basket',
        path: '/Basket',
        enabled: false,
        component: Basket
    }
]