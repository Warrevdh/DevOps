package com.example.oogartsapp.network

import androidx.room.PrimaryKey
import com.example.oogartsapp.model.Group
import kotlinx.serialization.Serializable

/**
 * Data class representing an API group with [id], [name], and [sequence] properties.
 *
 * @property id The unique identifier of the group.
 * @property name The name of the group.
 * @property sequence The sequence number of the group.
 */
@Serializable
data class ApiGroup(
    @PrimaryKey(autoGenerate = true)
    var id: Int = 0,
    var name: String,
    var sequence: Int,
)

/**
 * Extension function to convert an instance of [ApiGroup] into a domain [Group] object.
 *
 * @return Converted [Group] object.
 */
fun ApiGroup.asDomainObject(): Group {
    return Group(
        id = id,
        name = name,
        sequence = sequence,
    )
}

/**
 * Extension function to convert a list of [ApiGroup] objects into a list of domain [Group] objects.
 *
 * @return Converted list of [Group] objects.
 */
fun List<ApiGroup>.asDomainObjects(): List<Group> {
    return this.map {
        Group(
            id = it.id,
            name = it.name,
            sequence = it.sequence,
        )
    }
}
