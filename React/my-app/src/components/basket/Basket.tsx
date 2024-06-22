import { Box, Container, Grid, Typography, styled} from "@mui/material";
import { observer } from "mobx-react";
import { FC, ReactElement, createContext} from "react";
import BasketStore from "./BasketStore";
import BasketCard from "./basketCard";
import { IBasketStore } from "../../interfaces/backetStor";

const store : IBasketStore = {
  'basket': new BasketStore()
}

export const basketContext = createContext(store)
  
const Basket: FC<any> = (): ReactElement => {
    
    const Div = styled('div')(({ theme }) => ({
        ...theme.typography.button,
        backgroundColor: theme.palette.background.paper,
        padding: theme.spacing(1),
      }));

  return (  
    <Container>        
        <Box sx={{ width: '100%', typography: 'body1' }}> 
            <Grid container spacing={3} justifyContent='center' my={4}>                
                    <>
                    { Array.isArray(store.basket.items) && store.basket.items?.map((item, index) => (
                        <Grid key={item.id} item lg={2} md={3} xs={6}>
                            <BasketCard item={item} index={index} />
                        </Grid>
                    ))}                    
                    </>                
            </Grid>
        </Box>
       <Div> Total Price: {store.basket.price}</Div>
       <Div> Total Amount: {store.basket.amount}</Div>
    </Container>
    );
}

export default observer(Basket);