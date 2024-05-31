package com.example.oogartsapp.data.database.group

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.oogartsapp.model.Group

/**
 * Represents the database entity for a Group in the application. This entity is used to store
 * group-related information in the Room database.
 *
 * @property groupId The unique identifier for the group.
 * @property name The name of the group.
 * @property sequence The sequence of the group.
 */
@Entity(
    tableName = "group",
)
data class DbGroup(
    @PrimaryKey(autoGenerate = true)
    var groupId: Int = 0,
    var name: String = "",
    var sequence: Int = 0,
)

/**
 * Converts a [DbGroup] entity object to its corresponding [Group] domain object.
 *
 * @receiver The [DbGroup] entity object to be converted.
 * @return A [Group] domain object representing the converted [DbGroup] entity.
 */
fun DbGroup.asDomainGroup() = Group(
    id = groupId,
    name = name,
    sequence = sequence,
)

/**
 * Converts a [Group] domain object to its corresponding [DbGroup] entity object.
 *
 * @receiver The [Group] domain object to be converted.
 * @return A [DbGroup] entity object representing the converted [Group] domain.
 */
fun Group.asDbGroup() = DbGroup(
    groupId = id,
    name = name,
    sequence = sequence,
)

/**
 * Converts a list of [DbGroup] entity objects to a list of corresponding [Group] domain objects.
 *
 * @receiver The list of [DbGroup] entity objects to be converted.
 * @return A list of [Group] domain objects representing the converted [DbGroup] entities.
 */
fun List<DbGroup>.asDomainObjects() = map { it.asDomainGroup() }
