import { configureStore } from "@reduxjs/toolkit";
import { authApi, kompanijeApi, kontaktiApi, lokacijeApi, parniceApi, tipoviPostupakaApi } from "../../apis";
import { userAuthReducer, userAuthSlice } from "./userAuthSlice";
import { kontaktiReducer } from "./kontaktiSlice";
import { lokacijeReducer } from "./lokacijeSlice";
import { tipoviPostupakaReducer } from "./tipPostupkaSlice";
import { parniceReducer } from "./parniceSlice";

const store = configureStore({
    reducer: {
        userAuthStore: userAuthReducer,
        kontaktiStore: kontaktiReducer,
        lokacijeStore: lokacijeReducer,
        tipoviPostupakaStore: tipoviPostupakaReducer,
        parniceStore: parniceReducer,
        [authApi.reducerPath]: authApi.reducer,
        [kontaktiApi.reducerPath]: kontaktiApi.reducer,
        [lokacijeApi.reducerPath]: lokacijeApi.reducer,
        [tipoviPostupakaApi.reducerPath]: tipoviPostupakaApi.reducer,
        [kompanijeApi.reducerPath]: kompanijeApi.reducer,
        [parniceApi.reducerPath]: parniceApi.reducer,
    },
    middleware: (getDefaultMiddleware) => 
        getDefaultMiddleware()
            .concat(authApi.middleware)
            .concat(kontaktiApi.middleware)
            .concat(lokacijeApi.middleware)
            .concat(tipoviPostupakaApi.middleware)
            .concat(kompanijeApi.middleware)
            .concat(parniceApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;

export default store;