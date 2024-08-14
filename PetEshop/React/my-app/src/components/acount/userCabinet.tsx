import { observer } from "mobx-react-lite"
import { FC, ReactElement, useContext } from "react";
import UserStore from "./userStore";
import { AppStoreContext } from "../../App";
import UserCard from "./userCard";
import { Box, Container, Grid } from "@mui/material";

const userStore = new UserStore();

const UserCabinet: FC<any> = (): ReactElement =>{

    const appStore = useContext(AppStoreContext);
    userStore.setUser(appStore.user);
    const user = appStore.user;
       
    return (
        <>
            <UserCard user={user}/>
            List Order
            <Container> 
        {appStore.user?.profile.sub}       
        <Box sx={{ width: '100%', typography: 'body1' }}> 
            <Grid container spacing={3} justifyContent='center' my={4}>                
                <>
                    { Array.isArray(userStore.orderList) && 
                           userStore.orderList                     
                    }              
                </>                
            </Grid>
        </Box> 
    </Container>   
        </>
    );
}

export default observer(UserCabinet);