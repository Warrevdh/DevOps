package com.example.oogartsapp

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.ui.Modifier
import com.example.compose.OogartsAppTheme
import com.example.oogartsapp.ui.OogartsApp

/**
 * The main entry point of the Android application.
 * This Activity sets the content to display the OogartsApp.
 */
class MainActivity : ComponentActivity() {
    /**
     * Called when the activity is starting.
     * It sets the content to display the OogartsApp within the current activity.
     */
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContent {
            OogartsAppTheme {
                Surface(
                    modifier = Modifier.fillMaxSize(),
                    color = MaterialTheme.colorScheme.background,
                ) {
                    OogartsApp()
                }
            }
        }
    }
}
