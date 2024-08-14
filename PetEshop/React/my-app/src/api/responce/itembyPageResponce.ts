export interface IItemByPageResponce{
    "errorMessage": string | null,
    "respCode": number,
    "pageIndex": number,
    "pageSize": number,
    "count": number,
    "data": [
      {        
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
    ]
  }