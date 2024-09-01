export interface ICatalogItemResponse{
    
    "id": number,
    "name": string,
    "description": string,
    "price": number,
    "pictureUrl": string,
    "type": {
        "id": number,
        "type": string,        
    },
    "availableStock": number,    
}