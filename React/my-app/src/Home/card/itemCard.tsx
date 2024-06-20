import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { FC, ReactElement, useContext } from "react";
import { IItemForBasket } from "../../api/responce/IItemForBasket";
import { useNavigate } from "react-router-dom";
import { ICatalogItemResponse } from "../../api/responce/ICatalogItemResponse";
import BasketStore from "../../components/basket/BasketStore";
import { basketContext } from "../../components/basket/Basket";



const ItemCard: FC<ICatalogItemResponse> = (props) : ReactElement => {

    const basketStore = useContext(basketContext);

    const item: IItemForBasket = {
        id: props.id,
        name: props.name,
        description: props.description,
        price: props.price,
        pictureUrl: props.pictureUrl,
        catalogTypeId: props.catalogType.id,
        availableStock: props.availableStock
    };
    const puInItem = async () => {        
        await  basketStore.basket.putInBasket([item]);
        await basketStore.basket.prefetchData();
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
                        itemId: {props.id} 
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Price:{props.price}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.description} typeId:{props.catalogType.type}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.name} {props.availableStock}
                    </Typography>
                </CardContent>                
            </CardActionArea>
            <button onClick={puInItem}>Add to basket</button>
        </Card>
    );
}

export default ItemCard;