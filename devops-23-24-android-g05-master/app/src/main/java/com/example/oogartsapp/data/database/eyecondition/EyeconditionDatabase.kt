package com.example.oogartsapp.data.database.eyecondition

import androidx.room.Database
import androidx.room.RoomDatabase

/**
 * Database class for managing the [Eyecondition] entities using Room.
 */
@Database(entities = [DbEyecondition::class], version = 1)
abstract class EyeconditionDatabase : RoomDatabase() {
    /**
     * Retrieves the Data Access Object (DAO) for the [Eyecondition] entity.
     *
     * @return The [EyeconditionDao] interface to perform operations on the [DbEyecondition] entity.
     */
    abstract fun eyeconditionDao(): EyeconditionDao
}
