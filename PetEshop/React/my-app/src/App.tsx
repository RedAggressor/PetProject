import { createContext, FC, ReactElement, useContext } from 'react';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from './Layout/Layout';
import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import AuthStore from './Authentification/authStore';
import { routes as appRoutes } from "./routes";
import { observer } from 'mobx-react';

const storeAuth = AuthStore;

export const AppContext = createContext(storeAuth);

const App: FC<any> = (): ReactElement => {   

  const theme = createTheme({
    palette:{
      primary:{
        light: "#63b8ff",
        main: "#0989e3",
        dark: "#005db0",
        contrastText: "#000",
      },
      secondary:{
        main: "#4db6ac",
        light: "#82e9de",
        dark: "#00867d",
        contrastText: "#000",
      },
    },
  });
  
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline>
        <Router>
          <AppContext.Provider value={storeAuth}>
            <Layout>
              <Routes>
                {appRoutes.map((item) => (
                  <Route
                  key={item.key}
                  path={item.path}
                  element={<item.component/>}
                  />
                ))}                
              </Routes>              
            </Layout>
          </AppContext.Provider>
        </Router>
      </CssBaseline>
    </ThemeProvider>
  );
}

export default observer(App);

