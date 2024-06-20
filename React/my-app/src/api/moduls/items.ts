import apiClient from "../client"

export const getItemById = (id: string) => apiClient({
    path:`http://localhost:5000/api/v1/CatalogBff/GetById?id=${id}`,
    method:'POST',    
})

export const getItems = () => apiClient ({
    path:'http://localhost:5000/api/v1/CatalogBff/GetListItem',
    method:'POST'
})