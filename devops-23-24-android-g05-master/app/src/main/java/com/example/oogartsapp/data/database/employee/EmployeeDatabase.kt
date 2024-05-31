package com.example.oogartsapp.data.database.employee

import androidx.room.Database
import androidx.room.RoomDatabase

/**
 * Database class for managing the employee entities using Room.
 */
@Database(entities = [DbEmployee::class], version = 1)
abstract class EmployeeDatabase : RoomDatabase() {
    /**
     * Retrieves the Data Access Object (DAO) for the employee entity.
     *
     * @return The [EmployeeDao] interface to perform operations on the [DbEmployee] entity.
     */
    abstract fun employeeDao(): EmployeeDao
}
