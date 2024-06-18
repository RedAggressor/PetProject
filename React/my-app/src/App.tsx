import React, { useContext } from 'react';
import logo from './logo.svg';
import Home from './Home/Home';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout/Layout';

function App() {
  //let navigate = useNavigate();
  
  //function HandRedirect(){
     // navigate('http://www.alevelwebsite.com:5002/Account/Login');
  //}

  return (
    <div>    
      <button >Login</button>  
      <Home/> 
      <Layout />     
    </div>
  );
}

export default App;
