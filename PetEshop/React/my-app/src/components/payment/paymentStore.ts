import { makeAutoObservable } from "mobx";
import { IPaymentRequest, IPaymentResponse } from "../../api/moduls/payment";
import * as client from "../../api/moduls/payment";

class PaymentStore {
    
version = 3;
action = 'pay';
amount = 0;
currency = 'UAH';
description = 'test pay';
order_id = '';

data = '';
signature = '';
constructor(){
    makeAutoObservable(this)
}

getOrderId(){
    return this.order_id;
}

setOrderId(order_id: string){
    this.order_id = order_id;
}

getAmount(){
    return this.amount;
}

setAmount(amount:number){
    this.amount = amount;
}

getSignature(){
    return this.signature;
}

setSignature(signature: string){
    this.signature = signature;
}

setData(data:string){
    this.data = data;
}

getData(){
    return this.data;
}

request : IPaymentRequest = {    
    "version": this.version,
    "action": this.action,
    "amount": this.amount,
    "currency": this.currency,
    "description": this.description,
    "order_id": this.order_id
}

async getResponse(){
    try{
        const response:IPaymentResponse = await client.getPeymentOption(this.request);
        if(response != null){
            this.setData(response.data);        
            this.setSignature(response.signature);
        }        
    }
    catch(error){
        console.log(error);
    }
}
}

export default PaymentStore;