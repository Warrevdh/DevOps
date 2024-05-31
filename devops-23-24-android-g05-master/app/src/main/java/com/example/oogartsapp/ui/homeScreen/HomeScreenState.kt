package com.example.oogartsapp.ui.homeScreen

/**
* Represents the possible states of an HomeScreen API operation.
*/
sealed interface HomeApiState {
    /**
     * Represents the loading state of the API response.
     */
    object Loading : HomeApiState
    /**
     * Represents the error state of the API response.
     */
    object Error : HomeApiState
    /**
     * Represents the success state of the API response.
     */
    object Success : HomeApiState
}
