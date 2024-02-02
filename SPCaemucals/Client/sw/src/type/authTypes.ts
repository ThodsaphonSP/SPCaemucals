// src/types/authTypes.ts
import {User} from "./User";

export interface LoginCredentials {
    emailOrPhone: string,
    password: string,
    rememberMe: boolean
}

export interface UserData {
    id: string;
    username: string;
    // Add other user fields as necessary
}

export interface AuthState {
    user: User | null;
    status: 'idle' | 'loading' | 'succeeded' | 'failed';
    error: string | null;
}
