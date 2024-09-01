import {apiClient} from "../client";
import { IBasketAdd } from "../request/basketRequest";

const bathpass= "http://www.alevelwebsite.com:5003/api/v1/BasketBff/"

export const addItems = async (request:IBasketAdd) =>     
    await apiClient({
    path: `${bathpass}AddItems`,
    options:  { method: "POST"},
    data: request 
    });
    
export const getItems = async (userId:string) =>     
    await apiClient({
        path:`${bathpass}GetItems?userId=${userId}`,
        options: {method:"POST"}   
    });