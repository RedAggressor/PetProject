import { makeAutoObservable, runInAction, } from "mobx";
import * as apiClient from "../api/moduls/items";
import * as apiType from "../api/moduls/type";
import { ICatalogItemResponse as IItemResponse } from "../api/responce/ICatalogItemResponse";
import { IItemRequest } from "../api/request/itemRequest";
import { ITypeResponce } from "../api/responce/ITypeResponce";

class HomeStore {
    
    items: IItemResponse[] = [];
    totalPages = 0;
    currentPage = 1;
    pageSize = 6;
    type = 0;
    count = 0;
    error = '';
    listType: ITypeResponce [] = [];

    request: IItemRequest = {
        pageIndex: 1,
        pageSize: 0,
        filterTypeId: 0        
    }

    constructor() {
        makeAutoObservable(this);
        runInAction(async() => {
            await this.setTypeList();
            await this.prefetchData();
        });        
    }

    async setTypeList(){
        try{
        const responce = await apiType.getType();
        
        this.setListType(responce.data);
        
            //this.setError(responce.errorMessage);
        
        }
        catch (error) {
            console.log(error)
        }
    }

    prefetchData = async () => {
        try {
            this.setRequest();
            const responce = await apiClient.ItemsByPages(this.request);                  
            this.setItems(responce?.data);
            this.setCount(responce?.count);
            this.setTotalPage();
            //this.setError(responce.errorMessage);
            
        }
        catch (error) {
            if(error instanceof Error) {
                console.error(error)   
            }
        }
    };

    async changePage( page: number){        
        this.currentPage = page;               
        await this.prefetchData();           
    };

    async setType(id: number){
        this.type = id;
        await this.prefetchData();  
    };

    setTotalPage(){
        this.totalPages = Math.ceil(this.count / this.pageSize);
    }

    setItems(items: IItemResponse[]){
        this.items = items;
    }

    setCount(count:number){
        this.count = count;
    }
    setListType(listType: ITypeResponce []){
        this.listType = listType;        
    }

    setRequest(){
        this.request.pageIndex = this.currentPage;
        this.request.pageSize = this.pageSize;
        this.request.filterTypeId = this.type;
    }

    setCorrentPage(currentPage: number){
        this.currentPage = currentPage;
    }

    setError(error:string){
        this.error = error;
    }
}

export default HomeStore;

