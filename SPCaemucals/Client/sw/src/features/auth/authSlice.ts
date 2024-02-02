// src/features/auth/authSlice.ts
import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import {User} from "../../type/User";
import {AuthState, LoginCredentials,} from "../../type/authTypes";
import {serviceLogin, serviceLogout, serviceReLogin} from "../../Services/authService";


export const login = createAsyncThunk(
    'auth/login',
    async (credentials: LoginCredentials, { rejectWithValue }) => {
        try {
            const user = await serviceLogin(credentials);
            return user;
        } catch (error) {
            if (error instanceof Error) {
                return rejectWithValue(error.message);
            }
            return rejectWithValue('Login failed');
        }
    }
);





export const reLogin = createAsyncThunk(
    'auth/reLogin',
    async (_, { rejectWithValue }) => {
        try {
            const user = await serviceReLogin();
            return user;
        } catch (error) {
            if (error instanceof Error) {
                return rejectWithValue(error.message);
            }
            return rejectWithValue('Login failed');
        }
    }
);

export const logout = createAsyncThunk('auth/logout', async () => {
    await serviceLogout();
});

const initialState: AuthState = {
    user: null,
    status: 'idle',
    error: null,
};

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(login.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(login.fulfilled, (state, action: PayloadAction<User>) => {
                state.status = 'succeeded';
                state.user = action.payload;
            })
            .addCase(login.rejected, (state, action) => {
                state.status = 'failed';
                state.error = action.payload as string;
            })
            .addCase(logout.fulfilled, (state) => {
                state.user = null;
            })
            // Addition of relogin action
            .addCase(reLogin.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(reLogin.fulfilled, (state, action: PayloadAction<User>) => {
                state.status = 'succeeded';
                state.user = action.payload;
            })
            .addCase(reLogin.rejected, (state, action) => {
                state.status = 'failed';
                state.error = action.payload as string;
            });
    },
});

export const authReducer = authSlice.reducer;
