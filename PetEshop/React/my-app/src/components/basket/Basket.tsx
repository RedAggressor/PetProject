import { Box, Button, Container, Grid, styled } from "@mui/material";
import { observer } from "mobx-react";
import { FC, ReactElement, useContext } from "react";
import BasketCard from "./basketCard";
import PaymentStore from "../payment/paymentStore";
import BasketStore from "./BasketStore";
import { IBasketStore } from "../../interfaces/backetStor";
import { createContext } from "react";
import { AppContext } from "../../App";

const paymentstore = new PaymentStore();

const storeBasket : IBasketStore = {
  'basket': new BasketStore()
}

export const basketstore = createContext(storeBasket);

const Basket: FC<any> = (): ReactElement => {
  
  const appStore = useContext(AppContext);
  storeBasket.basket.setUserId(appStore.user?.profile.sub);

  const Div = styled('div')(({ theme }) => ({
    ...theme.typography.button,
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(1),
  }));

  const createOrderForPurch = async () => {
    await storeBasket.basket.createOrder();
    paymentstore.setAmount(storeBasket.basket.getPrice());
    paymentstore.setOrderId(storeBasket.basket.getOrderId());
    await paymentstore.getResponse();
  };

  const clearBasket = async () => {
    await storeBasket.basket.clearItems();
    paymentstore.setData(null);
  }

  return (
    <Container>      
      <Box sx={{ width: '100%', typography: 'body1' }}>
        <Grid container spacing={3} justifyContent='center' my={4}>
          <>
            {storeBasket.basket.getItems() && storeBasket.basket.getItems()?.map((item, index) => (
              <Grid key={item.id} item lg={2} md={3} xs={6}>
                <BasketCard item={item} index={index} remove={storeBasket.basket.removeItem} add={storeBasket.basket.addItemToBasket} />
              </Grid>
            ))}
          </>
        </Grid>
      </Box>
        <Div> Total Price: {storeBasket.basket.getPrice()}</Div>
        <Button variant="contained" color="success" onClick={clearBasket}>Clear Basket</Button>
        {appStore.user ? 
          <Div>
            {!paymentstore.getData() && 
              <Button variant="contained" color="success" onClick={createOrderForPurch}>make purches for {storeBasket.basket.getPrice()}</Button>}
            {paymentstore.getData() && 
             <form method="POST" action="https://www.liqpay.ua/api/3/checkout" acceptCharset="utf-8">
              <input type="hidden" name="data" value={paymentstore.getData()!} />
              <input type="hidden" name="signature" value={paymentstore.getSignature()} />
              <input type="image" src="//static.liqpay.ua/buttons/payUk.png" alt="payment" />        
            </form> }
            </Div>
        : <Button variant="contained" color="success" onClick={async()=> await appStore.login()}>sing in for buy</Button>}
    </Container>
  );
}

export default observer(Basket);