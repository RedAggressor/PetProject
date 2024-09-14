import { User} from "oidc-client";
import { makeAutoObservable, runInAction } from "mobx";
import { IOrderByUserIdResponce, IOrderList } from "../../api/request/orderRR";
import * as apiOrder from "../../api/moduls/order";

class UserStore {
    user: User | null = null;
    isUserLoaded = false;
    orderList : IOrderList[] = [];
    
    constructor() {
        makeAutoObservable(this);
        runInAction(async()=>await this.prefentchOrderList());            
    };

    async prefentchOrderList(){
        try{
            if(this.user !== null){
                const responce: IOrderByUserIdResponce = await apiOrder.getOrderByUserId();
                if(responce.respCode === 1){
                
                    this.setOrderList(responce.data);
                }
            }      
        }
        catch(error){
            console.log(error)
        }
    }

    setOrderList(orderList:IOrderList[]){
        this.orderList = orderList;
    }
    
    getOrderList(){        
        return this.orderList;
    }

    async setUser(user:User| null) {
        try {  
            if(user instanceof User){
                this.user = user;               
                this.isUserLoaded = true; 
            }
            else {
                this.user = null;
                this.isUserLoaded = false;
            }                       
                   
        } catch (error) {
            console.error('Error fetching user:', error);
            this.user = null;
            this.isUserLoaded = false; 
        }
    };    
}

export default UserStore;