import {apiClient} from "../client"
import { IItemRequest } from "../request/itemRequest"
import { IItemByPageResponce } from "../responce/itembyPageResponce";

export const getItemById = async (id: string) => 
    await apiClient({
        path:`http://localhost:5000/api/v1/CatalogBff/GetById?id=${id}`,
        options: {method:'POST'},        
    })


export const getItems = async () =>    
    await apiClient ({
        path:'http://localhost:5000/api/v1/CatalogBff/GetListItem',
        options:{method:'POST'}           
    })

    


export const Items = async (items: IItemRequest) =>       
    await apiClient ({
        path:'http://localhost:5000/api/v1/CatalogBff/Items',
        options:{method: "POST"},
        data:items
    })

   

