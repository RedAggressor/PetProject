import { FC, ReactElement } from "react";
import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { IItemForBasket } from "../../api/responce/IItemForBasket";

const BasketCard: FC<IItemForBasket> = (props) : ReactElement => {

    return (
        <Card sx={{maxWidth: 250}}>
            <CardActionArea>
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
                        {props.description} typeId:{props.catalogTypeId}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.name} {props.availableStock}
                    </Typography>
                </CardContent>                
            </CardActionArea>            
        </Card>
    );
}

export default BasketCard;