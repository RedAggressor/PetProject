const config = {
    authority: "http://www.alevelwebsite.com:5002",    
    client_id: "react_spa",
    redirect_uri: "http://www.alevelwebsite.com:3000/callback",
    response_type: "code",
    scope: "openid profile react mvc catalog.catalogbff catalog.catalogitem",
    allowed_cors_origins: "http://www.alevelwebsite.com:3000",
    post_logout_redirect_uri: "http://www.alevelwebsite.com:3000/signin-oidc"
  };
  
  export default config;