import { FC } from "react";
import Items from "./Item/Items";
import Home from "./Home/Home";
import Basket from "./Basket/Basket"
import Callback from "./Redirect/callback";
import UserCabinet from "./UserAccount/userCabinet";
import SuccusfullPay from "./Redirect/succesfullpay";

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