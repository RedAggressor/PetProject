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

    return (
        <div>
            <div>Loading...</div>
        </div>
    );
};

export default Callback;