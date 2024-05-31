package com.example.oogartsapp.network.items

import com.example.oogartsapp.network.ApiArticle
import kotlinx.serialization.Serializable

/**
 * Represents a serialized response containing a list of articles and the total count.
 *
 * @property articles List of [ApiArticle] objects.
 * @property totalAmount Total count of articles available.
 */
@Serializable
data class ArticleItem(
    val articles: List<ApiArticle>,
    val totalAmount: Int,
)
