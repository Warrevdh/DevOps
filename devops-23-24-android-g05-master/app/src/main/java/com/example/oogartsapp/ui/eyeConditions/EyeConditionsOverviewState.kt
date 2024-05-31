package com.example.oogartsapp.ui.eyeConditions

/**
 * Represents the possible states of an [Eyecondition] API operation.
 */
sealed interface EyeconditionApiState {
    /**
     * Indicates that the API request for eyeconditions is currently in progress.
     */
    object Loading : EyeconditionApiState
    /**
     * Indicates that an error occurred while fetching eyeconditions from the API.
     */
    object Error : EyeconditionApiState
    /**
     * Indicates a successful retrieval of eyeconditions from the API.
     */
    object Success : EyeconditionApiState
}
