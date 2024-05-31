package com.example.oogartsapp.network.service

import com.example.oogartsapp.network.ApiEyeconditionDetail
import com.example.oogartsapp.network.items.EyeconditionItem
import kotlinx.coroutines.flow.flow
import retrofit2.http.GET
import retrofit2.http.Path

/**
 * Interface defining functions to interact with the EyeCondition API.
 */
interface EyeconditionApiService {
    /**
     * Fetches a list of eye conditions from the API.
     *
     * @return [EyeconditionItem] containing a list of eye conditions and total count.
     */
    @GET("Eyecondition")
    suspend fun getEyeconditions(): EyeconditionItem

    /**
     * Fetches details of a specific eye condition from the API.
     *
     * @param id The unique identifier of the eye condition.
     * @return [ApiEyeconditionDetail] containing details of the specified eye condition.
     */
    @GET("Eyecondition/{id}")
    suspend fun getEyecondition(@Path("id") id: Int): ApiEyeconditionDetail
}

/**
 * Extension function converting the result of [getEyeconditions] into a Flow emitting the list of eye conditions.
 *
 * @return Flow emitting the list of eyeconditions.
 */
fun EyeconditionApiService.getEyeconditionsAsFlow() = flow { emit(getEyeconditions().eyeConditions) }

/**
 * Extension function converting the result of [getEyecondition] into a Flow emitting the eye condition detail.
 *
 * @param id The unique identifier of the eye condition.
 * @return Flow emitting the details of the specified eyecondition.
 */
fun EyeconditionApiService.getEyeconditionAsFlow(id: Int) = flow { emit(getEyecondition(id)) }
