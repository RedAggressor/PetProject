import Badge, { BadgeProps } from '@mui/material/Badge';
import { styled } from '@mui/material/styles';
import IconButton from '@mui/material/IconButton';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import { FC, ReactElement, useContext, useEffect, useState } from 'react';
import { basketContext } from './Basket';

const StyledBadge = styled(Badge)<BadgeProps>(({ theme }) => ({
    '& .MuiBadge-badge': {
      right: -3,
      top: 13,
      border: `2px solid ${theme.palette.background.paper}`,
      padding: '0 4px',
    },
  }));

  const BasketElement= ({count}:any) => {

    

    return (
      <IconButton aria-label="cart">        
        <StyledBadge badgeContent={count} color="secondary">
          <ShoppingCartIcon />
        </StyledBadge>
      </IconButton>
    );
  }

  export default BasketElement;