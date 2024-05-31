package com.example.oogartsapp.model

/**
 * Represents a group.
 *
 * @property id The unique identifier for the group. Defaults to 0.
 * @property name The name of the group.
 * @property sequence The sequence number associated with the group.
 */
data class Group(
    var id: Int = 0,
    var name: String,
    var sequence: Int,
)
