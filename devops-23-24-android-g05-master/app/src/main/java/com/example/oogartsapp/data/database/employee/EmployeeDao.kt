package com.example.oogartsapp.data.database.employee

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

/**
 * Data Access Object (DAO) interface for handling operations related to the "employee" table in the Room database.
 */
@Dao
interface EmployeeDao {
    /**
     * Inserts an [DbEmployee] into the "employee" table.
     * @param item The [DbEmployee] object to be inserted.
     */
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(item: DbEmployee)

    /**
     * Retrieves all employees from the "employee" table in ascending order of names.
     * @return A [Flow] emitting a list of [DbEmployee] objects as a result of the query.
     */
    @Query("SELECT * FROM employee ORDER BY lastName ASC")
    fun getTeam(): Flow<List<DbEmployee>>
}
