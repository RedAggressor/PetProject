export interface IItemByPageResponce{
    "errorMessage": string | null,
    "respCode": number,
    "pageIndex": number,
    "pageSize": number,
    "count": number,
    "data": [
      {
        "errorMessage": string | null,
        "respCode": number,
        "id": number,
        "name": string,
        "description": string,
        "price": number,
        "pictureUrl": string,
        "type": {
          "errorMessage": string | null,
          "respCode": number,
          "id": number,
          "type": string
        },
        "availableStock": number
      }
    ]
  }