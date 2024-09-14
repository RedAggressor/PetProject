import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import authStor from "../../Authentification/authStore"

const Callback : React.FC = () => {

    const navigate = useNavigate();

    useEffect(() => {
        authStor.complitLogin().then(()=>{
           navigate('/');
        });
    }, [navigate])  
    
    
    //useEffect(() => {
        //const completeLogin = async () => {
            //try {
              //  await authStor.complitLogin();
                //navigate('/');
            //} catch (error) {
               // console.error('Login error:', error);                
            //}
        //};

       // completeLogin();
   // }, [navigate]);

    return (
        <div>
            <div>Loading...</div>
        </div>
    );
};

export default Callback;