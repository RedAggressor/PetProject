import { User} from "oidc-client";
import { makeAutoObservable, runInAction } from "mobx";
import { IOrderByUserIdResponce, IOrderList } from "../../api/request/orderRR";
import * as apiOrder from "../../api/moduls/order";



class UserStore {
    user: User | null | undefined = null;
    userId: string | undefined = '';
    isUserLoaded = false;
    orderList : IOrderList[] = [];

    
    constructor() {
        makeAutoObservable(this);
        runInAction(async()=>await this.prefentchOrderList());            
    };

    async prefentchOrderList(){
        try{
            console.log('user id for order >>>>>>>', this.userId);
            if(this.userId !== undefined && this.userId !== ''){
                const responce: IOrderByUserIdResponce = await apiOrder.getOrderByUserId(this.userId);
                console.log('response  ------------>>>>',responce);
                if(responce.respCode === 1){
                    this.setOrderList(responce.data);
                }
            }
            else {
                console.log('!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!');
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

    setUserId(userId: string | undefined) {
        this.userId = userId;
    };

    getUserId() {
        return this.userId;
    };

    async setUser(user:User| null) {
        try {  
            if(user instanceof User)  
                {
                    this.user = user;
                console.log(this.user);
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