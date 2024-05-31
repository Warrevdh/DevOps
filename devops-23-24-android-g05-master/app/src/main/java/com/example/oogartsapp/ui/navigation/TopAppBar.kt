package com.example.oogartsapp.ui.navigation

import androidx.annotation.StringRes
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource

/**
 * TopAppBar Composable function that creates a top app bar.
 *
 * @param modifier Modifier to be applied to the layout.
 * @param title The resource ID for the title displayed in the app bar. Will not be display,
 * if title is absent.
 */
@Composable
@OptIn(ExperimentalMaterial3Api::class)
fun TopAppBar(
    modifier: Modifier = Modifier,
    @StringRes title: Int?,
) {
    title?.let {
        androidx.compose.material3.TopAppBar(
            modifier = modifier,
            title = {
                Text(
                    text = stringResource(it),
                    style = MaterialTheme.typography.titleMedium,
                    color = MaterialTheme.colorScheme.onSurface,
                )
            },
        )
    }
}
