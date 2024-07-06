import { isNull } from "util";
import {apiClient} from "../client";
import { IItemForBasket } from "../responce/IItemForBasket";
import { IBAskerGetRequest, IBasketAddRequest } from "../request/basketRequest";

export const addItems = async (request:IBasketAddRequest) =>     
    await apiClient({
    path: "http://localhost:5003/api/v1/BasketBff/AddItems",
    options:  { method: "POST"},
    data: request }
    )
    
export const getItems = async (request:IBAskerGetRequest) =>     
    await apiClient({
        path:`http://localhost:5003/api/v1/BasketBff/GetItems?userId=${request.userId}`,
        options: {method:"POST"},
        data: request       
    })