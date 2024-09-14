import {apiClient} from "../client"

const bassPath = "http://www.fruitshop.com:5008/Payment";

export const getPeymentOption = async (paymentModel:IPaymentRequest) => await apiClient({
    path:`${bassPath}`,
    options: {method:'POST'},
    data: paymentModel    
})

export interface IPaymentRequest{
    "version" : number, 
    "action" : string,
    "amount": number, 
    "currency" : string,
    "description" : string,
    "order_id" : string
}

export interface IPaymentResponse{
    "data":string,
    "signature":string
}