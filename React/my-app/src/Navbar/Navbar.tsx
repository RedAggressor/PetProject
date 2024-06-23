import {FC, ReactElement, useContext, useEffect, useState} from "react";
import { Box, Link, Container, IconButton, Menu, MenuItem,
  Toolbar, Typography,
  ListItemIcon, } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { routes } from "../routes";
import { NavLink, useNavigate } from "react-router-dom";
import { AppStoreContext } from "../App";
import {observer} from "mobx-react-lite";
import Acount from "../components/account";
import BasketElement from "../components/basket/basketElement";
import { basketContext } from "../App";

const Navbar: FC = (): ReactElement => {

  const basketStore = useContext(basketContext);
  const [amount, setAmount] = useState(basketStore.basket.amount);

  useEffect(()=>{
     setAmount(basketStore.basket.amount)},
  [basketStore.basket.amount]);

  const appStore = useContext(AppStoreContext);

  const [anchorElNav, setAnchorElNav] = useState(null);

  const handleOpenNavMenu = (event: any) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };  
  
  const navigate = useNavigate();
  const HandlClick = async () => {navigate('/Basket')};

  return (
    <Box
      sx={{
        width: "100%",
        height: "auto",
        backgroundColor: "grey",
      }}
    >
      <Container maxWidth="xl">
        <Toolbar disableGutters>          
          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: "block", md: "none" },
              }}
            >
              {routes.map((page) => (
              !!page.enabled && <Link
                  key={page.key}
                  component={NavLink}
                  to={page.path}
                  color="black"
                  underline="none"
                  variant="button"
                >
                  <MenuItem onClick={handleCloseNavMenu}>
                  <Typography textAlign="center">{page.title}</Typography>
                  </MenuItem>                  
                </Link>
              ))}
              
              
            </Menu> 
                       
          </Box>  
               
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}
          >
            {!!appStore.user?.access_token && <span>{<Acount/>}</span> } Тут
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            <Box
              sx={{
                display: "flex",
                flexDirection: "row",
                justifyContent: "flex-start",
                alignItems: "center",
                marginLeft: "1rem",
              }}
            >
              {routes.map((page) => (
               !!page.enabled && <Link
                  key={page.key}
                  component={NavLink}
                  to={page.path}
                  color="black"
                  underline="none"
                  variant="button"
                  sx={{ fontSize: "large", marginLeft: "2rem" }}
                >
                  {page.title}
                </Link>
              ))}
            </Box>
          </Box>         
          <Typography
            variant="h6"
            noWrap
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
            }}
          >
            {!!appStore.user?.access_token ?
                (<span>{<Acount/>}</span> ) :
                (<Link                  
                  color="black"
                  underline="none"
                  variant="button"
                >
                  <MenuItem onClick={() => appStore.login()}>
                    <Typography textAlign="center">Login</Typography>
                  </MenuItem>                   
                  </Link> 
                ) 
            } 
          </Typography>
          <Box>
            <MenuItem onClick={HandlClick}>
              <ListItemIcon>
                <BasketElement count={amount}/>
              </ListItemIcon>
              Basket
            </MenuItem>
            </Box>       
        </Toolbar>
      </Container>      
    </Box>
  );
};

export default observer(Navbar);
