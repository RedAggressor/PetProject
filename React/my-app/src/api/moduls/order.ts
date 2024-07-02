import {apiClient} from "../client"
import { IItemForBasket } from "../responce/IItemForBasket"

export const addOrder = async (order: IItemForBasket[]) => await apiClient({
    path:'',
    options: {method:'POST'},
    data: order
})