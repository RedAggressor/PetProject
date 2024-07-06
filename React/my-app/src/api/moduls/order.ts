import {apiClient} from "../client"
import { IOrderAddRequest } from "../request/orderRR"

export const getOrderById = async (orderId: string) => await apiClient({
    path:`http://localhost:5000/api/v1/Order/GetOrderById?orderId=${orderId}`,
    options: {method:'POST'}    
})

export const getOrderByUserId = async (userId: string) => await apiClient({
    path:`http://localhost:5000/api/v1/Order/GetOrderByUserId?userIds=${userId}`,
    options: {method:'POST'}
})

export const addOrder = async (order: IOrderAddRequest) => await apiClient({
    path:`http://localhost:5000/api/v1/Order/AddOrder`,
    options: {method:'POST'},
    data: order
})