import { IItemForBasket } from "../responce/IItemForBasket"

export interface IBasketAddRequest{
    "userId": string,
    "items": IItemForBasket[]
}

export interface IBAskerGetRequest{
    'userId':string
}