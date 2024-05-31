package com.example.oogartsapp.model

/**
 * Represents an employee.
 *
 * @property id The unique identifier for the employee. Defaults to 0.
 * @property firstName The first name of the employee.
 * @property lastName The last name of the employee.
 * @property email The email address of the employee. Defaults to an empty string.
 * @property image The image URL or path associated with the employee. Defaults to an empty string.
 * @property group The [Group] the employee belongs to.
 * @property birthdate The birthdate of the employee. Defaults to an empty string.
 * @property phone The phone number of the employee. Defaults to an empty string.
 */
data class Employee(
    var id: Int = 0,
    var firstName: String,
    var lastName: String,
    var email: String = "",
    var image: String = "",
    var group: Group,
    var birthdate: String = "",
    var phone: String = "",
)
