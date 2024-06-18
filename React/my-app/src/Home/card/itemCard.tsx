import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { FC, ReactElement } from "react";
import { IItemResponce } from "../../api/responce/ItemsResponce";



const ItemCard: FC<IItemResponce> = (props) : ReactElement => {
    

    return (
        <Card sx={{maxWidth: 250}}>
            <CardActionArea >
            <CardMedia
                    component='img'
                    height='250'
                    image={props.pictureUrl}
                    alt={props.name}
                />               
                <CardContent>
                    <Typography noWrap gutterBottom variant="h6" component='div'>
                        {props.id} 
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.price}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.description} {props.catalogType.type}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.name} {props.availableStock}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}

export default ItemCard;