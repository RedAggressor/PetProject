import { Box, Container, Grid, Typography} from "@mui/material";
import { observer } from "mobx-react";
import { FC, ReactElement, createContext} from "react";
import BasketStore from "./BasketStore";
import BasketCard from "./basketCard";
import { IBasketStore } from "../../interfaces/backetStor";

//window.location.reload();

const store : IBasketStore = {
  'basket': new BasketStore()
}

export const basketContext = createContext(store)
  
const Basket: FC<any> = (): ReactElement => {
  
  return (  
    <Container>        
        <Box sx={{ width: '100%', typography: 'body1' }}> 
            <Grid container spacing={3} justifyContent='center' my={4}>                
                    <>
                    { Array.isArray(store.basket.items) && store.basket.items?.map((item) => (
                        <Grid key={item.id} item lg={2} md={3} xs={6}>
                            <BasketCard {...item}/>
                        </Grid>
                    ))}                    
                    </>                
            </Grid>
        </Box>                
    </Container>
    );
}

export default observer(Basket);