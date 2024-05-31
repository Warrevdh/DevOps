package com.example.oogartsapp.data.database.bio

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

/**
 * Data Access Object (DAO) interface responsible for bio-related database operations.
 */
@Dao
interface BioDao {
    /**
     * Inserts or replaces a [DbBio] object into the "bio" table.
     */
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(item: DbBio)

    /**
     * Retrieves a bio item from the "bio" table by its ID.
     * Emits a [Flow] of [DbBio] objects representing the result.
     */
    @Query("SELECT * FROM bio WHERE id = :id")
    fun getBio(id: Int): Flow<DbBio>
}
