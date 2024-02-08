// src/services/authService.ts
import {firstValueFrom, from, tap} from 'rxjs';
import { switchMap } from 'rxjs/operators';
import {LoginCredentials} from "../type/authTypes";
import {User} from "../type/User";
import {api} from "./RoleAPI";
import axios from "axios";
import {jwtDecode} from "jwt-decode";
import Cookies from 'js-cookie';

let headers: Record<string, string> = {
    'accept': 'application/json',
    'Content-Type': 'application/json'
};
export const serviceLogin = async (credentials: LoginCredentials): Promise<User> => {
    try {
        const response = await firstValueFrom(from(api.post('/Account/login', credentials)).pipe(
            tap((resp) => {
                // Now we get JWT token from resp.data.token
                const token = resp.data.token;
                // Attach the Bearer token in a 'Authorization' header for subsequent requests
                api.defaults.headers.common['Authorization'] = 'Bearer ' + token;
                // Assuming the refresh token is returned in the response body
                const refreshToken = resp.data.refreshToken;
                // Store refresh token securely
                window.localStorage.setItem('refreshToken', refreshToken);

                // decode the JWT token to get expiration date
                const decodedToken = jwtDecode(token);
                const expirationDate = decodedToken.exp;
                // convert JWT exp time (seconds since the Epoch) to Date object
                const expiryDate = new Date(0);

                if (expirationDate){
                    expiryDate.setUTCSeconds(expirationDate);
                }


                // calculate time till token expiration in seconds
                const now = new Date();
                const timeTillExpiry = (expiryDate.getTime() - now.getTime()) / 1000; // in seconds

                // set the token in the cookie to expire at the same time as the JWT token
                Cookies.set('token', token, { expires: timeTillExpiry });
            }),
            switchMap(() => from(api.get<User>('/Account/info')))
        ));
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            const message = `Error ${error.response?.data} at ${error.config?.url}. Message: ${error.response?.data.title as string || 'Login failed'}`;
            throw new Error(message);
        } else {
            throw new Error('An unknown error occurred');
        }
    }
};

export const serviceReLogin = async (): Promise<User> => {
    try {
        const token = Cookies.get('token');

        if (!token) {
            throw new Error('No token available');
        }


        api.defaults.headers.common['Authorization'] = 'Bearer ' + token;


        const userInfo = await api.get<User>('/Account/info');

        return userInfo.data;

    } catch (err) {
        if (axios.isAxiosError(err)) {
            console.error(err.response?.data as string || 'Login failed');

            throw new Error(err.response?.data as string || 'Login failed');
        } else {
            throw new Error('An unknown error occurred');
        }
    }
};

export const serviceLogout = async (): Promise<void> => {
    try {
        // Call the logout API
        await axios.post('/Account/logout');
    } catch (error) {
        if (axios.isAxiosError(error)) {
            const message = `Error ${error.response?.status} at ${error.config?.url}. Message: ${error.response?.data.title as string || 'Logout failed'}`;
            throw new Error(message);
        } else {
            throw new Error('An unknown error occurred');
        }
    } finally {
        // Clear headers and localStorage regardless of API success/failure
        delete headers['Authorization'];
        window.localStorage.removeItem('refreshToken');
    }
};
