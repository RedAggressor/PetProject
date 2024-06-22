import apiClient from "../client";

export const getType = async () => await apiClient({
    path: "http://localhost:5000/api/v1/CatalogBff/GetListType",
    method: "POST"
})