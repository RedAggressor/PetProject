export interface IOrderAddResponse{    
    "errorMessage": string,
    "respCode": number,
    "id": number      
}

export interface IOrdergetByIdResponce{

        "errorMessage": string,
        "respCode": number,
        "id": number,
        "orderItems": [
          {
            "id": number,
            "count": number,
            "itemId":number,
            "item": {              
              "id": number,
              "name": string,
              "description": string,
              "price": number,
              "pictureUrl": string,
              "type": {
                "id": number,
                "type": string
              },
              "availableStock": number
            }
        }
    ]    
}

export interface IOrderByUserIdResponce{
    "errorMessage": string,
    "respCode": number,
    "data": [
      {        
        "id": number,
        "userId": string,
        "status": number,
        "orderItems": [
          {
            "id": number,
            "count": number,
            "itemId":number,
            "item": {
              "id": number,
              "name": string,
              "description": string,
              "price": number,
              "pictureUrl": string,
              "type": {                
                "id": number,
                "type": string
              },
              "availableStock": number
            }
          }
        ]
      }
    ]
  }

  export interface IOrderList{   
        "id": number,
        "userId": string,
        "status": number,
        "orderItems": [
          {
            "id": number,
            "count": number,
            "itemId":number,
            "item": {
              "id": number,
              "name": string,
              "description": string,
              "price": number,
              "pictureUrl": string,
              "type": {                
                "id": number,
                "type": string
              },
              "availableStock": number
            }
          }
        ]    
  }

  export interface IItemforOrder {       
      "id": number,
      "name": string,
      "description": string,
      "price": number,
      "pictureUrl": string,
      "type": { 
        "type": string
      },
      "availableStock": number
   }

   export interface IOrderItem{   
      "orderId": string,      
      "orderItems": 
        {         
          "count": number,
          "itemId": number          
        }[]            
   }
  