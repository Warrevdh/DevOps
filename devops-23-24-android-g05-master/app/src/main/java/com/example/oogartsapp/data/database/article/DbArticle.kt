package com.example.oogartsapp.data.database.article

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.oogartsapp.model.Article

/**
 * Room Entity class representing an Article in the database.
 *
 * @property id The unique identifier for the article.
 * @property title The title of the article.
 * @property imageUrl The URL of the image associated with the article.
 * @property description The description of the article.
 * @property content The content or body text of the article.
 * @property author The author of the article.
 * @property created The creation date or timestamp of the article.
 */
@Entity(tableName = "article")
data class DbArticle(
    @PrimaryKey(autoGenerate = true)
    var id: Int = 0,
    var title: String,
    var imageUrl: String,
    var description: String,
    var content: String = "",
    var author: String = "",
    var created: String = "",
)

/**
 * Extension function converting an [Article] object to its corresponding [DbArticle] object.
 */
fun Article.asDbArticle() = DbArticle(
    id = id,
    title = title,
    imageUrl = imageUrl,
    description = description,
    content = content,
    author = author,
    created = created,
)

/**
 * Extension function converting a [DbArticle] object to its corresponding [Article] object.
 */
fun DbArticle.asDomainArticle() = Article(
    id = id,
    title = title,
    imageUrl = imageUrl,
    description = description,
    content = content,
    author = author,
    created = created,
)

/**
 * Extension function converting a [List] of [DbArticle] objects to a [List] of [Article] objects.
 */
fun List<DbArticle>.asDomainObjects() = map { it.asDomainArticle() }
