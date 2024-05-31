package com.example.oogartsapp.ui.navigation

import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.NavigationBarItemDefaults
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.vector.ImageVector

/**
 * BottomAppBar Composable function for displaying the list of items with their corresponding icons,
 * allowing selection and triggering an action on item selection.
 *
 * @param items [List] of strings representing the labels for the items.
 * @param icons [List] of [ImageVector] representing the icons for the items.
 * @param selectedItem The currently selected item index.
 * @param onItemSelected Callback function triggered when an item is selected. Receives the index of the selected item.
 */
@Composable
fun BottomAppBar(
    items: List<String>,
    icons: List<ImageVector>,
    selectedItem: Int,
    onItemSelected: (Int) -> Unit,
) {
    NavigationBar(
        containerColor = Color(0xFF5BBEBE),
        contentColor = MaterialTheme.colorScheme.surfaceVariant,
    ) {
        items.forEachIndexed { index, item ->
            NavigationBarItem(
                icon = {
                    Icon(
                        imageVector = icons[index],
                        contentDescription = item,
                    )
                },
                label = { Text(item) },
                selected = selectedItem == index,
                onClick = { onItemSelected(index) },
                colors = NavigationBarItemDefaults.colors(
                    selectedIconColor = Color.Black,
                    selectedTextColor = Color.Black,
                    indicatorColor = MaterialTheme.colorScheme.surfaceVariant,
                ),
            )
        }
    }
}
