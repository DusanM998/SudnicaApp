import { createSlice } from "@reduxjs/toolkit";
import { userModel } from "../../Interfaces";

export const emptyUserState : userModel = {
    punoIme: "",
    id: "",
    email: "",
    role: "",
    godine: 0,
};

export const userAuthSlice = createSlice({
    name: "userAuth",
    initialState: emptyUserState,
    reducers: {
        setLoggedInUser: (state, action) => {
            state.punoIme = action.payload.punoIme;
            state.id = action.payload.id;
            state.email = action.payload.email;
            state.role = action.payload.role;
            state.godine = action.payload.godine;
        },
    },
});

export const { setLoggedInUser } = userAuthSlice.actions;
export const userAuthReducer = userAuthSlice.reducer;