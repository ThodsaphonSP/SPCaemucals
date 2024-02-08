import axios from "axios";
import {Role} from "../type/Role";

// Create an axios instance
export const api = axios.create({
    baseURL: process.env.REACT_APP_API_BASE_URL,
});

export async function GetRoleAPI(name: string) {
    const response = await api
        .get<Role[]>(`api/role?name=${name}`);
    return response;
}