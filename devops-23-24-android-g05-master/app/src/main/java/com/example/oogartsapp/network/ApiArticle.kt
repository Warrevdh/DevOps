package com.example.oogartsapp.network

import com.example.oogartsapp.model.Article
import kotlinx.serialization.Serializable

/**
 * Serializable data class representing a simplified version of an article from the API.
 *
 * @property id Unique identifier of the article.
 * @property title Title of the article.
 * @property imageUrl URL for the image associated with the article.
 * @property description Brief description of the article.
 */
@Serializable
data class ApiArticle(
    val id: Int,
    var title: String,
    var imageUrl: String,
    var description: String,
)

/**
 * Serializable data class representing detailed information of an article from the API.
 *
 * @property id Unique identifier of the article.
 * @property title Title of the article.
 * @property imageUrl URL for the image associated with the article.
 * @property description Brief description of the article.
 * @property content Full content of the article.
 * @property author Author of the article (nullable).
 * @property created Date and time when the article was created.
 */
@Serializable
data class ApiArticleDetail(
    val id: Int,
    var title: String,
    var imageUrl: String,
    var description: String,
    var content: String,
    var author: String?,
    var created: String,
)

/**
 * Extension function to convert a list of [ApiArticle] objects into a list of domain [Article] objects.
 *
 * @return List of domain [Article] objects.
 */
fun List<ApiArticle>.asDomainObjects(): List<Article> {
    return this.map {
        Article(
            id = it.id,
            title = it.title,
            imageUrl = it.imageUrl,
            description = it.description,
        )
    }
}

/**
 * Extension function to convert an [ApiArticle] object into a domain [Article] object.
 *
 * @return Domain [Article] object.
 */
fun ApiArticle.asDomainObject() = Article(
    id = this.id,
    title = this.title,
    imageUrl = this.imageUrl,
    description = this.description,
)

/**
 * Extension function to convert an [ApiArticleDetail] object into a domain [Article] object.
 *
 * @return Domain [Article] object.
 */
fun ApiArticleDetail.asDomainObject() = Article(
    id = this.id,
    title = this.title,
    imageUrl = this.imageUrl,
    description = this.description,
    content = this.content,
    author = this.author ?: "",
    created = this.created,
)
