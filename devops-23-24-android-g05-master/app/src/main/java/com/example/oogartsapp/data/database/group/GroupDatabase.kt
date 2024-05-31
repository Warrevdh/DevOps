package com.example.oogartsapp.data.database.group

import androidx.room.Database
import androidx.room.RoomDatabase

/**
 *  Database class for managing the [Group] entities using Room.
 */
@Database(entities = [DbGroup::class], version = 1)
abstract class GroupDatabase : RoomDatabase() {
    /**
     * Retrieves the Data Access Object (DAO) for the Group entity.
     *
     * @return The [GroupDao] interface to perform operations on the [DbGroup] entity.
     */
    abstract fun groupDao(): GroupDao
}
