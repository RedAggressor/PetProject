export interface ICatalogItemResponse{
    
    "id": number,
    "name": string,
    "description": string,
    "price": number,
    "pictureUrl": string,
    "type": {
        "id": number,
        "type": string,
        "errorMessage": string | null,
        "respCode": number
    },
    "availableStock": number,
    "errorMessage": string,
    "respCode": number   
}