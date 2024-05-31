package com.example.oogartsapp.network.items

import com.example.oogartsapp.network.ApiEmployee
import kotlinx.serialization.Serializable

/**
 * Represents a serialized response containing a list of employees and the total count.
 *
 * @property employees List of [ApiEmployee] objects.
 * @property totalAmount Total count of employees available.
 */
@Serializable
data class EmployeeItem(
    val employees: List<ApiEmployee>,
    val totalAmount: Int,
)
