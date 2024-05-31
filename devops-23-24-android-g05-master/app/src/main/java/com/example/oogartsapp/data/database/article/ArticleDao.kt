package com.example.oogartsapp.data.database.article

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

/**
 * Data Access Object (DAO) interface responsible for defining operations
 * to access and manipulate articles in a local database using Room.
 */
@Dao
interface ArticleDao {
    /**
     * Inserts an article into the database. If there is a conflict,
     * it replaces the existing article with the new one.
     *
     * @param item The article to be inserted or replaced in the database.
     */
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(item: DbArticle)

    /**
     * Retrieves all articles from the database sorted by title in ascending order.
     *
     * @return A [Flow] emitting a list of [DbArticle] stored in the database.
     */
    @Query("SELECT * FROM article ORDER BY title ASC")
    fun getArticles(): Flow<List<DbArticle>>

    /**
     * Retrieves an article from the database based on its ID.
     *
     * @param id The ID of the article to retrieve.
     * @return A [Flow] emitting the [DbArticle] associated with the provided ID.
     */
    @Query("SELECT * FROM article WHERE id = :id")
    fun getArticle(id: Int): Flow<DbArticle>
}
