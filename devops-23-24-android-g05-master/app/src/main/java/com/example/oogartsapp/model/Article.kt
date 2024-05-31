package com.example.oogartsapp.model

/**
 * Represents an article.
 *
 * @property id The unique identifier of the article.
 * @property title The title of the article.
 * @property imageUrl The URL pointing to the image associated with the article.
 * @property description A brief description or summary of the article.
 * @property content The content or body of the article. Defaults to an empty string.
 * @property author The author of the article. Defaults to an empty string.
 * @property created The creation date of the article. Defaults to an empty string.
 */
data class Article(
    val id: Int,
    var title: String,
    var imageUrl: String,
    var description: String,
    var content: String = "",
    var author: String = "",
    var created: String = "",
)
