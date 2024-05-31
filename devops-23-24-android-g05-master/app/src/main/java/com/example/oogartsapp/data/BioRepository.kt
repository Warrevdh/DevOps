package com.example.oogartsapp.data

import android.util.Log
import com.example.oogartsapp.data.database.bio.BioDao
import com.example.oogartsapp.data.database.bio.asDbBio
import com.example.oogartsapp.data.database.bio.asDomainBio
import com.example.oogartsapp.model.Bio
import com.example.oogartsapp.network.asDomainObject
import com.example.oogartsapp.network.service.BioApiService
import com.example.oogartsapp.network.service.getBioAsFlow
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.flow.map
import java.net.SocketTimeoutException

/**
 * Interface defining operations to interact with the bio repository.
 */
interface BioRepository {
    /**
     * Inserts a [Bio] into the repository.
     *
     * @param item The [Bio] to be inserted.
     */
    suspend fun insert(item: Bio)

    /**
     * Retrieves a [Flow] of [Bio] based on the given ID from the repository.
     *
     * @param id The ID of the [Bio] to retrieve.
     * @return A [Flow] emitting the requested [Bio].
     */
    fun getBio(id: Int): Flow<Bio>

    /**
     * Refreshes the repository data for the specified [Bio] ID.
     *
     * @param id The ID of the [Bio] to refresh.
     */
    suspend fun refresh(id: Int)
}

/**
 * Implementation of the [BioRepository] interface that caches bio data.
 *
 * @property bioDao The DAO for accessing [Bio] data in the local database.
 * @property bioApiService The API service for fetching [Bio] data from the remote source.
 */
class CachingBioRepository(
    private val bioDao: BioDao,
    private val bioApiService: BioApiService,
) : BioRepository {

    /**
     * Inserts a [Bio] into the local database.
     *
     * @param item The [Bio] to be inserted.
     */
    override suspend fun insert(item: Bio) {
        bioDao.insert(item.asDbBio())
    }

    /**
     * Retrieves a [Flow] of [Bio] based on the given ID from the local database.
     *
     * @param id The ID of the [Bio] to retrieve.
     * @return A [Flow] emitting the requested [Bio].
     */
    override fun getBio(id: Int): Flow<Bio> {
        return bioDao.getBio(id).map {
            it.asDomainBio()
        }
    }

    /**
     * Refreshes the local database by fetching bio data for the specified [id] from the remote API source.
     * In case of a SocketTimeoutException, logs an error message indicating API call timeout.
     * In case of an HTTP exception, inserts an empty [Bio] with a default message.
     *
     * @param id The ID of the [Bio] to refresh.
     */
    override suspend fun refresh(id: Int) {
        try {
            bioApiService.getBioAsFlow(id).collect {
                Log.i("BioRepository", "refresh: $id")
                insert(it.asDomainObject())
            }
        } catch (e: SocketTimeoutException) {
            Log.e("BioRepository", "refresh: API call timed out")
        } catch (e: retrofit2.HttpException) {
            Log.e("BioRepository", "refresh: API call failed")
            insert(Bio(id = id, info = "Bio op dit moment niet beschikbaar"))
            Log.i("BioRepository", "inserted empty Bio")
        }
    }
}
