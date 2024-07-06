const config = {
    authority: "http://localhost:5002",
    
    client_id: "react_spa",
    redirect_uri: "http://localhost:3000/callback",
    response_type: "code",
    scope: "openid profile react catalog.catalogbff catalog.catalogitem mvc",
    allowed_cors_origins: "localhost",
    post_logout_redirect_uri: "http://localhost:3000/signin-oidc"   
  };
  
  export default config;