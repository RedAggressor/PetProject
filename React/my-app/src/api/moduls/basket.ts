import apiClient from "../client";
import { IItemForBasket } from "../responce/IItemForBasket";

export const addItems = async (items : IItemForBasket[]) => await apiClient({
    path: "http://localhost:5003/api/v1/BasketBff/AddItems",
    method: "POST",
    data: items
});

export const getItems = async () => await apiClient({
    path: "http://localhost:5003/api/v1/BasketBff/GetItems",
    method: "POST"
})