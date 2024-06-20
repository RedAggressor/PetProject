import { createContext, useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from './Layout/Layout';
import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import AuthStore from './Authentification/AuthStore';
import { IAppStore } from './interfaces/appStore';
import { routes as appRoutes } from "./routes";

const store: IAppStore = {
  'authStore': new AuthStore()
}

export const AppStoreContext = createContext(store)

function App() {  
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

  const [appStore, setAppStore] = useState(store);
  
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline>
        <Router>
          <AppStoreContext.Provider value={appStore}>
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
          </AppStoreContext.Provider>
        </Router>
      </CssBaseline>
    </ThemeProvider>
  );
}

export default App;
