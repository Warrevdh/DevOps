package com.example.oogartsapp.data.database.eyecondition

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.oogartsapp.model.Eyecondition

/**
 * Represents the "eyecondition" table in the Room database.
 * @param id Unique identifier for the eye condition.
 * @param name Name of the eye condition.
 * @param description Description of the eye condition.
 * @param body Additional body information about the eye condition (nullable).
 * @param imageUrl URL for the image associated with the eye condition (nullable).
 * @param brochureUrl URL for the brochure associated with the eye condition (nullable).
 */
@Entity(tableName = "eyecondition")
data class DbEyecondition(
    @PrimaryKey(autoGenerate = true)
    var id: Int = 0,
    var name: String,
    var description: String,
    var body: String? = null,
    var imageUrl: String? = null,
    var brochureUrl: String? = null,
)

/**
 * Converts an [Eyecondition] domain object to its corresponding [DbEyecondition] entity object.
 *
 * @receiver The [Eyecondition] domain object to be converted.
 * @return A [DbEyecondition] entity object representing the converted [Eyecondition] domain.
 */
fun Eyecondition.asDbEyecondition() = DbEyecondition(
    id = id,
    name = name,
    description = description,
    body = body,
    imageUrl = imageUrl,
    brochureUrl = brochureUrl,
)

/**
 * Converts a [DbEyecondition] entity object to its corresponding [Eyecondition] domain object.
 *
 * @receiver The [DbEyecondition] entity object to be converted.
 * @return An [Eyecondition] domain object representing the converted [DbEyecondition] entity.
 */
fun DbEyecondition.asDomainEyecondition() = Eyecondition(
    id = id,
    name = name,
    description = description,
    body = body,
    imageUrl = imageUrl,
    brochureUrl = brochureUrl,
)

/**
 * Converts a list of [DbEyecondition] objects to a list of [Eyecondition] objects.
 */
fun List<DbEyecondition>.asDomainObjects() = map { it.asDomainEyecondition() }
