import { Box, CardContent, Container, Grid, Pagination, Typography} from "@mui/material";
import { FC, ReactElement, useState } from "react";
import {observer} from "mobx-react-lite";
import HomeStore from "./HomeStore";
import ItemCard from "./card/itemCard";
import BasketStore from "../components/basket/BasketStore";

const store = new HomeStore();
const Home: FC<any> = (): ReactElement => {        

    return (  
    <Container>        
        <Box sx={{ width: '100%', typography: 'body1' }}> 
            <Grid container spacing={3} justifyContent='center' my={4}>                
                    <>
                    { Array.isArray(store.items) && store.items?.map((item) => (
                        <Grid key={item.id} item lg={2} md={3} xs={6}>
                            <ItemCard {...item}/>
                        </Grid>
                    ))}                    
                    </>                
            </Grid>
        </Box>            
    </Container>
    );
};

export default observer(Home);
