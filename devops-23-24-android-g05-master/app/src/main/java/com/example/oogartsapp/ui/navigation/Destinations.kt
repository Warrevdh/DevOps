package com.example.oogartsapp.ui.navigation

import androidx.annotation.StringRes
import com.example.oogartsapp.R

/**
 * Enum class defining the destinations of the application.
 *
 * @property title The resource ID for the title of the destination.
 */
enum class Destinations (@StringRes val title: Int) {
    Start(title = R.string.home),
    Team(title = R.string.team),
    EyeConditions(title = R.string.eye_conditions),
}