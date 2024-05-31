package com.example.oogartsapp.data.database.eyecondition

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

/**
 * Data Access Object (DAO) for interacting with the "eyecondition" table in the Room database.
 */
@Dao
interface EyeconditionDao {
    /**
     * Inserts an [DbEyecondition] into the "eyecondition" table.
     * @param item The [DbEyecondition] object to be inserted.
     */
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(item: DbEyecondition)

    /**
     * Retrieves all eye conditions from the "eyecondition" table in ascending order of names.
     * @return A [Flow] emitting a list of [DbEyecondition] objects as a result of the query.
     */
    @Query("SELECT * FROM eyecondition ORDER BY name ASC")
    fun getEyeconditions(): Flow<List<DbEyecondition>>

    /**
     * Retrieves a specific eye condition based on its ID from the "eyecondition" table.
     * @param id The ID of the eye condition to retrieve.
     * @return A [Flow] emitting a [DbEyecondition] object with the specified ID as a result of the query.
     */
    @Query("SELECT * FROM eyecondition WHERE id = :id")
    fun getEyecondition(id: Int): Flow<DbEyecondition>
}
