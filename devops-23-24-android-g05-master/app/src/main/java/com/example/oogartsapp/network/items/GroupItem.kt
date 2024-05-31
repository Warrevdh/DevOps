package com.example.oogartsapp.network.items

import com.example.oogartsapp.network.ApiGroup
import kotlinx.serialization.Serializable

/**
 * Represents a serialized response containing a list of groups and the total count.
 *
 * @property groups List of [ApiGroup] objects.
 * @property totalAmount Total count of groups available.
 */
@Serializable
data class GroupItem(
    val groups: List<ApiGroup>,
    val totalAmount: Int,
)
