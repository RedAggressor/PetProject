import apiClient from "../client";
import { IItemForBasket } from "../responce/IItemForBasket";

export const addItems = (items : IItemForBasket[]) => apiClient({
    path: "http://localhost:5003/api/v1/BasketBff/AddItems",
    method: "POST",
    data: items
});

export const getItems = () => apiClient({
    path: "http://localhost:5003/api/v1/BasketBff/GetItem",
    method: "POST"
})