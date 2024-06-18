export interface IItemResponce {

        "id": number,
        "name": string,
        "description": string,
        "price": number,
        "pictureUrl": string,
        "catalogType": {
            "id": number,
            "type": string,
            "errorMessage": string,
            "respCode": string           
        },        
        "availableStock": number,
        "errorMessage": string,
        "respCode": string      
}