import {apiClient} from "../client";

const bassPath = "http://www.fruitshop.com:5000/api/v1/CatalogBff/"

export const getType = async () => await apiClient({
    path:`${bassPath}GetListType`,
    options: {method: "POST"}
})
    