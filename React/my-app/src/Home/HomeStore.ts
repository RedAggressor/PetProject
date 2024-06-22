import { makeAutoObservable, runInAction, } from "mobx";
import * as apiClient from "../api/moduls/items";
import * as apiType from "../api/moduls/type";
import { ICatalogItemResponse } from "../api/responce/ICatalogItemResponse";
import { IItemRequest } from "../api/request/itemRequest";
import { IItemByPageResponce } from "../api/responce/itembyPageResponce";
import { ITypeResponce } from "../api/responce/ITypeResponce";

class HomeStore {
    
    items: ICatalogItemResponse[] = [];
    totalPages = 0;
    currentPage = 0;
    pageSize = 6;
    type = 0;
    count = 0;    
    listType: ITypeResponce [] = [];

    request: IItemRequest = {
        pageIndex: 0,
        pageSize: 0,
        filterTypeId: 0        
    }

    constructor() {
        makeAutoObservable(this);
        runInAction(async() => {await this.setTypeList(); await this.prefetchData();});        
    }

    async setTypeList(){
        try{
        const responce = await apiType.getType();
        this.setListType(responce.list);
        }
        catch (error) {
            console.log(error)
        }
    }

    prefetchData = async () => {
        try {
            this.setRequest();
            const respon = await apiClient.Items(this.request);           
            this.setItems(respon.data);
            this.setCount(respon.count);
            this.setTotalPage();
        }
        catch (error) {
            if(error instanceof Error) {
                console.error(error.message)   
            }
        }
    };

    async changePage( page: number){        
        this.currentPage = (page -1);               
        await this.prefetchData();           
    };

    async setType(id: number){
        this.type = id;
        await this.prefetchData();  
    };

    setTotalPage(){
        this.totalPages = Math.ceil(this.count / this.pageSize);
    }

    setItems(items: ICatalogItemResponse[]){
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
}

export default HomeStore;

