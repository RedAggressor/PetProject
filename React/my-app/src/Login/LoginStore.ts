import { makeAutoObservable } from "mobx";
import AuthStore from "../Authentification/AuthStore";
import { FC } from "react";

class LoginStore {

    private authStore: AuthStore;

    email = '';
    password = '';
    error = '';

    constructor(authStore: AuthStore) {
        this.authStore = authStore;
        makeAutoObservable(this);
    }

    changeEmail(email: string) {
        this.email = email;
        if (!!this.error) {
            this.error = '';
        }
    }

    changePassword(password: string) {
        this.password = password;
        if (!!this.error) {
            this.error = '';
        }
    }

    async login() {
        try {            
           await this.authStore.login(this.email, this.password);
           
        }
        catch (e) {
            if (e instanceof Error) {
                this.error = e.message;                
            }
        }       
    };
}

export default LoginStore;