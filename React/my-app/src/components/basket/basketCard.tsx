import { FC, useContext } from "react";
import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { IItemForBasket } from "../../api/responce/IItemForBasket";
import { basketContext } from "../../App";

const BasketCard: FC< {item : IItemForBasket} & { index: number }> = ({ item, index }) => {

    const basketStore = useContext(basketContext);
    
    return (
        <Card sx={{maxWidth: 250}}>
            <CardActionArea>
                <CardMedia
                    component='img'
                    height='250'
                    image={item.pictureUrl}
                    alt={item.name}
                />               
                <CardContent>
                    <Typography noWrap gutterBottom variant="h6" component='div'>
                        itemId: {item.id} 
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Price:{item.price}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {item.description} {item.type}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {item.name} {item.availableStock}
                    </Typography>
                </CardContent>                               
            </CardActionArea>
            <button onClick={() => basketStore.basket.removeItem(index)}>Delete from Basket</button>
            <button onClick={() => basketStore.basket.addItem(item)}>Add to Basket</button>
        </Card>
    );
}

export default BasketCard;