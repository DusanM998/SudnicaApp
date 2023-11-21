import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const kontaktiApi = createApi({
    reducerPath: "kontakt",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:7185/api/",
        prepareHeaders: (headers: Headers, api) => {
            const token = localStorage.getItem("token");
            token && headers.append("Authorization", "Bearer" + token);
        },
    }),
    tagTypes: ["Kontakti"],
    endpoints: (builder) => ({
        getKontakti: builder.query({
            query: () => ({
                url:"kontakt"
            }),
            providesTags: ["Kontakti"]
        }),
        getKontaktById: builder.query({
            query: (id) => ({
                url:`kontakt/${id}`,
            }),
            providesTags: ["Kontakti"]
        }),
        createContact: builder.mutation({
            query: (data) => ({
                url: "kontakt",
                method: "POST",
                body: data,
            }),
            invalidatesTags: ["Kontakti"],
        }),
        updateContact: builder.mutation({
            query: ({ data, id }) => ({
                url: "kontakt/" + id,
                method: "PUT",
                body: data
            }),
            invalidatesTags: ["Kontakti"],
        }),
        deleteContact: builder.mutation({
            query: (id) => ({
                url: "kontakt/" + id,
                method: "DELETE",
            }),
            invalidatesTags: ["Kontakti"],
        }),
    }),
});

export const {
    useGetKontaktiQuery,
    useGetKontaktByIdQuery,
    useCreateContactMutation,
    useUpdateContactMutation,
    useDeleteContactMutation
} = kontaktiApi;

export default kontaktiApi;