package com.example.oogartsapp.network.service

import com.example.oogartsapp.network.items.GroupItem
import kotlinx.coroutines.flow.flow
import retrofit2.http.GET

/**
 * Interface defining functions to interact with the Group API.
 */
interface GroupApiService {
    /**
     * Fetches a list of groups from the API.
     *
     * @return [GroupItem] containing a list of groups.
     */
    @GET("Group")
    suspend fun getGroups(): GroupItem
}

/**
 * Extension function converting the result of [getGroups] into a Flow emitting the list of groups.
 *
 * @return Flow emitting the list of groups.
 */
fun GroupApiService.getGroupsAsFlow() = flow { emit(getGroups().groups) }
