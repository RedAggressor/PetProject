import { makeAutoObservable} from "mobx";
import * as authApi from "../api/moduls/authApi"

class AuthStore{
    token='';
    email='';
           
    constructor() {
        makeAutoObservable(this);
    }

    async login(email: string, password: string) {
        const respon = await authApi.login({ email, password });
        this.token = respon.token;
        this.email = email;       
    }

    logOut() {
        this.token = '';
        this.email='';
    }

    async registartion(email: string, password: string){
        const respon = await authApi.registration({email, password});
       this.token = respon.token;
       this.email = email;        
    }
}

export default AuthStore;