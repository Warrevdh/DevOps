package com.example.oogartsapp.model

/**
 * Represents a team member.
 *
 * @property id The unique identifier for the team member.
 * @property firstName The first name of the team member.
 * @property lastName The last name of the team member.
 * @property role The role or position of the team member.
 * @property phoneNumber The phone number of the team member.
 * @property email The email address of the team member.
 * @property availability The availability status of the team member.
 */
data class Team(
    val id: Int,
    var firstName: String,
    var lastName: String,
    var role: String,
    var phoneNumber: String,
    var email: String,
    var availability: String,
)
