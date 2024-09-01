import { IItemForBasket } from "../responce/IItemForBasket"

export interface IBasketAdd{
    "userId": string,
    "items": IItemForBasket[]
}