package com.example.oogartsapp

import android.app.Application
import com.example.oogartsapp.data.AppContainer
import com.example.oogartsapp.data.DefaultAppContainer

/**
 * Custom Application class responsible for initializing the AppContainer.
 */
class Application : Application() {
    lateinit var container: AppContainer

    /**
     * Overridden method called when the application is starting.
     * Initializes the AppContainer when the application starts.
     */
    override fun onCreate() {
        super.onCreate()
        container = DefaultAppContainer(applicationContext)
    }
}
