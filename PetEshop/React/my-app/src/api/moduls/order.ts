import {apiClient} from "../client"
import { IOrderAddRequest } from "../request/orderRR"

const bassPath = "http://www.alevelwebsite.com:5000/api/v1/Order/";

export const getOrderById = async (orderId: string) => await apiClient({
    path:`${bassPath}GetOrderById?orderId=${orderId}`,
    options: {method:'POST'}    
})

export const getOrderByUserId = async (userId: string) => await apiClient({
    path:`${bassPath}GetOrderByUserId?userId=${userId}`,
    options: {method:'POST'}
})

export const addOrder = async (order: IOrderAddRequest) => await apiClient({
    path:`${bassPath}AddOrder`,
    options: {method:'POST'},
    data: order
})