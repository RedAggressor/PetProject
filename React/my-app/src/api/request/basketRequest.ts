import { IItemForBasket } from "../responce/IItemForBasket"

export interface IBasketAddRequest{
    "userId": string,
    "item": IItemForBasket[]
}

export interface IBAskerGetRequest{
    'userId':string
}