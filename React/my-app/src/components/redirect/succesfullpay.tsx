import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

const SuccusfullPay : React.FC = () => {
    const navigate = useNavigate();
    
    
    return (
        <div>
            <div>PayMent Seccusfull!!</div>
            <Button onClick={()=> navigate('/')}>Home</Button>
        </div>
    );
};

export default SuccusfullPay;