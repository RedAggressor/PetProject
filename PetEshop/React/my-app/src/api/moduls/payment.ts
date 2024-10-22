import {apiClient, apiClientFormUrlEncoded} from "../client"

const bassPath = "http://www.fruitshop.com:5008/api/v1/Payment/";
const liqpayPath = "https://www.liqpay.ua/api/request";

export const getPeymentLink = async (paymentModel:IPaymentRequest) => await apiClient({
    path:`${bassPath}GetPaymentLink`,
    options: {method:'POST'},
    data: paymentModel    
})

export const getOrderStatus = async (orderStatusModel: IOrderStatusRequest) => await apiClient({
    path:`${bassPath}GetOrderStatus`,
    options: {method:'POST'},
    data: orderStatusModel
})

export interface IPaymentRequest{    
    "amount": number, 
    "currency" : string,
    "description" : string,
    "order_id" : string
}

export interface IPaymentResponse{
    "data":string,
    "errorMessage": null,
    "respCode": number
}

export interface IOrderStatusRequest{    
    "orderid": string
}