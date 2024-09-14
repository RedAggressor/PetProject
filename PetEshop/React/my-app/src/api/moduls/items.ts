import {apiClient} from "../client"
import { IItemRequest } from "../request/itemRequest"

const bathPass="http://www.fruitshop.com:5000/api/v1/CatalogBff/";

export const getItemById = async (id: string) => 
    await apiClient({
        path:`${bathPass}GetItemById?id=${id}`,
        options: {method:'POST'},        
    })

export const getItems = async () =>    
    await apiClient ({
        path:`${bathPass}GetListItem`,
        options:{method:'POST'}           
    })

export const ItemsByPages = async (items: IItemRequest) =>       
    await apiClient ({
        path:`${bathPass}ItemsByPage`,
        options:{method: "POST"},
        data:items
    })

   

