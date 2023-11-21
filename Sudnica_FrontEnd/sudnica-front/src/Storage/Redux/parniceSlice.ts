import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    parnice: []
};

export const parniceSlice = createSlice({
    name: "Parnice",
    initialState: initialState,
    reducers: {
        setParnica: (state, action) => {
            state.parnice = action.payload;
        },
    },
});

export const { setParnica } = parniceSlice.actions;
export const parniceReducer = parniceSlice.reducer;