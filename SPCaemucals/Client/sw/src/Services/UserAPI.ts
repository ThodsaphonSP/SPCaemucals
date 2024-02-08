
import {UserListResponse} from "../type/User";
import {api} from "./RoleAPI";

export async function GetUserAPI(page: number, rowsPerPage: number, phoneOrMail: string) {


    const response = await api
        .get<UserListResponse>(`/account/users?pagNumber=${page}&pageSize=${rowsPerPage}&phoneOrMail=${phoneOrMail}`, {
            params: {
                page: page,
                pageSize: rowsPerPage
            }
        });
    return response;
}