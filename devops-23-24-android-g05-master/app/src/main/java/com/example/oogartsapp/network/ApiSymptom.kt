package com.example.oogartsapp.network

import kotlinx.serialization.Serializable

/**
 * Data class representing a symptom retrieved from the API with [id] and [name] properties.
 *
 * @property id The unique identifier of the symptom.
 * @property name The name of the symptom.
 */
@Serializable
data class ApiSymptom(
    val id: Int,
    val name: String,
)
