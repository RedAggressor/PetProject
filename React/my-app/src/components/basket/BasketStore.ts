import { makeAutoObservable, runInAction } from "mobx";
import { IItemForBasket} from "../../api/responce/IItemForBasket";
import * as apiBasket from "../../api/moduls/basket";

class BasketStore {
    amount = 0;
    items: IItemForBasket [] = [];
    totalPrice = 0;
    error = '';
    clickButton = false;
    state = ''; 
    
    constructor()
    {
        makeAutoObservable(this);        
    }

    prefetchData = async () => {
        try{
            const responce = await apiBasket.getItems();
            if(responce.respCode === 1){
                this.items = responce.data;
            }                        
        }
        catch (error) {
            console.assert(error)
            
        }
    }

    async addItem(item: IItemForBasket) {
        try{
            this.items.push(item)
            this.setAmount();
            this.setPrice();
            const items: IItemForBasket [] = this.items;
            await apiBasket.addItems(items);
            await this.prefetchData();                               
        }
        catch (error){
            console.log(error);
        }
    }

    removeItem = async (id: number) => {
        try {
            this.items.splice(id, 1);
            this.setAmount();
            this.setPrice();
            const items: IItemForBasket [] = this.items;
            await apiBasket.addItems(items);
            await this.prefetchData();
        }
        catch (error){
            console.log(error)
        }
    }

    setAmount(){
        this.amount = this.items.length;
    }

    setPrice(){
        this.totalPrice = this.items.map(item => item.price).reduce((acc, curr) => acc + curr, 0);
    }

    setError(error:string){
        this.error = error;
    }

    getError() {
        return this.error;
    }
    
}

export default BasketStore;