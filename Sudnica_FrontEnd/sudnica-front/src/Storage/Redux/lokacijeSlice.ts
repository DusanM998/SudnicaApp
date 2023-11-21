import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    lokacije: []
};

export const lokacijeSlice = createSlice({
    name: "Lokacije",
    initialState: initialState,
    reducers: {
        setLokacije: (state, action) => {
            state.lokacije = action.payload;
        },
    },
});

export const { setLokacije } = lokacijeSlice.actions;
export const lokacijeReducer = lokacijeSlice.reducer;