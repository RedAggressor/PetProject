import AuthStore from "../Authentification/authStore";

const baseUrl = "http://localhost:5000/api/v1/CatalogBff/"


const handlResponce = async (response: Response) => {
  if(!response.ok){
    const message = await response.json()   
    throw Error(message.error || 'Request error')
  }

  return response.json();
}

export const apiClient = async ({path,options, data}: apiClientProps) => {

  const token = AuthStore.user?.access_token;
  //if(!token) {
   // throw new Error("No access token availeble");
  //}

const requestOptions = {
  ...options,
  headers: {
      ...options.headers,
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json' 
  },
  body: !!data ? JSON.stringify(data) : undefined,
};

return await fetch(`${path}`, requestOptions).then((responce) => handlResponce(responce))
  };
  

  interface apiClientProps {
    path:string,
    options: RequestInit,
    data?: any
  }