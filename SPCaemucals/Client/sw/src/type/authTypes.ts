// src/types/authTypes.ts
export interface LoginCredentials {
    email: string;
    password: string;
    twoFactorCode:string
    twoFactorRecoveryCode:string
}

export interface UserData {
    id: string;
    username: string;
    // Add other user fields as necessary
}

export interface AuthState {
    user: UserData | null;
    status: 'idle' | 'loading' | 'succeeded' | 'failed';
    error: string | null;
}
