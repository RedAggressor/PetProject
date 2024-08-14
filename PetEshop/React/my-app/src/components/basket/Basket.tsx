import { Box, Button, Container, Grid, Stack, styled} from "@mui/material";
import { observer } from "mobx-react";
import { FC, ReactElement, useContext} from "react";
import BasketCard from "./basketCard";
import { AppStoreContext, basketContext } from "../../App";
import * as crypto from 'crypto';
import PaymentStore from "../payment/paymentStore";

const paymentstore = new PaymentStore();

const Basket: FC<any> = (): ReactElement => {

    const basketstore = useContext(basketContext);       
   
    const appStore = useContext(AppStoreContext);   

    const Div = styled('div')(({ theme }) => ({
        ...theme.typography.button,
        backgroundColor: theme.palette.background.paper,
        padding: theme.spacing(1),
      }));    
   
    const data = paymentstore.getData(); 
    const signature = paymentstore.getSignature();
    const orderId = basketstore.basket.order;
    const amount = basketstore.basket.getAmount();
    paymentstore.setAmount(amount);
    paymentstore.setOrderId("000002");

  return (  
    <Container> 
        {appStore.user?.profile.sub} 
        {paymentstore.data}
        {paymentstore.signature}      
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
       
       <Div> Total Amount: {basketstore.basket.getAmount()}</Div>
       <Stack direction="row" spacing={2}>
       <Button onClick={()=>console.log(data, signature)}>request signature</Button>  
      <Button variant="contained" color="success" onClick={async ()=>{ alert("Suucesful"); await basketstore.basket.orderAdd()}} >
      {basketstore.basket.getPrice()} Buy
      </Button>
      <Button variant="contained" color="success" onClick={async()=> {await basketstore.basket.clearItems()}}>Clear Basket</Button>
      <Div> Total Price: {basketstore.basket.getPrice()}</Div> 
      <Button variant="contained" color="success" onClick={async()=> {await paymentstore.getResponse()}}>make payment</Button>      
      <form method="POST" action="https://www.liqpay.ua/api/3/checkout" acceptCharset="utf-8">
    <input type="hidden" name="data" value={data} />
    <input type="hidden" name="signature" value={signature} />
    <input type="image" src="//static.liqpay.ua/buttons/payUk.png" />
    </form>     
    </Stack>
    
    </Container>
    );
}

export default observer(Basket);