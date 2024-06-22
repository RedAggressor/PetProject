export interface IItemByPageResponce{
    "errorMessage": string,
    "respCode": number,
    "pageIndex": number,
    "pageSize": number,
    "count": number,
    "data": [
      {
        "errorMessage": string,
        "respCode": number,
        "id": number,
        "name": string,
        "description": string,
        "price": number,
        "pictureUrl": string,
        "catalogType": {
          "errorMessage": string,
          "respCode": number,
          "id": number,
          "type": string
        },
        "availableStock": number
      }
    ]
  }