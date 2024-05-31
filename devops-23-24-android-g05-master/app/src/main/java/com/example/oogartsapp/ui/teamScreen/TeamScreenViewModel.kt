package com.example.oogartsapp.ui.teamScreen

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.ViewModelProvider.AndroidViewModelFactory.Companion.APPLICATION_KEY
import androidx.lifecycle.viewModelScope
import androidx.lifecycle.viewmodel.initializer
import androidx.lifecycle.viewmodel.viewModelFactory
import com.example.oogartsapp.Application
import com.example.oogartsapp.data.BioRepository
import com.example.oogartsapp.data.GroupRepository
import com.example.oogartsapp.data.TeamRepository
import com.example.oogartsapp.model.Bio
import com.example.oogartsapp.model.Employee
import com.example.oogartsapp.model.Group
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import java.io.IOException

/**
 * ViewModel class responsible for eyecondition data and API states.
 *
 * @param groupRepository Repository handling group data operations.
 * @param employeeRepository Repository handling employee data operations.
 * @param bioRepository Repository handling bio data operations.
 *
 */
class TeamScreenViewModel(
    private val groupRepository: GroupRepository,
    private val employeeRepository: TeamRepository,
    private val bioRepository: BioRepository,
) : ViewModel() {
    /**
     * State representing the API request status for employees.
     */
    var teamApiState: TeamApiState by mutableStateOf(TeamApiState.Loading)
        private set

    /**
     * StateFlow representing the [List] of [Employee] available.
     */

    lateinit var employeeListState: StateFlow<List<Employee>>
    /**
     * StateFlow representing the [List] of [Group] available.
     */

    lateinit var groupListState: StateFlow<List<Group>>
    /**
     * StateFlow representing the [List] of [Bio] available.
     */
    lateinit var bioState: StateFlow<Bio>

    /**
     * Initialization block to fetch initial data of employees, groups and bios.
     */
    init {
        getRepoGroups()
        getRepoEmployees()
        getBio(1)
    }

    /**
     * Function to fetch an [Bio] by its ID.
     *
     * @param id Id of [Bio].
     */
    fun getBio(id: Int) {
        try {
            teamApiState = TeamApiState.Loading
            viewModelScope.launch {
                bioRepository.refresh(id)
                bioState = bioRepository.getBio(id).stateIn(
                    scope = viewModelScope,
                    started = SharingStarted.WhileSubscribed(5_000L),
                    initialValue = Bio(
                        id = 0,
                        info = "",
                    ),
                )
                teamApiState = TeamApiState.Success
            }
        } catch (e: IOException) {
            teamApiState = TeamApiState.Error
        }
    }

    /**
     * Function to fetch a [List] of [Employee].
     */
    private fun getRepoEmployees() {
        try {
            viewModelScope.launch { employeeRepository.refresh() }
            employeeListState = employeeRepository.getTeam().stateIn(
                scope = viewModelScope,
                started = SharingStarted.WhileSubscribed(5_000L),
                initialValue = listOf(),
            )
            teamApiState = TeamApiState.Success
        } catch (e: IOException) {
            teamApiState = TeamApiState.Error
        }
    }

    /**
     * Function to fetch a [List] of [Group].
     */
    private fun getRepoGroups() {
        try {
            viewModelScope.launch { groupRepository.refresh() }
            groupListState = groupRepository.getGroups().stateIn(
                scope = viewModelScope,
                started = SharingStarted.WhileSubscribed(5_000L),
                initialValue = listOf(),
            )
            teamApiState = TeamApiState.Success
        } catch (e: IOException) {
            teamApiState = TeamApiState.Error
        }
    }

    /**
     * Companion object providing a Factory to create instances of [TeamScreenViewModel].
     */
    companion object {
        val Factory: ViewModelProvider.Factory = viewModelFactory {
            initializer {
                val application = this[APPLICATION_KEY] as Application
                val teamRepository = application.container.teamRepository
                val groupRepository = application.container.groupRepository
                val bioRepository = application.container.bioRepository
                TeamScreenViewModel(groupRepository, teamRepository, bioRepository)
            }
        }
    }
}
