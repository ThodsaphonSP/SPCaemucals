// src/services/authService.ts
import {firstValueFrom, from} from 'rxjs';
import { switchMap } from 'rxjs/operators';
import axios from 'axios';
import {LoginCredentials} from "../type/authTypes";
import {User} from "../type/User";

const headers = {
    'accept': 'application/json',
    'Content-Type': 'application/json',
};
export const serviceLogin = async (credentials: LoginCredentials): Promise<User> => {
    try {
        const response = await firstValueFrom(
            from(axios.post('api/Account/login', credentials, { headers }))
                .pipe(switchMap(() => from(axios.get<User>(`api/Account/info`, { headers }))))
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            const message = `Error ${error.response?.status} at ${error.config?.url}. Message: ${error.response?.data.title as string || 'Login failed'}`;
            throw new Error(message);
        } else {
            throw new Error('An unknown error occurred');
        }
    }
};

export const serviceReLogin = async (): Promise<User> => {
    try {
        const response = await axios.get<User>(`api/Account/info`, { headers })
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(error.response?.data as string || 'Login failed');
        } else {
            throw new Error('An unknown error occurred');
        }
    }
};

export const serviceLogout = async (): Promise<void> => {
    // Implement logout logic
    // Example: await axios.post('/api/logout');
};
