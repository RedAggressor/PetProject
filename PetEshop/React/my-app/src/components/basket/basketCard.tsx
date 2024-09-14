import { Button, Card, CardActionArea, CardContent, CardMedia, Grid, Typography } from "@mui/material";
import { IItemForBasket } from "../../api/responce/IItemForBasket";

interface BasketCardProps {
    item: IItemForBasket;
    index: number;
    remove: (index: number) => void;
    add: (item: IItemForBasket) => void;
  }

const BasketCard : React.FC<BasketCardProps> = ({ item, index, remove, add }) => {
    
    return (
        <Card sx={{maxWidth: 400}}>
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
                        {item.description} {item.typeName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {item.name} {item.availableStock}
                    </Typography>
                </CardContent>                               
            </CardActionArea>
            <Grid>
            <Button variant="contained" color="success" onClick={() => remove(index)}>Delete from Basket</Button>           
            <Button variant="contained" color="success" onClick={() => add(item)}>Add More</Button>            
            </Grid>
        </Card>
    );
}

export default BasketCard;