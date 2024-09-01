import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { FC, ReactElement, useContext } from "react";
import { IItemForBasket } from "../../api/responce/IItemForBasket";
import { useNavigate } from "react-router-dom";
import { ICatalogItemResponse } from "../../api/responce/ICatalogItemResponse";
import { basketContext } from "../../App";



const ItemCard: FC< ICatalogItemResponse > = (props) : ReactElement => {

    const basketStore = useContext(basketContext);

    const item: IItemForBasket = {
        id: props.id,
        name: props.name,
        description: props.description,
        price: props.price,
        pictureUrl: props.pictureUrl,
        typeId: props.type.id,
        typeName: props.type.type,
        availableStock: props.availableStock,        
    };

    const puInItem = async () => {  
        await basketStore.basket.addItemBasket(item);       
    };

    const navigate = useNavigate();
    return (
        <Card sx={{maxWidth: 250}}>
            <CardActionArea onClick={() => navigate(`/item/${props.id}`)}>
            <CardMedia
                    component='img'
                    height='250'
                    image={props.pictureUrl}
                    alt={props.name}
                />               
                <CardContent>
                    <Typography noWrap gutterBottom variant="h6" component='div'>
                    {props.name}
                    </Typography>
                    <Typography noWrap gutterBottom variant="h6" component='div'>
                    Price:{props.price}
                    </Typography>                    
                    <Typography variant="body2" color="text.secondary">
                        {props.description} 
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                         {props.availableStock}
                    </Typography>
                </CardContent>                
            </CardActionArea>
            <button onClick={puInItem}>Add to basket</button>
        </Card>
    );
}

export default ItemCard;