import AuthStore from "../Authentification/authStore";

const handlResponce = async (response: Response) => {
  if(!response.ok){
    const message = await response.json()   
    throw Error(message.error || 'Request error')
  }

  return response.json();
}

export const apiClient = async ({path, options, data}: apiClientProps) => {

  const token = AuthStore.getToken();
  //if(!token) {
   //throw new Error("No access token availeble");
  //}

const requestOptions = {
  ...options,
  headers: {    
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'      
  },
  body: !!data ? JSON.stringify(data) : undefined,
};

const resp = await fetch(`${path}`, requestOptions).then((responce) => handlResponce(responce));

return resp;
}; 

  interface apiClientProps {
    path:string,
    options: RequestInit,
    data?: any
  }