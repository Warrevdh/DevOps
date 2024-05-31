package com.example.oogartsapp.data.database.bio

import androidx.room.Database
import androidx.room.RoomDatabase

/**
 * Room Database class representing the BioDatabase.
 */
@Database(entities = [DbBio::class], version = 1)
abstract class BioDatabase : RoomDatabase() {
    /**
     * Abstract method to retrieve the DAO (Data Access Object) associated with the bio table.
     * @return Instance of [BioDao] to interact with the "bio" table.
     */
    abstract fun bioDao(): BioDao
}
