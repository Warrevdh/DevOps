package com.example.oogartsapp.data

import android.util.Log
import com.example.oogartsapp.data.database.article.ArticleDao
import com.example.oogartsapp.data.database.article.asDbArticle
import com.example.oogartsapp.data.database.article.asDomainArticle
import com.example.oogartsapp.data.database.article.asDomainObjects
import com.example.oogartsapp.model.Article
import com.example.oogartsapp.network.asDomainObject
import com.example.oogartsapp.network.asDomainObjects
import com.example.oogartsapp.network.service.ArticleApiService
import com.example.oogartsapp.network.service.getArticleAsFlow
import com.example.oogartsapp.network.service.getArticlesAsFlow
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import java.net.SocketTimeoutException

/**
 * Interface defining operations to interact with the article repository.
 */
interface ArticleRepository {
    /**
     * Inserts an [Article] into the repository.
     *
     * @param item The [Article] to be inserted.
     */
    suspend fun insert(item: Article)

    /**
     * Inserts an article with the specified [id] into the repository.
     *
     * @param id The ID of the article to insert.
     */
    suspend fun insertArticle(id: Int)

    /**
     * Retrieves a [Flow] of lists of [Article] from the repository.
     *
     * @return A [Flow] emitting lists of [Article].
     */
    fun getArticles(): Flow<List<Article>>

    /**
     * Retrieves a [Flow] of [Article] based on the given ID from the repository.
     *
     * @param id The ID of the article to retrieve.
     * @return A [Flow] emitting the requested [Article].
     */
    fun getArticle(id: Int): Flow<Article>

    /**
     * Refreshes the repository data by fetching articles from the remote source.
     */

    suspend fun refresh()
}

/**
 * Implementation of the [ArticleRepository] interface that caches article data.
 *
 * @property articleDao The DAO for accessing [Article] data in the local database.
 * @property articleApiService The API service for fetching [Article] data from the remote source.
 */
class CachingArticleRepository(
    private val articleDao: ArticleDao,
    private val articleApiService: ArticleApiService,
) : ArticleRepository {

    /**
     * Inserts an [Article] into the local database.
     *
     * @param item The [Article] to be inserted.
     */
    override suspend fun insert(item: Article) {
        articleDao.insert(item.asDbArticle())
    }

    /**
     * Inserts an article with the specified [id] by fetching it from the remote API source.
     * In case of a SocketTimeoutException, logs an error message indicating API call timeout.
     * In case of an HTTP exception, inserts an empty [Article] with default values.
     *
     * @param id The ID of the article to insert.
     */
    override suspend fun insertArticle(id: Int) {
        try {
            articleApiService.getArticleAsFlow(id).collect {
                Log.i("ArticleRepository", "insertArticle: $id")
                insert(it.asDomainObject())
            }
        } catch (e: SocketTimeoutException) {
            Log.e("ArticleRepository", "insertArticle: API call timed out")
        } catch (e: retrofit2.HttpException) {
            Log.e("ArticleRepository", "insertArticle: API call failed")
            insert(
                Article(
                    id = id,
                    title = "Artikel op dit moment niet beschikbaar",
                    imageUrl = "",
                    description = "",
                    content = "",
                    author = "",
                    created = "",
                ),
            )
            Log.i("ArticleRepository", "inserted empty Article")
        }
    }

    /**
     * Retrieves a [Flow] of lists of [Article] from the local database.
     *
     * @return A [Flow] emitting lists of [Article].
     */
    override fun getArticles(): Flow<List<Article>> {
        return articleDao.getArticles().map {
            it.asDomainObjects()
        }
    }

    /**
     * Retrieves a [Flow] of [Article] based on the given ID from the local database.
     *
     * @param id The ID of the article to retrieve.
     * @return A [Flow] emitting the requested [Article].
     */
    override fun getArticle(id: Int): Flow<Article> {
        return articleDao.getArticle(id).map {
            it.asDomainArticle()
        }
    }

    /**
     * Refreshes the local database by fetching articles from the remote API source.
     * In case of a SocketTimeoutException, logs an error message indicating API call timeout.
     */
    override suspend fun refresh() {
        try {
            articleApiService.getArticlesAsFlow().collect {
                for (article in it.asDomainObjects()) {
                    Log.i("ArticleRepository", "refresh: $article")
                    insert(article)
                }
            }
        } catch (e: SocketTimeoutException) {
            Log.e("ArticleRepository", "refresh: API call timed out")
        }
    }
}
