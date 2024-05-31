package com.example.oogartsapp.data

import android.util.Log
import com.example.oogartsapp.data.database.group.GroupDao
import com.example.oogartsapp.data.database.group.asDbGroup
import com.example.oogartsapp.data.database.group.asDomainObjects
import com.example.oogartsapp.model.Group
import com.example.oogartsapp.network.asDomainObjects
import com.example.oogartsapp.network.service.GroupApiService
import com.example.oogartsapp.network.service.getGroupsAsFlow
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import java.net.SocketTimeoutException

/**
 * Interface defining operations to interact with the Group repository.
 */
interface GroupRepository {
    /**
     * Inserts a [Group] into the repository.
     *
     * @param item The [Group] to be inserted.
     */
    suspend fun insert(item: Group)

    /**
     * Retrieves a [Flow] of lists of [Group]s from the repository.
     *
     * @return A [Flow] emitting lists of [Group]s.
     */
    fun getGroups(): Flow<List<Group>>

    /**
     * Refreshes the repository data by fetching [Group]s from the API.
     */
    suspend fun refresh()
}

/**
 * Implementation of the [GroupRepository] interface that caches [Group] data.
 *
 * @property groupDao The DAO for accessing [Group] data in the local database.
 * @property groupApiService The API service for fetching [Group] data from the remote source.
 */
class CachingGroupRepository(
    private val groupDao: GroupDao,
    private val groupApiService: GroupApiService,
) : GroupRepository {

    /**
     * Inserts a [Group] into the local database.
     *
     * @param item The [Group] to be inserted.
     */
    override suspend fun insert(item: Group) {
        groupDao.insert(item.asDbGroup())
    }

    /**
     * Retrieves a [Flow] of lists of [Group]s from the local database.
     *
     * @return A [Flow] emitting lists of [Group]s.
     */
    override fun getGroups(): Flow<List<Group>> {
        return groupDao.getGroups().map {
            it.asDomainObjects()
        }
    }

    /**
     * Refreshes the local database by fetching [Group]s from the remote API source.
     * In case of a SocketTimeoutException, logs an error message indicating API call timeout.
     */
    override suspend fun refresh() {
        try {
            groupApiService.getGroupsAsFlow().collect {
                for (group in it.asDomainObjects()) {
                    Log.i("GroupRepository", "refresh: $group")
                    insert(group)
                }
            }
        } catch (e: SocketTimeoutException) {
            Log.e("GroupRepository", "refresh: API call timed out")
        }
    }
}
