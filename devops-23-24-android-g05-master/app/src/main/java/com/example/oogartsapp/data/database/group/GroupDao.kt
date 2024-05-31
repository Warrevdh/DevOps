package com.example.oogartsapp.data.database.group

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

/**
 * Data Access Object (DAO) interface for managing group-related database operations.
 */
@Dao
interface GroupDao {
    /**
     * Inserts a [DbGroup] entity into the database.
     *
     * @param item The [DbGroup] entity to be inserted.
     */
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(item: DbGroup)

    /**
     * Retrieves all groups from the database sorted by their sequence.
     *
     * @return A [Flow] emitting a list of [DbGroup] entities in ascending sequence order.
     */
    @Query("SELECT * FROM `group` ORDER BY sequence ASC")
    fun getGroups(): Flow<List<DbGroup>>
}
