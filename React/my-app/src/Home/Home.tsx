import { Box, CardContent, Container, Grid, Pagination, Typography} from "@mui/material";
import { FC, ReactElement, useState } from "react";
import {observer} from "mobx-react-lite";
import HomeStore from "./HomeStore";
import ItemCard from "./card/itemCard";

const store = new HomeStore();

const Home: FC<any> = (): ReactElement => { 
    
    const handClick = () => {
        alert(store.brands.length)
    }

    return (  
    <Container>
        <Typography>
            Somesthing
        </Typography>
            <Grid container spacing={3} justifyContent='center' my={4}>                
                    <>
                    { Array.isArray(store.brands) && store.brands?.map((item) => (
                        <Grid key={item.id} item lg={2} md={3} xs={6}>
                            <ItemCard {...item}/>
                        </Grid>
                    ))}

                    <button onClick={handClick}>click</button>
                    </>                
            </Grid>            
        </Container>
    );
};

export default observer(Home);
