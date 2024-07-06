import { makeAutoObservable, runInAction } from "mobx";
import { IItemForBasket} from "../../api/responce/IItemForBasket";
import * as apiBasket from "../../api/moduls/basket";
import { IBAskerGetRequest, IBasketAddRequest } from "../../api/request/basketRequest";
import * as apiOrder from "../../api/moduls/order"
import { IOrderAddRequest } from "../../api/request/orderRR";

class BasketStore {
    amount = 0;
    items: IItemForBasket [] = [];
    totalPrice = 0;
    error = '';
    clickButton = false;
    state = '';
    userId: string | undefined = '';

    itemsOrder: {
        count: number,
        itemId: number
    }[] = [];

    order: IOrderAddRequest = {
        userId: '',
        items: []
    }

    requestAdd: IBasketAddRequest = {
        userId: "",
        item: []
    };

    requestGet: IBAskerGetRequest = {
        userId: ''
    };

    constructor() {
        makeAutoObservable(this);
        runInAction(async ()=> await this.prefetchData())
    }

    async clearItems(){
        this.items = [];
        this.setRequestAdd();
        try{
            const responce = await apiBasket.addItems(this.requestAdd);
            console.log('items clear >>>>', this.items )
            console.log('responce items>>>>>', responce )
            this.prefetchData();
        }
        catch (error) {
            console.log(error);
        }
        
    }

    getItemId(){
        if(this.items.length > 0){
            console.log('items>>>>>>>>>>>>>>>',this.items)
            this.itemsOrder = [];
            this.items.map((item) => {                
                this.itemsOrder.push({ 
                    count: 1,
                    itemId: item.id
                })
            });
        }
        else{
            console.log('basket is empty >>>>>', this.items )
        }        
    }

    createOrder(){
        if(this.userId !== undefined && this.userId !== ''){
            this.order.userId = this.userId;            
        }
        else{
            console.log('user id not valide in order');
        }
        if(this.itemsOrder !== null){
            this.getItemId();
            this.order.items.push(...this.itemsOrder);
        }
        else{
            console.log('items for order is null');
        }        
    }

    async orderAdd(){
        try{
            this.createOrder();
            console.log('oder >>>', this.order);
            const response = await apiOrder.addOrder(this.order);
            //if(response.respCode === 1){
                console.log('responce >>>', response);
                this.clearItems();
            //}           
            
        }
        catch(eror){
            console.log(eror)
        }        
    }

    setUserId(userId:string| undefined){
        this.userId = userId;
    }

    prefetchData = async () => {
        try{
            this.setRequestGet(this.userId!);

            if(this.userId !== undefined && this.userId !== ''){
                const responce = await apiBasket.getItems(this.requestGet);
                if(responce.data !== null){
                    this.items = responce.data;
                    this.setAmount();
                    this.setPrice();                    
                }                
            }
            else {
                console.log("error user id from basket")
            }                 
        }
        catch (error) {
            console.assert(error)            
        }
    }

    async addItem(item: IItemForBasket) {
        try{
            if(this.userId !== undefined && this.userId !== '')
            {                
                this.items.push(item);
                
                this.setAmount();
                this.setPrice();
                       
                this.setRequestAdd();

                await apiBasket.addItems(this.requestAdd);
                await this.prefetchData(); 
            } 
            else {
               alert('registrate account or sing in')
            }                   
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
            this.setRequestAdd();

            await apiBasket.addItems(this.requestAdd);
            await this.prefetchData();
        }
        catch (error){
            console.log(error)
        }
    }

    setAmount(){
        this.amount = this.items.length;
    }

    getAmount(){
        return this.amount;
    }

    setPrice(){
        this.totalPrice = this.items.map(item => item.price).reduce((acc, curr) => acc + curr, 0);
    }

    getPrice(){
        return this.totalPrice;
    }

    setError(error:string){
        this.error = error;
    }

    getError() {
        return this.error;
    }

    setRequestAdd(){
        if(this.userId !== undefined)  {
            this.requestAdd.userId = this.userId;
            this.requestAdd.item = this.items;
        }
        else{
            console.log("userId is undefined")
        }        
    }

    setRequestGet(userId:string){
        this.requestGet.userId = userId;
    }
    
}

export default BasketStore;