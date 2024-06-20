const baseUrl = "http://localhost:5000/api/v1/CatalogBff/"

const handleResponse = async (response: Response) => {
  if (!response.ok) {
    const message = await response.json()   
    throw Error(message.error || 'Request error')
  }
  return response.json()
}

const apiClient = async ({ path, method, data }: apiClientProps) => {
  const requestOptions = {
    method,
    headers: { 'Content-Type': 'application/json' },
    body: !!data ? JSON.stringify(data) : undefined
  }
  return await fetch(`${path}`, requestOptions).then((responce) => handleResponse(responce))
}

interface apiClientProps {
  path: string
  method: string
  data?: any
}

export default apiClient