import { makeAutoObservable } from "mobx";

class AuthStore{
    token ='';
    name='';
    constructor(){
        makeAutoObservable(this);
    }

    async login(name:string, password:string){
        //const responce = await // конект
        //this.token = responce.token;
        //this.name = responce.normalize;
    }
}