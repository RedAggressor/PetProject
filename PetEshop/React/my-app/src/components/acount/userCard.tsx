import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { FC, useContext } from "react";

import { User } from "oidc-client";

const UserCard: FC<{ user: User | null}> = ({ user }) => {    
    
    return (
<Card sx={{maxWidth: 250}}>
<CardActionArea>
    <CardMedia
        component='img'
        height='250'
        image={user?.profile.picture}
        alt={user?.profile.name}
    />               
    <CardContent>
    <Typography noWrap gutterBottom variant="h6" component='div'>
            Id: {user?.profile.sub}
        </Typography>
        <Typography noWrap gutterBottom variant="h6" component='div'>
            Name: {user?.profile.given_name}
        </Typography>
        <Typography noWrap gutterBottom variant="h6" component='div'>
            Famile:{user?.profile.family_name}
        </Typography>
        <Typography noWrap gutterBottom variant="h6" component='div'>
            Email:{user?.profile.email}
        </Typography>
    </CardContent>                               
</CardActionArea>
</Card>
    )}

    export default UserCard;