import { makeAutoObservable, runInAction, } from "mobx";
import * as apiClient from "../api/moduls/items"
import { ICatalogItemResponse } from "../api/responce/ICatalogItemResponse";

class HomeStore {
    
    items: ICatalogItemResponse[] = [];    
   
    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }

    prefetchData = async () => {
        try {
            const respon = await apiClient.getItems();            
            this.items = respon
                           
        }
        catch (error) {
            if(error instanceof Error) {
                console.error(error.message)   
            }
        }
    };
}

export default HomeStore;

