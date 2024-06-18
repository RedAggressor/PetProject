import { makeAutoObservable, runInAction, } from "mobx";
import * as apiClient from "../api/moduls/items"
import {IBrandsResponce} from "../api/responce/BrandsResponce"
import { IItemResponce } from "../api/responce/ItemsResponce";
import { IErrorResponce } from "../api/responce/IErrorResponce";

class HomeStore {
    
    brands: IItemResponce[] = [];    
   
    constructor() {
        makeAutoObservable(this);
        runInAction(this.prefetchData);
    }

    prefetchData = async () => {
        try {
            const respon = await apiClient.getItems();            
            this.brands = respon
                           
        }
        catch (error) {
            if(error instanceof Error) {
                console.error(error.message)   
            }
        }
    };
}

export default HomeStore;

