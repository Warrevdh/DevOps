package com.example.oogartsapp.data.database.bio

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.oogartsapp.model.Bio

/**
 * Room Entity representing the "bio" table in the database.
 */
@Entity(tableName = "bio")
data class DbBio(
    @PrimaryKey(autoGenerate = true)
    var id: Int = 0,
    var info: String? = "",
)

/**
 * Extension function to convert a [Bio] domain model object to a [DbBio] Room entity.
 * @return [DbBio] instance mapped from the [Bio] object.
 */
fun Bio.asDbBio() = DbBio(
    id = id,
    info = info ?: "",
)

/**
 * Extension function to convert a DbBio Room entity to a [Bio] domain model object.
 * @return [Bio] instance mapped from the [DbBio] object.
 */
fun DbBio.asDomainBio() = Bio(
    id = id,
    info = info ?: "",
)
