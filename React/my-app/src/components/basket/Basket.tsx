import { Box, Container, Grid, styled} from "@mui/material";
import { observer } from "mobx-react";
import { FC, ReactElement, useContext} from "react";
import BasketCard from "./basketCard";
import { basketContext } from "../../App";

const Basket: FC<any> = (): ReactElement => {

    const basketstore = useContext(basketContext);

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
                    { Array.isArray(basketstore.basket.items) && basketstore.basket.items?.map((item, index) => (
                        <Grid key={item.id} item lg={2} md={3} xs={6}>
                            <BasketCard item={item} index={index} />
                        </Grid>
                    ))}              
                </>                
            </Grid>
        </Box>
       <Div> Total Price: {basketstore.basket.price}</Div>
       <Div> Total Amount: {basketstore.basket.amount}</Div>
    </Container>
    );
}

export default observer(Basket);