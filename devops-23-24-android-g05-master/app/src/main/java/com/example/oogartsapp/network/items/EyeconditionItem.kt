package com.example.oogartsapp.network.items

import com.example.oogartsapp.network.ApiEyecondition
import kotlinx.serialization.Serializable

/**
 * Represents a serialized response containing a list of eyeconditions and the total count.
 *
 * @property eyeConditions List of [ApiEyecondition] objects.
 * @property totalAmount Total count of eyeconditions available.
 */
@Serializable
data class EyeconditionItem(
    val eyeConditions: List<ApiEyecondition>,
    val totalAmount: Int,
)
