import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const tipoviPostupakaApi = createApi({
    reducerPath: "tipPostupka",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:7185/api/",
        prepareHeaders: (headers: Headers, api) => {
            const token = localStorage.getItem("token");
            token && headers.append("Authorization", "Bearer" + token);
        },
    }),
    tagTypes: ["TipoviPostupaka"],
    endpoints: (builder) => ({
        getTipoviPostupaka: builder.query({
            query: () => ({
                url:"tipPostupka"
            }),
            providesTags: ["TipoviPostupaka"]
        }),
        getTipPostupkaById: builder.query({
            query: (id) => ({
                url:`tipPostupka/${id}`,
            }),
            providesTags: ["TipoviPostupaka"]
        }),
        createTipPostupka: builder.mutation({
            query: (data) => ({
                url: "tipPostupka",
                method: "POST",
                body: data,
            }),
            invalidatesTags: ["TipoviPostupaka"],
        }),
        updateTipPostupka: builder.mutation({
            query: ({ data, id }) => ({
                url: "tipPostupka/" + id,
                method: "PUT",
                body: data
            }),
            invalidatesTags: ["TipoviPostupaka"],
        }),
        deleteTipPostupka: builder.mutation({
            query: (id) => ({
                url: "tipPostupka/" + id,
                method: "DELETE",
            }),
            invalidatesTags: ["TipoviPostupaka"],
        }),
    }),
});

export const {
    useGetTipoviPostupakaQuery,
    useGetTipPostupkaByIdQuery,
    useCreateTipPostupkaMutation,
    useUpdateTipPostupkaMutation,
    useDeleteTipPostupkaMutation
} = tipoviPostupakaApi;

export default tipoviPostupakaApi;