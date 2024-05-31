package com.example.oogartsapp.ui.teamScreen

import com.example.oogartsapp.model.Employee

/**
 * Represents the state for the Team Screen, including bio information.
 *
 * @property bio Bio information displayed on the Team Screen.
 */
data class TeamScreenState(
    val bio: String = "",
)

/**
 * Represents the state containing a list of employees.
 *
 * @property employees List of employees to be displayed.
 */
data class EmployeeListState(
    val employees: List<Employee> = listOf(),
)

/**
 * Represents the state containing a list of groups.
 *
 * @property groups List of groups for team organization.
 */
data class GroupListState(
    val groups: List<String> = listOf(),
)

/**
 * Represents the possible states of employees API operation.
 */
sealed interface TeamApiState {
    /**
     * Indicates that the API request for employees is currently in progress.
     */
    object Loading : TeamApiState
    /**
     * Indicates that an error occurred while fetching employees from the API.
     */
    object Error : TeamApiState
    /**
     * Indicates a successful retrieval of employees from the API.
     */
    object Success : TeamApiState
}
