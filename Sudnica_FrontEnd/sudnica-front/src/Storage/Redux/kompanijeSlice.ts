import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    kompanije: []
};

export const kompanijeSlice = createSlice({
    name: "Kontakti",
    initialState: initialState,
    reducers: {
        setKompanije: (state, action) => {
            state.kompanije = action.payload;
        },
    },
});

export const { setKompanije } = kompanijeSlice.actions;
export const kontaktiReducer = kompanijeSlice.reducer;