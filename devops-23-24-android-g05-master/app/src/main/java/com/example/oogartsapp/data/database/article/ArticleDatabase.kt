package com.example.oogartsapp.data.database.article

import androidx.room.Database
import androidx.room.RoomDatabase

/**
 * Room Database class responsible for defining the database instance and providing access to DAOs.
 */
@Database(entities = [DbArticle::class], version = 1)
abstract class ArticleDatabase : RoomDatabase() {
    /**
     * Abstract method to retrieve the [ArticleDao].
     *
     * @return An instance of the [ArticleDao] interface for performing database operations related to articles.
     */
    abstract fun articleDao(): ArticleDao
}
