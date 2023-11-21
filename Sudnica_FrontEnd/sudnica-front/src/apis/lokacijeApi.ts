import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const lokacijeApi = createApi({
    reducerPath: "lokacija",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:7185/api/",
        prepareHeaders: (headers: Headers, api) => {
            const token = localStorage.getItem("token");
            token && headers.append("Authorization", "Bearer" + token);
        },
    }),
    tagTypes: ["Lokacije"],
    endpoints: (builder) => ({
        getLokacije: builder.query({
            query: () => ({
                url:"lokacija"
            }),
            providesTags: ["Lokacije"]
        }),
        getLokacijaById: builder.query({
            query: (id) => ({
                url:`lokacija/${id}`,
            }),
            providesTags: ["Lokacije"]
        }),
        createLokacija: builder.mutation({
            query: (data) => ({
                url: "lokacija",
                method: "POST",
                body: data,
            }),
            invalidatesTags: ["Lokacije"],
        }),
        updateLokacija: builder.mutation({
            query: ({ data, id }) => ({
                url: "lokacija/" + id,
                method: "PUT",
                body: data
            }),
            invalidatesTags: ["Lokacije"],
        }),
        deleteLokacija: builder.mutation({
            query: (id) => ({
                url: "lokacija/" + id,
                method: "DELETE",
            }),
            invalidatesTags: ["Lokacije"],
        }),
    }),
});

export const {
    useGetLokacijeQuery,
    useGetLokacijaByIdQuery,
    useCreateLokacijaMutation,
    useUpdateLokacijaMutation,
    useDeleteLokacijaMutation
} = lokacijeApi;

export default lokacijeApi;