import { FC, ReactElement } from "react";

interface LiqPayButtonProps{
  data:string;
  signature:string;
  price:number;
}

const LiqPayButton: React.FC<LiqPayButtonProps> = ({data, signature, price}) => {

    return (            
    <form method="POST" action="https://www.liqpay.ua/api/3/checkout" acceptCharset="utf-8" target="_blank" >    
      <input type="hidden" name="data" value={data} />
      <input type="hidden" name="signature" value={signature} />
      <button style={{
        border: 'none',
        display: 'inline-block',
        textAlign: 'center',
        padding: '5px 24px',
        color: '#fff',
        boxShadow: '0px 0px 4px rgba(0, 0, 0, 0.12), 0px 2px 4px rgba(0, 0, 0, 0.12)',
        fontSize: '16px',
        lineHeight: '1.75',
        fontWeight: '600',
        fontFamily: "'Open Sans', sans-serif",
        cursor: 'pointer',
        borderRadius: '8px',
        background: '#77CC5D',
        opacity: '1'
      }} onMouseOver={(e) => e.currentTarget.style.opacity = '0.5'} onMouseOut={(e) => e.currentTarget.style.opacity = '1'}>
        <img src="https://static.liqpay.ua/buttons/logo-white.svg" alt="LiqPay" style={{ verticalAlign: 'middle' }} />
        <span style={{ verticalAlign: 'middle', marginLeft: '8px', textTransform: 'uppercase' }}>Pay {price} UAH</span>
      </button>
    </form>
    
  );
}
  
  export default LiqPayButton;