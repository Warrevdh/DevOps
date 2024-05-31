package com.example.oogartsapp.network

import com.example.oogartsapp.model.Bio
import kotlinx.serialization.Serializable

/**
 * Serializable data class representing bio information retrieved from the API.
 *
 * @property id Unique identifier of the bio.
 * @property info Information or details related to the bio.
 */
@Serializable
data class ApiBio(
    val id: Int,
    var info: String,
)

/**
 * Extension function to convert an [ApiBio] object into a domain [Bio] object.
 *
 * @return Domain [Bio] object.
 */
fun ApiBio.asDomainObject() = Bio(
    id = this.id,
    info = this.info,
)
