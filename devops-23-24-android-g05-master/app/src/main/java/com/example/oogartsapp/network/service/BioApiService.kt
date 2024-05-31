package com.example.oogartsapp.network.service

import com.example.oogartsapp.network.ApiBio
import kotlinx.coroutines.flow.flow
import retrofit2.http.GET
import retrofit2.http.Path

/**
 * Interface defining functions to interact with the Bio API.
 */
interface BioApiService {
    /**
     * Fetches a bio from the API with corresponding ID.
     *
     * @param id The ID of the bio information to retrieve.
     * @return [ApiBio] containing the Bio information.
     */
    @GET("Bio/{id}")
    suspend fun getBio(@Path("id") id: Int): ApiBio
}

/**
 * Extension function to convert the result of [getBio] into a Flow emitting the article based on ID.
 *
 * @param id The ID of the bio to retrieve.
 * @return Flow emitting the bio information.
 */
fun BioApiService.getBioAsFlow(id: Int) = flow { emit(getBio(id)) }
