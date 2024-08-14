import {apiClient} from "../client";
import { IBAskerGetRequest, IBasketAddRequest } from "../request/basketRequest";

const bathpass= "http://www.alevelwebsite.com:5003/api/v1/BasketBff/"

export const addItems = async (request:IBasketAddRequest) =>     
    await apiClient({
    path: `${bathpass}AddItems`,
    options:  { method: "POST"},
    data: request }
    )
    
export const getItems = async (request:IBAskerGetRequest) =>     
    await apiClient({
        path:`${bathpass}GetItems?userId=${request.userId}`,
        options: {method:"POST"},
        data: request       
    })