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
  
const requestOptions = {
  ...options,
  headers: {    
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'      
  },
  body: !!data ? JSON.stringify(data) : undefined
};

const resp = await fetch(`${path}`, requestOptions).then((responce) => handlResponce(responce));

return resp;
};


export const apiClientNoJwt = async ({path, method, data}: apiClientNoJwtProps) => {

const requestOptions : RequestInit = {   
  method: method,
  headers: {          
      'Content-Type': 'application/json'      
  },
  body: !!data ? JSON.stringify(data) : undefined,
  //credentials: 'include'
};

const resp = await fetch(`${path}`, requestOptions).then((responce) => handlResponce(responce));

return resp;
};

export const apiClientFormUrlEncoded = async ({path, method, data}: apiClientNoJwtProps) => {

  const requestOptions : RequestInit = {   
    method: method,
    headers: {          
        'Content-Type': 'application/x-www-form-urlencoded'      
    },
    body: !!data ? JSON.stringify(data) : undefined
  };
  
  const resp = await fetch(`${path}`, requestOptions).then((responce) => handlResponce(responce));
  
  return resp;
  };

  interface apiClientProps {
    path:string,
    options: RequestInit,
    data?: any    
  }

  interface apiClientNoJwtProps {
    path:string,
    method:string,
    data?: any    
  }