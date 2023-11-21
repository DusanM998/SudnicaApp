import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const kompanijeApi = createApi({
    reducerPath: "kompanija",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:7185/api/",
        prepareHeaders: (headers: Headers, api) => {
            const token = localStorage.getItem("token");
            token && headers.append("Authorization", "Bearer" + token);
        },
    }),
    tagTypes: ["Kompanije"],
    endpoints: (builder) => ({
        getKompanije: builder.query({
            query: () => ({
                url:"kompanija"
            }),
            providesTags: ["Kompanije"]
        }),
        getKompanijaById: builder.query({
            query: (id) => ({
                url:`kompanija/${id}`,
            }),
            providesTags: ["Kompanije"]
        }),
        createKompanija: builder.mutation({
            query: (data) => ({
                url: "kompanija",
                method: "POST",
                body: data,
            }),
            invalidatesTags: ["Kompanije"],
        }),
        updateKompanija: builder.mutation({
            query: ({ data, id }) => ({
                url: "kompanija/" + id,
                method: "PUT",
                body: data
            }),
            invalidatesTags: ["Kompanije"],
        }),
        deleteKompanija: builder.mutation({
            query: (id) => ({
                url: "kompanija/" + id,
                method: "DELETE",
            }),
            invalidatesTags: ["Kompanije"],
        }),
    }),
});

export const {
    useGetKompanijeQuery,
    useGetKompanijaByIdQuery,
    useCreateKompanijaMutation,
    useUpdateKompanijaMutation,
    useDeleteKompanijaMutation
} = kompanijeApi;

export default kompanijeApi;