// src/services/authService.ts
import {firstValueFrom, from} from 'rxjs';
import { switchMap } from 'rxjs/operators';
import axios from 'axios';
import {LoginCredentials, UserData} from "../type/authTypes";

const headers = {
    'accept': 'application/json',
    'Content-Type': 'application/json',
};
export const serviceLogin = async (credentials: LoginCredentials): Promise<UserData> => {
    try {
        const response = await firstValueFrom(
            from(axios.post('login?useCookies=true', credentials, { headers }))
                .pipe(switchMap(() => from(axios.get<UserData>(`manage/info`, { headers }))))
        );

        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(error.response?.data as string || 'Login failed');
        } else {
            throw new Error('An unknown error occurred');
        }
    }
};

export const serviceReLogin = async (): Promise<UserData> => {
    try {
        const response = await axios.get<UserData>(`manage/info`, { headers })

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
