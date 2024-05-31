package com.example.oogartsapp.data

import android.util.Log
import com.example.oogartsapp.data.database.eyecondition.EyeconditionDao
import com.example.oogartsapp.data.database.eyecondition.asDbEyecondition
import com.example.oogartsapp.data.database.eyecondition.asDomainEyecondition
import com.example.oogartsapp.data.database.eyecondition.asDomainObjects
import com.example.oogartsapp.model.Eyecondition
import com.example.oogartsapp.network.asDomainObject
import com.example.oogartsapp.network.asDomainObjects
import com.example.oogartsapp.network.service.EyeconditionApiService
import com.example.oogartsapp.network.service.getEyeconditionAsFlow
import com.example.oogartsapp.network.service.getEyeconditionsAsFlow
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import java.net.SocketTimeoutException

/**
 * Interface defining operations to interact with the [Eyecondition] repository.
 */
interface EyeconditionRepository {
    /**
     * Inserts an [Eyecondition] into the repository.
     *
     * @param item The [Eyecondition] to be inserted.
     */
    suspend fun insert(item: Eyecondition)

    /**
     * Inserts an [Eyecondition] by ID into the repository.
     *
     * @param id The ID of the [Eyecondition] to be inserted.
     */
    suspend fun insertEyecondition(id: Int)

    /**
     * Retrieves a [Flow] of lists of [Eyecondition]s from the repository.
     *
     * @return A [Flow] emitting lists of [Eyecondition]s.
     */
    fun getEyeconditions(): Flow<List<Eyecondition>>

    /**
     * Retrieves a [Flow] of an [Eyecondition] by its ID from the repository.
     *
     * @param id The ID of the [Eyecondition] to be retrieved.
     * @return A [Flow] emitting the [Eyecondition].
     */
    fun getEyecondition(id: Int): Flow<Eyecondition>

    /**
     * Refreshes the repository data by fetching [Eyecondition]s from the API.
     */
    suspend fun refresh()
}

/**
 * Implementation of the [EyeconditionRepository] interface that caches [Eyecondition] data.
 *
 * @property eyeconditionDao The DAO for accessing [Eyecondition] data in the local database.
 * @property eyeconditionApiService The API service for fetching [Eyecondition] data from the remote source.
 */
class CachingEyeconditionRepository(
    private val eyeconditionDao: EyeconditionDao,
    private val eyeconditionApiService: EyeconditionApiService,
) : EyeconditionRepository {

    /**
     * Inserts an [Eyecondition] into the local database.
     *
     * @param item The [Eyecondition] to be inserted.
     */
    override suspend fun insert(item: Eyecondition) {
        eyeconditionDao.insert(item.asDbEyecondition())
    }

    /**
     * Inserts an [Eyecondition] by ID into the local database by fetching it from the API.
     * In case of a SocketTimeoutException or HttpException, a placeholder [Eyecondition] is inserted.
     *
     * @param id The ID of the [Eyecondition] to be inserted.
     */
    override suspend fun insertEyecondition(id: Int) {
        try {
            eyeconditionApiService.getEyeconditionAsFlow(id).collect {
                insert(it.asDomainObject())
            }
        } catch (e: SocketTimeoutException) {
            Log.e("EyeconditionRepository", "insertEyecondition: API call timed out")
        } catch (e: retrofit2.HttpException) {
            insert(
                Eyecondition(
                    id = id,
                    name = "Oogziekte op dit moment niet beschikbaar",
                    imageUrl = "",
                    description = "",
                    body = "",
                    brochureUrl = "",
                ),
            )
        }
    }

    /**
     * Retrieves a [Flow] of lists of [Eyecondition]s from the local database.
     *
     * @return A [Flow] emitting lists of [Eyecondition]s.
     */
    override fun getEyeconditions(): Flow<List<Eyecondition>> {
        return eyeconditionDao.getEyeconditions().map {
            it.asDomainObjects()
        }
    }

    /**
     * Retrieves a [Flow] of an [Eyecondition] by its ID from the local database.
     *
     * @param id The ID of the [Eyecondition] to be retrieved.
     * @return A [Flow] emitting the [Eyecondition].
     */
    override fun getEyecondition(id: Int): Flow<Eyecondition> {
        return eyeconditionDao.getEyecondition(id).map {
            it.asDomainEyecondition()
        }
    }

    /**
     * Refreshes the local database by fetching [Eyecondition]s from the remote API source.
     * In case of a SocketTimeoutException, logs an error message indicating API call timeout.
     */
    override suspend fun refresh() {
        try {
            eyeconditionApiService.getEyeconditionsAsFlow().collect {
                for (eyecondition in it.asDomainObjects()) {
                    insert(eyecondition)
                }
            }
        } catch (e: SocketTimeoutException) {
            Log.e("EyeconditionRepository", "refresh: API call timed out")
        }
    }
}
