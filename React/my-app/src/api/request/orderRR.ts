export interface IOrderAddRequest{
    "userId": string,
    "items": {"count": number, "itemId": number} [ ]
  }

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
            "item": {
              "errorMessage": string,
              "respCode": number,
              "id": number,
              "name": string,
              "description": string,
              "price": number,
              "pictureUrl": string,
              "type": {
                "errorMessage": string,
                "respCode": number,
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
        "errorMessage": string,
        "respCode": number,
        "id": number,
        "orderItems": [
          {
            "id": number,
            "count": number,
            "item": {
              "errorMessage": string,
              "respCode": number,
              "id": number,
              "name": string,
              "description": string,
              "price": number,
              "pictureUrl": string,
              "type": {
                "errorMessage": string,
                "respCode": number,
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
      
        "errorMessage": string,
        "respCode": number,
        "id": number,
        "orderItems": [
          {
            "id": number,
            "count": number,
            "item": {              
              "errorMessage": string,
              "respCode": number,
              "id": number,
              "name": string,
              "description": string,
              "price": number,
              "pictureUrl": string,
              "type": {
                "errorMessage": string,
                "respCode": number,
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
    "id": number,
    "count": number,
    "item": {              
      "errorMessage": string,
      "respCode": number,
      "id": number,
      "name": string,
      "description": string,
      "price": number,
      "pictureUrl": string,
      "type": {
        "errorMessage": string,
        "respCode": number,
        "id": number,
        "type": string
      },
      "availableStock": number
    }
   }
  