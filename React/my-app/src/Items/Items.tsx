import {ReactElement, FC, useEffect, useState} from "react";
import {   
    Card,
    CardContent,
    CardMedia,
    CircularProgress,
    Container,
    Grid,   
    Typography
} from '@mui/material'
import * as itemApi from "../api/moduls/items";
import {useParams} from "react-router-dom";
import { IItemForBasket} from "../api/responce/IItemForBasket";
import { observer } from "mobx-react";
import BasketStore from "../components/basket/BasketStore";

const Items: FC<any> = (): ReactElement => {
    const [item, setItem] = useState<IItemForBasket | null>(null);
    const [isLoading, setisLoading] = useState<boolean>(false)
    const {id} = useParams();    
    const basketStore = new BasketStore(); 

    useEffect(() => {
        if(id) {
            const getItem = async () => {
                try {
                    setisLoading(true);
                    const respon = await itemApi.getItemById(id)
                    setItem(respon)
                    
                    
                } catch (error) {
                    if (error instanceof Error) {
                        console.error(error.message)
                    }
                }
                setisLoading(false);
            }
            getItem()
        }
    }, [id])

    const items: IItemForBasket[] = [];
    if(item){
        items.push(item);
    }
    

    const put = async()=> await basketStore.putInBasket(items);

    return (
        <Container>
            <Grid container spacing={3} justifyContent='center' m={4}>
                {isLoading ? (
                    <CircularProgress/>
                    
                ) : (
                    <>
                    <Card sx={{ maxWidth: 250}}>
                        <CardMedia
                            component='img'
                            height='250'
                            image={item?.pictureUrl}
                            alt={item?.name}
                        />
                        <CardContent>
                            <Typography noWrap gutterBottom variant="h6" component='div'>
                                {item?.name} {item?.catalogTypeId} {item?.description}
                            </Typography>
                            <Typography variant="body2" color="text.secondary">
                                {item?.price}
                            </Typography>
                        </CardContent>
                    </Card>
                    </>
                )}
            </Grid>

            <button onClick={put}>click</button>
        </Container>
    );
}

export default observer(Items);