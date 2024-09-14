import {ReactElement, FC, useEffect, useState, useContext} from "react";
import {   
    Button,
    CardMedia,
    Container,
    Grid,   
    Typography
} from '@mui/material'
import * as itemApi from "../api/moduls/items";
import {useParams} from "react-router-dom";
import { observer } from "mobx-react";
import { basketstore } from "../components/basket/Basket";

const Items: FC<any> = (): ReactElement => {
    const basketStore = useContext(basketstore);
    const [item, setItem] = useState<any | null>(null);
    const [isLoading, setisLoading] = useState<boolean>(false);
    const {id} = useParams();    
     

    useEffect(() => {
        if(id) {
            const getItem = async () => {
                try {
                    setisLoading(true);
                    const respon = await itemApi.getItemById(id);
                    setItem(respon.itemDto);                    
                    
                } catch (error) {
                    if (error instanceof Error) {
                        console.error(error.message);
                    }
                }
                setisLoading(false);
            }
            getItem()
        }
    }, [id])

    const addItemToBasket = async () => {            
            await basketStore.basket.addItemToBasket(item);
        };

    return (
        <Container>
            <Grid container spacing={3} justifyContent='center' m={4}>                
                    <>
                    <Container sx={{maxWidth:'50', maxHeight:'50'}}>
                        <CardMedia
                            component='img'                                                                            
                            image={item?.pictureUrl}
                            alt={item?.name}
                        />
                    </Container>
                        <Container>                       
                            <Typography noWrap gutterBottom variant="h6" component='div'>
                                {item?.name} 
                            </Typography>
                        </Container>
                        <Container>                        
                            <Typography noWrap gutterBottom variant="h6" component='div'>
                                {item?.description}
                            </Typography>
                        </Container>
                        <Container>                        
                            <Typography variant="h3" color="text.secondary">
                                Price: {item?.price}
                            </Typography>
                        </Container>
                        <Container>
                        <Button variant="contained" color="success" onClick={addItemToBasket}>Add to basket</Button> 
                        </Container>                        
                    </>                
            </Grid>                 
        </Container>
    );
}

export default observer(Items);