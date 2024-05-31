package com.example.oogartsapp.network.service

import com.example.oogartsapp.network.ApiArticleDetail
import com.example.oogartsapp.network.items.ArticleItem
import kotlinx.coroutines.flow.flow
import retrofit2.http.GET
import retrofit2.http.Path

/**
 * Interface defining functions to interact with the Article API.
 */
interface ArticleApiService {
    /**
     * Fetches a list of articles from the API.
     *
     * @return [ArticleItem] containing a list of articles and total count.
     */
    @GET("Article")
    suspend fun getArticles(): ArticleItem

    /**
     * Fetches a single article from the API based on the provided ID.
     *
     * @param id The ID of the article to retrieve.
     * @return [ApiArticleDetail] representing the details of the article.
     */
    @GET("Article/{id}")
    suspend fun getArticle(@Path("id") id: Int): ApiArticleDetail
}


/**
 * Extension function to convert the result of [getArticles] into a Flow emitting the list of articles.
 *
 * @return Flow emitting the list of articles.
 */
fun ArticleApiService.getArticlesAsFlow() = flow { emit(getArticles().articles) }

/**
 * Extension function to convert the result of [getArticle] into a Flow emitting the article based on ID.
 *
 * @param id The ID of the article to retrieve.
 * @return Flow emitting the article corresponding to the provided ID.
 */
fun ArticleApiService.getArticleAsFlow(id: Int) = flow { emit(getArticle(id)) }
