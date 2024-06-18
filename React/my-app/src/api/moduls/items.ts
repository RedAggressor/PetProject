import apiClient from "../client"

export const getBrands = () => apiClient({
    path:`GetListBrand`,
    method:'POST'
})

export const getItems = () => apiClient ({
    path:'GetListItem',
    method:'POST'
})