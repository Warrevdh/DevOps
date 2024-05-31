package com.example.oogartsapp.network

import com.example.oogartsapp.model.Eyecondition
import kotlinx.serialization.Serializable

/**
 * Serializable data class representing eye condition information retrieved from the API.
 *
 * @property id Unique identifier of the eye condition.
 * @property name Name of the eye condition.
 * @property description Description of the eye condition.
 * @property imageUrl Image URL representing the eye condition's image.
 * @property body Body text providing additional information about the eye condition.
 * @property brochureUrl URL pointing to a brochure related to the eye condition.
 * @property symptoms List of [ApiSymptom] objects representing symptoms associated with the eye condition.
 */
@Serializable
data class ApiEyecondition(
    val id: Int,
    var name: String,
    var description: String,
    var imageUrl: String,
    var body: String? = null,
    var brochureUrl: String? = null,
    var symptoms: List<ApiSymptom>,
)

/**
 * Serializable data class representing detailed eye condition information retrieved from the API.
 *
 * @property id Unique identifier of the eye condition.
 * @property name Name of the eye condition.
 * @property description Description of the eye condition.
 * @property imageUrl Image URL representing the eye condition's image.
 * @property body Body text providing additional information about the eye condition.
 * @property brochureUrl URL pointing to a brochure related to the eye condition.
 */
@Serializable
data class ApiEyeconditionDetail(
    val id: Int,
    var name: String,
    var description: String,
    var imageUrl: String,
    var body: String? = null,
    var brochureUrl: String? = null,
)

/**
 * Extension function to convert a list of [ApiEyecondition] objects into a list of domain [Eyecondition] objects.
 *
 * @return List of domain [Eyecondition] objects.
 */
fun List<ApiEyecondition>.asDomainObjects(): List<Eyecondition> {
    return this.map {
        Eyecondition(
            id = it.id,
            name = it.name,
            imageUrl = it.imageUrl,
            description = it.description,
        )
    }
}

/**
 * Extension function to convert an instance of [ApiEyecondition] into a domain [Eyecondition] object.
 *
 * @return Converted [Eyecondition] object.
 */
fun ApiEyecondition.asDomainObject() = Eyecondition(
    id = this.id,
    name = this.name,
    imageUrl = this.imageUrl,
    description = this.description,
)

/**
 * Extension function to convert an instance of [ApiEyeconditionDetail] into a domain [Eyecondition] object.
 *
 * @return Converted [Eyecondition] object.
 */
fun ApiEyeconditionDetail.asDomainObject() = Eyecondition(
    id = this.id,
    name = this.name,
    imageUrl = this.imageUrl,
    description = this.description,
    body = this.body ?: "",
    brochureUrl = this.brochureUrl ?: "",
)
