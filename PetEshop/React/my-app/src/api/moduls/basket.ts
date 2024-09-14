import {apiClient, apiClientNoJwt} from "../client";
import { IBasketAdd } from "../request/basketRequest";

const bathpass = "http://www.fruitshop.com:5003/api/v1/BasketBff/";
const bathpassNojwt = "http://www.fruitshop.com:5003/api/v1/BasketBffNoJwt/";

export const addItems = async (request:IBasketAdd) =>     
    await apiClient({
        path: `${bathpass}AddItems`,
        options:  { method: "POST"},
        data: request 
    });
    
export const getItems = async () =>     
    await apiClient({
        path:`${bathpass}GetItems`,
        options: {method:"POST"}   
    });

export const addItemsNoJwt = async (request:IBasketAdd) =>
    await apiClientNoJwt({
        path:`${bathpassNojwt}AddItems`,
        method: 'POST',
        data: request
    });

export const getItemNoJwt = async () =>
    await apiClientNoJwt({
        path:`${bathpassNojwt}GetItems`,
        method: 'POST'
    });