import { makeAutoObservable} from "mobx";
import * as authApi from "../api/moduls/authApi"
import { User, UserManager, WebStorageStateStore } from "oidc-client";
import autConfig from "../interfaces/config"

class AuthStore{
    user: User | null = null;
    userManager: UserManager | null = null;

    constructor(){
        makeAutoObservable(this);

        this.userManager = new UserManager({
            ...autConfig,
            userStore: new WebStorageStateStore({ store: window.localStorage}),
        });

        this.userManager.events.addUserLoaded((user) => {
            this.setUser(user);
        });

        this.userManager.events.addUserUnloaded(() => {
            this.setUser(null);
        })

        this.getUser();
    }

    setUser(user: User | null){
        this.user = user;
    }

    setUserManager(userManager: UserManager | null){
        this.userManager = userManager;
    }

    async getUser(){
        try{
        const responce = await this.userManager!.getUser();
        this.setUser(responce);
        console.log("User ----->>>", this.user)
        }
        catch (error){
            console.log(error);
        }
    }
    
    login() {
        this.userManager?.signinRedirect();     
    }

    logOut() {
        this.userManager?.signoutRedirect();
    }

    async complitLogin(){
        try{
        const responce = await this.userManager!.signinRedirectCallback(); 
        this.setUser(responce);
        } 
        catch(error){
            console.log(error);
        }
    }

    async complitLogout(){
        try{
        await this.userManager?.signoutRedirectCallback();
        this.setUser(null);
        } 
        catch(error){
            console.log(error);
        }
    }
}

export default new AuthStore();