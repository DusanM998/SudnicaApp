import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const parniceApi = createApi({
    reducerPath: "parnica",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:7185/api/",
        prepareHeaders: (headers: Headers, api) => {
            const token = localStorage.getItem("token");
            token && headers.append("Authorization", "Bearer" + token);
        },
    }),
    tagTypes: ["Parnice"],
    endpoints: (builder) => ({
        getParnice: builder.query({
            query: () => ({
                url:"parnica"
            }),
            providesTags: ["Parnice"]
        }),
        getParnicaById: builder.query({
            query: (id) => ({
                url:`parnica/${id}`,
            }),
            providesTags: ["Parnice"]
        }),
        createParnica: builder.mutation({
            query: (data) => ({
                url: "parnica",
                method: "POST",
                body: data,
            }),
            invalidatesTags: ["Parnice"],
        }),
        updateParnica: builder.mutation({
            query: ({ data, id }) => ({
                url: "parnica/" + id,
                method: "PUT",
                body: data
            }),
            invalidatesTags: ["Parnice"],
        }),
        deleteParnica: builder.mutation({
            query: (id) => ({
                url: "parnica/" + id,
                method: "DELETE",
            }),
            invalidatesTags: ["Parnice"],
        }),
    }),
});

export const {
    useGetParniceQuery,
    useGetParnicaByIdQuery,
    useCreateParnicaMutation,
    useUpdateParnicaMutation,
    useDeleteParnicaMutation
} = parniceApi;

export default parniceApi;