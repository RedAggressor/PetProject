const config = {
    authority: "http://www.fruitshop.com:5002",    
    client_id: "react_spa",
    redirect_uri: "http://www.fruitshop.com:3000/callback",
    response_type: "code",
    scope: "openid profile react mvc catalog.catalogbff catalog.catalogitem",
    allowed_cors_origins: [
      "http://www.fruitshop.com:3000",
      "https://www.liqpay.ua"
    ],
    post_logout_redirect_uri: "http://www.fruitshop.com:3000/"    
  };
  
  export default config;