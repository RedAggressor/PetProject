import { Box, CssBaseline } from "@mui/material";
import { FC, ReactNode } from "react";
import Footer from "../Footer/Footer";
import Navbar from "../Navbar/Navbar";

interface LayoutProps {
    children: ReactNode;
}

const Layout: FC<LayoutProps> = ({children}) => {
    return (
        <>
            <CssBaseline/>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: "flex-start",
                        minHeight: "100vh",
                        maxWidth: "100vw",
                        flexGrow: 1,
                        flexDirection: 'column',                       
                    }}
                >                    
                    <Navbar />      
                    {children}      
                    <Footer />                           
                </Box>
        </>
    );
}

export default Layout;