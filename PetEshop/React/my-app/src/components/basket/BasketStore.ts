import { makeAutoObservable, runInAction } from "mobx";
import { IItemForBasket } from "../../api/responce/IItemForBasket";
import * as apiBasket from "../../api/moduls/basket";
import * as apiOrder from "../../api/moduls/order"
import { IOrderItem } from "../../api/request/orderRR";

class BasketStore {
    amount: number = 0;
    items: IItemForBasket[] = [];
    totalPrice: number = 0;
    error = "";
    userId: string | undefined = "";
    orderId = "";
    
    responseGet: {
        "data": IItemForBasket[],
        "errorMessage": string | null,
        "respCode": number
    } = { data: [], errorMessage: null, respCode: 0 };

    responseAdd: {
        "id": string,
        "errorMessage": string | null,
        "respCode": number
    } = { id: '', errorMessage: null, respCode: 0 };   

    itemsForOrder: {
        count: number,
        itemId: number
    }[] = [];

    order:IOrderItem = {
        orderId: this.getOrderId(),
        orderItems: []
    }

    constructor() {
        makeAutoObservable(this, { responseGet: false, responseAdd: false });
        runInAction(async () => await this.prefetchData())
    }

    async clearItems() {
        try{
            this.setItems([]);

            this.responseAdd = (this.userId !== undefined && this.userId !== '') ? 
                await apiBasket.addItems({items: this.getItems()}) :  
                await apiBasket.addItemsNoJwt({items: this.getItems()});            
                await this.prefetchData();
                
            if (this.responseAdd.respCode === 1) {
                
            }
        } catch (error){
            console.log(error);
        }        
    }

    setOrderId(orderId:string){
        this.orderId = orderId;
    }

    getOrderId(){
        return this.orderId;
    }

    setItems(items: IItemForBasket[]) {
        this.items = items;
    }

    getItems() {
        return this.items;
    }

    setOrderItemToOrder() {
        if (this.items.length > 0) {
            this.order.orderItems = [];
            this.order.orderId = this.getOrderId();
            this.items.map((item) => {
                this.order.orderItems.push({
                    count: 1,
                    itemId: item.id
                })
            });
        }
        else {
            console.log('basket is empty >>>>>', this.items)
        }
    }

    getItemForOrder(){
        return this.itemsForOrder;
    }

    async createOrder() {
        try{
            if (this.userId !== undefined && this.userId !== '') {
                this.responseAdd = await apiOrder.addOrder();
                if(this.responseAdd.respCode === 1) {
                    this.setOrderId(this.responseAdd.id);
                } else {
                    console.log("order id not create or somethink go wrong!");
                }           
            }
        } catch(error){
            console.log(this.error);
        }        
    }

    async addItemToOrder() {
        try {
            this.setOrderItemToOrder();
            this.responseAdd = await apiOrder.addItemsToOrder(this.order);
            if (this.responseAdd.respCode === 1) {                
                this.clearItems();
            }
        }
        catch (eror) {
            console.log(eror)
        }
    }

    getUserId() {
        return this.userId;
    }

    setUserId(userId: string | undefined) {
        this.userId = userId;
    }
    
    prefetchData = async () => {
        try {

            this.responseGet = (this.userId !== undefined && this.userId !== '') ?
                await apiBasket.getItems() :            
                await apiBasket.getItemNoJwt();            

            if (this.responseGet.respCode === 1) {
                this.setItems(this.responseGet.data ?? []);
                this.setAmount();
                this.setPrice();
            }else {
                console.log("bad response from prefetch basket");
            }
        }
        catch (error) {
            console.assert(error)
        }
    }

    async addItemToBasket(item: IItemForBasket) {
        try {
            this.pushItem(item);
            this.setAmount();
            this.setPrice();

            this.responseAdd = (this.userId !== undefined && this.userId !== '') ?
                await apiBasket.addItems({items: this.getItems()}) :
                await apiBasket.addItemsNoJwt({items: this.getItems()});

            if (this.responseAdd.respCode === 1) {
                await this.prefetchData();
            }
            else {
                this.items.pop();
                this.setAmount();
                this.setPrice();
            }
        }
        catch (error) {
            console.log(error);
        }
    }

    pushItem(item: IItemForBasket) {
        if (this.items != null) {
            this.items.push(item);
        } else {
            console.log(" item === null!");
        }
    }

    removeItem = async (id: number) => {
        try {
            this.items.splice(id, 1);
            this.setAmount();
            this.setPrice();

            this.responseAdd = (this.userId !== undefined && this.userId !== '') ?
                await apiBasket.addItems({items: this.getItems()}): 
                await apiBasket.addItemsNoJwt({items: this.getItems()});           

            if (this.responseAdd.respCode === 1) {
                await this.prefetchData();
            }

        }
        catch (error) {
            console.log(error)
        }
    }

    setAmount() {
        this.amount = this.items.length;
    }

    getAmount() {
        return this.amount;
    }

    setPrice() {
        this.totalPrice = this.items.map(item => item.price).reduce((acc, curr) => acc + curr, 0);
    }

    getPrice() {
        return this.totalPrice;
    }

    setError(error: string) {
        this.error = error;
    }

    getError() {
        return this.error;
    }
}

export default BasketStore;