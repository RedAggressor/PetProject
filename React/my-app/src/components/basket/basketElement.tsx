import * as React from 'react';
import Badge, { BadgeProps } from '@mui/material/Badge';
import { styled } from '@mui/material/styles';
import IconButton from '@mui/material/IconButton';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import BasketStore from './BasketStore';
import { useContext } from 'react';
import { basketContext } from './Basket';

const StyledBadge = styled(Badge)<BadgeProps>(({ theme }) => ({
    '& .MuiBadge-badge': {
      right: -3,
      top: 13,
      border: `2px solid ${theme.palette.background.paper}`,
      padding: '0 4px',
    },
  }));

  export default function BasketElement() {

    const storeBasket = useContext(basketContext);
    
    return (
      <IconButton aria-label="cart">        
        <StyledBadge badgeContent={storeBasket.basket.amount} color="secondary">
          <ShoppingCartIcon />
        </StyledBadge>
      </IconButton>
    );
  }