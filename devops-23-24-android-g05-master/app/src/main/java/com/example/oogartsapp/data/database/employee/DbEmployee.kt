package com.example.oogartsapp.data.database.employee

import androidx.room.Embedded
import androidx.room.Entity
import androidx.room.PrimaryKey
import com.example.oogartsapp.data.database.group.DbGroup
import com.example.oogartsapp.data.database.group.asDbGroup
import com.example.oogartsapp.data.database.group.asDomainGroup
import com.example.oogartsapp.model.Employee

/**
 * Room Entity class representing an Employee in the database.
 *
 * @property id The unique identifier for the employee.
 * @property firstName The first name of the employee.
 * @property lastName The last name of the employee.
 * @property image The image URL of the employee.
 * @property email The email address of the employee.
 * @property birthdate The birthdate of the employee.
 * @property phone The phone number of the employee.
 * @property group The group the employee belongs to, represented by [DbGroup].
 */
@Entity(tableName = "employee")
data class DbEmployee(
    @PrimaryKey(autoGenerate = true)
    var id: Int = 0,
    var firstName: String = "",
    var lastName: String = "",
    var image: String = "",
    var email: String = "",
    var birthdate: String = "",
    var phone: String = "",
    @Embedded var group: DbGroup,
)

/**
 * Extension function to convert an [Employee] domain model object to a [DbEmployee] Room entity.
 * @return [DbEmployee] instance mapped from the [Employee] object.
 */
fun Employee.asDbEmployee() = DbEmployee(
    id = id,
    firstName = firstName,
    lastName = lastName,
    image = image,
    email = email,
    group = group.asDbGroup(),
    birthdate = birthdate,
    phone = phone,
)

/**
 * Extension function to convert a [DbEmployee] Room entity to an [Employee] domain model object.
 * @return [Employee] instance mapped from the [DbEmployee] object.
 */
fun DbEmployee.asDomainEmployee() = Employee(
    id = id,
    firstName = firstName,
    lastName = lastName,
    image = image,
    email = email,
    group = group.asDomainGroup(),
    birthdate = birthdate,
    phone = phone,
)

/**
 * Extension function to convert a list of [DbEmployee] Room entities to a list of [Employee] domain model objects.
 * @return List containing [Employee] instances mapped from [DbEmployee] objects.
 */
fun List<DbEmployee>.asDomainObjects() = map { it.asDomainEmployee() }
