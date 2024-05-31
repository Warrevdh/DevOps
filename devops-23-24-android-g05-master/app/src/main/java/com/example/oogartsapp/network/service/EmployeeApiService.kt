package com.example.oogartsapp.network.service

import com.example.oogartsapp.network.items.EmployeeItem
import kotlinx.coroutines.flow.flow
import retrofit2.http.GET

/**
 * Interface defining functions to interact with the Employee API.
 */
interface EmployeeApiService {
    /**
     * Fetches a list of employees from the API.
     *
     * @return [EmployeeItem] containing a list of employees and total count.
     */
    @GET("Employee")
    suspend fun getTeam(): EmployeeItem
}

/**
 * Extension function converting the result of [getTeam] into a Flow emitting the list of employees.
 *
 * @return Flow emitting the list of employees.
 */
fun EmployeeApiService.getTeamAsFlow() = flow { emit(getTeam().employees) }
