import {createSlice, PayloadAction} from "@reduxjs/toolkit";


export interface NavState {
    title: string;

}

const initialState: NavState = {
    title:""
};

// The function below is called a thunk and allows us to perform async logic. It
// can be dispatched like a regular action: `dispatch(incrementAsync(10))`. This
// will call the thunk with the `dispatch` function as the first argument. Async
// code can then be executed and other actions can be dispatched. Thunks are
// typically used to make async requests.


export const NavSlice = createSlice({
    name: 'nav',
    initialState,
    // The `reducers` field lets us define reducers and generate associated actions
    reducers: {
        setTitle: (state, action: PayloadAction<string>) => {
            state.title = action.payload;
        }
    },
});

export const { setTitle } = NavSlice.actions;



// We can also write thunks by hand, which may contain both sync and async logic.
// Here's an example of conditionally dispatching actions based on current state.

export const navReducer = NavSlice.reducer;