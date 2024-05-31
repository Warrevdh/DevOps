package com.example.oogartsapp.ui

import androidx.compose.foundation.layout.padding
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.outlined.Biotech
import androidx.compose.material.icons.outlined.DateRange
import androidx.compose.material.icons.outlined.Home
import androidx.compose.material.icons.outlined.Person
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.example.oogartsapp.R
import com.example.oogartsapp.ui.eyeConditions.EyeConditionsOverview
import com.example.oogartsapp.ui.homeScreen.HomeScreen
import com.example.oogartsapp.ui.navigation.BottomAppBar
import com.example.oogartsapp.ui.navigation.Destinations
import com.example.oogartsapp.ui.navigation.TopAppBar
import com.example.oogartsapp.ui.teamScreen.TeamScreen

/**
 * Composable function representing the main entry point of the OogartsApp.
 *
 * @param navController NavHostController used for navigation within the app.
 */
@Composable
fun OogartsApp(
    navController: NavHostController = rememberNavController(),
) {
    var selectedItem by remember { mutableStateOf(0) }
    val items = listOf("Home", "Team", "Oogaandoeningen")
    val icons = listOf(Icons.Outlined.Home, Icons.Outlined.Person, Icons.Outlined.Biotech)

    Scaffold(
        topBar = {
            TopAppBar(
                modifier = Modifier.padding(8.dp),
                title = when (selectedItem) {
                    1 -> R.string.team
                    2 -> R.string.eye_conditions
                    else -> null // No title, will result in no TopAppBar being rendered
                },
            )
        },
        bottomBar = {
            BottomAppBar(
                items = items,
                icons = icons,
                selectedItem = selectedItem,
                onItemSelected = { index ->
                    selectedItem = index
                    navController.navigate(Destinations.values()[index].name)
                },
            )
        },
    ) { innerPadding ->
        NavHost(navController = navController, startDestination = Destinations.Start.name) {
            composable(route = Destinations.Start.name) {
                HomeScreen(innerPadding = innerPadding)
            }
            composable(route = Destinations.Team.name) {
                TeamScreen(innerPadding = innerPadding)
            }
            composable(route = Destinations.EyeConditions.name) {
                EyeConditionsOverview(innerPadding = innerPadding)
            }
        }
    }
}
