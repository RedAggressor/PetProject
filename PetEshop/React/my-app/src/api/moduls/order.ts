import {apiClient} from "../client"
import { IOrderItem } from "../request/orderRR"

const bassPath = "http://www.fruitshop.com:5000/api/v1/Order/";

export const getOrderById = async (orderId: string) => await apiClient({
    path:`${bassPath}GetOrderById?orderId=${orderId}`,
    options: {method:'POST'}    
})

export const getOrderByUserId = async () => await apiClient({
    path:`${bassPath}GetOrderByUserId`,
    options: {method:'POST'}
})

export const addOrder = async () => await apiClient({
    path:`${bassPath}AddOrder`,
    options: {method:'POST'}
})

export const addItemsToOrder = async (order:IOrderItem) => await apiClient({
    path:`${bassPath}AddItemToOrder`,
    options:{method:'POST'},
    data:order
})

export const updateStatusOrder = async ( data : {orderId:string, orderStatus:string}) =>await apiClient({
    path:`${bassPath}UpdateStatusOrder`,
    options:{method:`POST`},
    data: data
})