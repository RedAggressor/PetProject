import apiClient from "../client"
import { IItemRequest } from "../request/itemRequest"

export const getItemById = async (id: string) => await apiClient({
    path:`http://localhost:5000/api/v1/CatalogBff/GetById?id=${id}`,
    method:'POST',    
})

export const getItems = async () => await apiClient ({
    path:'http://localhost:5000/api/v1/CatalogBff/GetListItem',
    method:'POST'
})

export const Items = async (items: IItemRequest) => await apiClient ({
    path: 'http://localhost:5000/api/v1/CatalogBff/Items',
    method: "POST",
    data: items
})