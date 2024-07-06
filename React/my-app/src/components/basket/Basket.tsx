import { Box, Button, Container, Grid, Stack, styled} from "@mui/material";
import { observer } from "mobx-react";
import { FC, ReactElement, useContext} from "react";
import BasketCard from "./basketCard";
import { AppStoreContext, basketContext } from "../../App";
import * as crypto from 'crypto';
const Basket: FC<any> = (): ReactElement => {

    const basketstore = useContext(basketContext);
    //const paymentstore = new PaymentStore();    

   
    const appStore = useContext(AppStoreContext);   

    const Div = styled('div')(({ theme }) => ({
        ...theme.typography.button,
        backgroundColor: theme.palette.background.paper,
        padding: theme.spacing(1),
      }));

    function base64_encode(input: string): string {

        return Buffer.from(input).toString('base64');       
    }  
    
    function sha1(input: string): string {

        const sha1 = crypto.createHash('sha1');
        sha1.update(input);
        return sha1.digest('base64');
    }

    function generate_signature(private_key: string, data: string): string {
       
        const sign_string = private_key + data + private_key;
        const signature = base64_encode(sha1(private_key + data + private_key));
        return signature;        
    }
    
    const public_key = "sandbox_i57108353826";
    const private_key = "sandbox_S7ubZnIOYz54ZXF5ctR4wc5zAsb8vJzwVz8UrOZM";
                        
    const version = 3;
    const action = "pay";
    const amount = 3;
    const currency = 'UAH';
    const description = 'test pay';
    const order_id = '000002';
    
    const json_string = { public_key: "sandbox_i57108353826", private_key: "sandbox_S7ubZnIOYz54ZXF5ctR4wc5zAsb8vJzwVz8UrOZM", version: version, action: action, amount: amount, currency: currency, description: description, order_id: order_id }
    console.log(json_string);
    //const signature = '+vbH/5mavGchjwncvUTUABJ5hBM='
                            

                 //eyJwdWJsaWNfa2V5Ijoic2FuZGJveF9pNTcxMDgzNTM4MjYiLCJwcml2YXRlX2tleSI6InNhbmRib3hfUzd1YlpuSU9ZejU0WlhGNWN0UjR3YzV6QXNiOHZKendWejhVck9aTSIsInZlcnNpb24iOjMsImFjdGlvbiI6InBheSIsImFtb3VudCI6MywiY3VycmVuY3kiOiJVQUgiLCJkZXNjcmlwdGlvbiI6InRlc3QiLCJvcmRlcl9pZCI6IjAwMDAwMiJ9 
   //const data = 'eyJwdWJsaWNfa2V5Ijoic2FuZGJveF9pNTcxMDgzNTM4MjYiLCJwcml2YXRlX2tleSI6InNhbmRib3hfUzd1YlpuSU9ZejU0WlhGNWN0UjR3YzV6QXNiOHZKendWejhVck9aTSIsInZlcnNpb24iOjMsImFjdGlvbiI6InBheSIsImFtb3VudCI6MywiY3VycmVuY3kiOiJVQUgiLCJkZXNjcmlwdGlvbiI6InRlc3QiLCJvcmRlcl9pZCI6IjAwMDAwMiJ9'
    
    const data = base64_encode(JSON.stringify(json_string));    
    
    const signature = generate_signature(private_key, data);   

  return (  
    <Container> 
        {appStore.user?.profile.sub}       
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
         Buy
      </Button>
      <Button variant="contained" color="success" onClick={async()=> {await basketstore.basket.clearItems()}}>Clear Basket</Button>
      <Div> Total Price: {basketstore.basket.getPrice()}</Div> 
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