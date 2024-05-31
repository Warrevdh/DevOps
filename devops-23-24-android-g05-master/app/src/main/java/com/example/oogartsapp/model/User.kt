package com.example.oogartsapp.model

/**
 * Represents a user.
 *
 * @property id The unique identifier for the user.
 * @property teamMemberId The identifier referencing a team member, if applicable.
 * @property email The email address of the user.
 * @property passwordHash The hashed password of the user.
 * @property googleLoginId The Google login identifier associated with the user, if available.
 * @property twoFactorAuthEnabled Flag indicating whether two-factor authentication is enabled for the user.
 */
data class User(
    val id: Int,
    var teamMemberId: Int?,
    var email: String,
    var passwordHash: String,
    var googleLoginId: String?,
    var twoFactorAuthEnabled: Boolean
)
