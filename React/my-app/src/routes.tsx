import { FC } from "react";
import Items from "./Items/Items";
import Home from "./Home/Home";
import Basket from "./components/basket/Basket"
import Callback from "./components/redirect/callback";
import UserCabinet from "./components/acount/userCabinet";
import SuccusfullPay from "./components/redirect/succesfullpay";

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
        key: 'basket-route',
        title: 'Basket',
        path: '/Basket',
        enabled: false,
        component: Basket
    },
    {
        key: 'callback-route',
        title: 'Callback',
        path: '/callback',
        enabled: false,
        component: Callback
    },
    {
        key: 'user-route',
        title: 'UserCabinet',
        path: '/usercabinet',
        enabled: false,
        component: UserCabinet
    },
    {
        key: 'succusfullpay-route',
        title: 'SuccusfullPay',
        path: '/succusfullpay',
        enabled: false,
        component: SuccusfullPay
    },
]