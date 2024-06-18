import { Box, Container, Grid, Typography } from "@mui/material";
import { FC, ReactElement } from "react";

export const Footer: FC = (): ReactElement => {
    return (
        <Box 
            sx={{
                width: "100%",
                height: "auto",
                backgroundColor: "secondary.main",
                paddingTop: "1rem",
                paddingBottom: "1rem",                
            }}
        >
            <Container maxWidth="lg">
                <Grid container direction='column' alignItems='center'>
                    <Grid item xs={12}>
                        <Typography color="black" variant="h5">
                            Fruit Store 
                        </Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography color="textSecondary" variant="subtitle1">
                            {`${new Date().getFullYear()} | Hi | Good Day | Come Back`}
                        </Typography>
                    </Grid>
                </Grid>
            </Container>
        </Box>
    );
 };

 export default Footer;