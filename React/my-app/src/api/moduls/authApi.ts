import apiClient from "../client";

export const login = ({ email, password }:{ email:string, password:string }) => apiClient({
    path: 'https://reqres.in/api/login',
    method: 'POST',
    data: {email, password}
})

export const registration = ({email, password} : {email:string, password:string}) => apiClient({
    path: `https://reqres.in/api/register`,
    method: 'POST',
    data: {email, password}
})

export const loginIdentityGet = async () => await apiClient({
    path: "http://www.alevelwebsite.com:5002/Account/Login",
    method: "GET"
})