package com.example.oogartsapp.network

import com.example.oogartsapp.model.Employee
import kotlinx.serialization.Serializable

/**
 * Serializable data class representing employee information retrieved from the API.
 *
 * @property id Unique identifier of the employee.
 * @property firstname First name of the employee.
 * @property lastname Last name of the employee.
 * @property email Email address of the employee.
 * @property image Image URL representing the employee's image.
 * @property group [ApiGroup] object representing the group to which the employee belongs.
 * @property birthdate Date of birth of the employee.
 * @property phone Phone number of the employee.
 */
@Serializable
data class ApiEmployee(
    var id: Int = 0,
    var firstname: String,
    var lastname: String,
    var email: String = "",
    var image: String = "",
    var group: ApiGroup,
    var birthdate: String = "",
    var phone: String = "",
)

/**
 * Extension function to convert a list of [ApiEmployee] objects into a list of domain [Employee] objects.
 *
 * @return List of domain [Employee] objects.
 */
fun List<ApiEmployee>.asDomainObjects(): List<Employee> {
    return this.map {
        Employee(
            id = it.id,
            firstName = it.firstname,
            lastName = it.lastname,
            email = it.email,
            image = it.image,
            group = it.group.asDomainObject(),
            birthdate = it.birthdate,
            phone = it.phone,
        )
    }
}
