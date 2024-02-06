import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import {authReducer} from "../features/auth/authSlice";
import {navReducer} from "../features/Nav/NavSlice"

export const store = configureStore({
  reducer: {
    auth: authReducer,
    nav:navReducer
  },
});


export const selectUser = (state: RootState) => state.auth.user;
export const selectNavTitle = (state:RootState)=> state.nav.title;

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
