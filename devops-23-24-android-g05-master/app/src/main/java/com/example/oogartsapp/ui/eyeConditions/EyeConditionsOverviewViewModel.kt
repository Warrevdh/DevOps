package com.example.oogartsapp.ui.eyeConditions

import android.text.Editable.Factory
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
import com.example.oogartsapp.data.EyeconditionRepository
import com.example.oogartsapp.model.Eyecondition
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch

/**
 * ViewModel class responsible for eyecondition data and API states.
 *
 * @param eyeconditionRepository Repository handling eyecondition data operations.
 */
class EyeConditionsOverviewViewModel(
    private val eyeconditionRepository: EyeconditionRepository,
) : ViewModel() {
    /**
     * State representing the API request status for an eyecondition.
     */
    var eyeconditionApiState: EyeconditionApiState by mutableStateOf(EyeconditionApiState.Loading)
        private set

    /**
     * StateFlow representing the list of eyeconditions available.
     */
    lateinit var uiListState: StateFlow<List<Eyecondition>>

    /**
     * StateFlow representing a single eyecondition for detailed view.
     */
    lateinit var eyeconditionState: StateFlow<Eyecondition>

    /**
     * Initialization block to fetch initial data of eyeconditions.
     */
    init {
        getEyeconditions()
        getEyecondition(1)
    }

    /**
     * Function to fetch an eyecondition by its ID.
     *
     * @param id Id of eyecondition.
     */
    fun getEyecondition(id: Int) {
        try {
            eyeconditionApiState = EyeconditionApiState.Loading
            viewModelScope.launch {
                eyeconditionRepository.insertEyecondition(id)
                eyeconditionState = eyeconditionRepository.getEyecondition(id).stateIn(
                    scope = viewModelScope,
                    started = SharingStarted.WhileSubscribed(5_000L),
                    initialValue = Eyecondition(
                        id = 0,
                        name = "",
                        imageUrl = "",
                        description = "",
                        body = "",
                        brochureUrl = "",
                    ),
                )
                eyeconditionApiState = EyeconditionApiState.Success
            }
        } catch (e: Exception) {
            eyeconditionApiState = EyeconditionApiState.Error
        }
    }


    /**
     * Function to fetch a [List] of [Eyecondition].
     */
    private fun getEyeconditions() {
        try {
            viewModelScope.launch { eyeconditionRepository.refresh() }
            uiListState = eyeconditionRepository.getEyeconditions().stateIn(
                scope = viewModelScope,
                started = SharingStarted.WhileSubscribed(5_000L),
                initialValue = listOf(),
            )
            eyeconditionApiState = EyeconditionApiState.Success
        } catch (e: Exception) {
            eyeconditionApiState = EyeconditionApiState.Error
        }
    }

    /**
     * Companion object providing a Factory to create instances of [EyeConditionsOverviewViewModel].
     */
    companion object {
        val Factory: ViewModelProvider.Factory = viewModelFactory {
            initializer {
                val application = this[APPLICATION_KEY] as Application
                val eyeconditionRepository = application.container.eyeconditionRepository
                EyeConditionsOverviewViewModel(eyeconditionRepository)
            }
        }
    }
}
