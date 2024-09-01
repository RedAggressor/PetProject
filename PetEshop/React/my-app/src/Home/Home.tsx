import { Box, CardContent, Container, Grid, Pagination, Tab, Typography} from "@mui/material";
import { FC, ReactElement, useContext, useEffect, useState } from "react";
import {observer} from "mobx-react-lite";
import HomeStore from "./HomeStore";
import ItemCard from "./card/itemCard";
import TabList from "@mui/lab/TabList";
import TabContext from "@mui/lab/TabContext";
import TabPanel from "@mui/lab/TabPanel";
import { AppStoreContext, basketContext } from "../App";
import UserStore from "../components/acount/userStore";

const store = new HomeStore();

const Home: FC<any> = (): ReactElement => {        

const [value, setValue] = useState('0');
const basketStore = useContext(basketContext);
const authStore = useContext(AppStoreContext);
const userStore = new UserStore();

useEffect(() =>  {    
    basketStore.basket.setUserId(authStore.user?.profile.sub ?? '');
    basketStore.basket.prefetchData();
    userStore.setUserId(authStore.user?.profile.sub);
    userStore.prefentchOrderList();
    console.log('user id is changed');
  }, [authStore.user?.profile])

const handleChange = (event: React.SyntheticEvent, newValue: string) => {

    setValue(newValue);};  

    return ( 
        <Container>                      
            <Box sx={{ width: '100%', typography: 'body1' }}>
                <TabContext value={value} >
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={handleChange}  aria-label="lab API tabs example">
                            <Tab value="0" label={"All product"} onClick={() => store.setType(0)}/>
                            {store.listType.map((tab) => (
                                <Tab key={tab.id} label={tab.type} value={`${tab.id}`} onClick={() => {store.setCorrentPage(1); store.setType(tab.id);}}/>
                            ))} 
                        </TabList>
                    </Box>
                    <TabPanel value={`0`}>
                        <Container>
                            <Typography>{store.count}</Typography>
                            <Box sx={{ width: '100%', typography: 'body1' }}>
                                <Grid container spacing={3} justifyContent='center' my={4}>
                                    <>
                                        {Array.isArray(store.items) &&
                                            store.items.map((item) => (
                                                <Grid key={item.id} item lg={2} md={3} xs={6}>
                                                    <ItemCard {...item} />
                                                </Grid>
                                            ))
                                        }
                                    </>
                                </Grid>
                                <Box
                                    sx={{
                                        display: 'flex',
                                        justifyContent: 'center',
                                    }}
                                >
                                    <Pagination 
                                        count={store.totalPages}
                                        page={store.currentPage}
                                        onChange={(event, page) => store.changePage(page)}
                                    />
                                </Box>
                            </Box>
                        </Container>
                    </TabPanel>
                    {store.listType.map((tab) => (
                        <TabPanel key={tab.id} value={`${tab.id}`}>
                            <Container>
                                <Typography>{store.count}</Typography>
                                <Box sx={{ width: '100%', typography: 'body1' }}>
                                    <Grid container spacing={3} justifyContent='center' my={4}>
                                        <>
                                            {Array.isArray(store.items) &&
                                                store.items.map((item) => (
                                                    <Grid key={item.id} item lg={2} md={3} xs={6}>
                                                        <ItemCard {...item} />
                                                    </Grid>
                                                ))
                                            }
                                        </>
                                    </Grid>
                                    <Box  sx={{ display: 'flex',  justifyContent: 'center', }} >
                                        <Pagination                
                                            count={store.totalPages}
                                            page={store.currentPage}
                                            onChange={(event, page) => { store.changePage(page);}}
                                        />
                                    </Box>
                                </Box>
                            </Container>
                        </TabPanel>
                    ))}    
                </TabContext>
            </Box>
        </Container>
    );
};

export default observer(Home);

