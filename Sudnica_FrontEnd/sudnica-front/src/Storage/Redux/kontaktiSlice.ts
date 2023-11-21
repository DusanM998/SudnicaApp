import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    kontakti: []
};

export const kontaktiSlice = createSlice({
    name: "Kontakti",
    initialState: initialState,
    reducers: {
        setKontakti: (state, action) => {
            state.kontakti = action.payload;
        },
    },
});

export const { setKontakti } = kontaktiSlice.actions;
export const kontaktiReducer = kontaktiSlice.reducer;