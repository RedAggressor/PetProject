import { makeAutoObservable, runInAction } from "mobx";
import { IItemForBasket} from "../../api/responce/IItemForBasket";
import * as apiBasket from "../../api/moduls/basket";

class BasketStore {
    amount = 0;
    items: IItemForBasket [] = [];
    price = 0;
    error = '';
    
    constructor()
    {
        makeAutoObservable(this);
        runInAction(this.prefetchData)
    }

    prefetchData = async () => {
        try{
            const responce =  await apiBasket.getItems();
            this.items = responce.data;
            this.amount = this.items.length;
            this.price = this.items.map(item => item.price).reduce((acc, curr) => acc + curr, 0);
        }
        catch (error) {
            if(error instanceof Error)
            console.assert(error)
        }
    }

    putInBasket = async (it: IItemForBasket[]) => {
        try{            
            const responce = await apiBasket.addItems(it);
            console.log(responce);
        }
        catch (error){
            console.log(error)
        }
    }
}

export default BasketStore;