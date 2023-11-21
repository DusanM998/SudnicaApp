import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    tipPostupka: []
};

export const tipPostupkaSlice = createSlice({
    name: "TipPostupka",
    initialState: initialState,
    reducers: {
        setTipPostupka: (state, action) => {
            state.tipPostupka = action.payload;
        },
    },
});

export const { setTipPostupka } = tipPostupkaSlice.actions;
export const tipoviPostupakaReducer = tipPostupkaSlice.reducer;