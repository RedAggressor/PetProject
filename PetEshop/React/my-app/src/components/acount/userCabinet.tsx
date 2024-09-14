import { observer } from "mobx-react-lite"
import { FC, ReactElement, useContext } from "react";
import { AppContext } from "../../App";
import UserCard from "./userCard";
import { Box, Container, Grid } from "@mui/material";
import UserStore from "./userStore";
import { basketstore } from "../basket/Basket";

const userStore = new UserStore();

const UserCabinet: FC<any> = (): ReactElement =>{

    const appStore = useContext(AppContext);
    const basketStore = useContext(basketstore);   

    userStore.setUser(appStore.user);
       
    return (
        <>
            <UserCard user={appStore.user}/>
            List Order
            <Container> 
        <Box sx={{ width: '100%', typography: 'body1' }}> 
            <Grid container spacing={3} justifyContent='center' my={4}>                
                <>
                    { Array.isArray(userStore.getOrderList()) && 
                           userStore.getOrderList()                     
                    }              
                </>                
            </Grid>
        </Box> 
    </Container>   
        </>
    );
}

export default observer(UserCabinet);