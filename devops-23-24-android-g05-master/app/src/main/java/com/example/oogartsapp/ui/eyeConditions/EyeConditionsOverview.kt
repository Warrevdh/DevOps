package com.example.oogartsapp.ui.eyeConditions

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.example.oogartsapp.model.Eyecondition
import com.example.oogartsapp.ui.components.Card

/**
 * EyeConditionsOverview Composable function for displaying an overview of eyeconditions.
 *
 * @param eyeConditionsOverviewViewModel ViewModel for handling eyeconditions.
 * @param innerPadding Padding values.
 */
@Composable
fun EyeConditionsOverview(
    eyeConditionsOverviewViewModel: EyeConditionsOverviewViewModel = viewModel(factory = EyeConditionsOverviewViewModel.Factory),
    innerPadding: PaddingValues,
) {
    val eyeconditionListState by eyeConditionsOverviewViewModel.uiListState.collectAsState()
    val eyeconditionApiState = eyeConditionsOverviewViewModel.eyeconditionApiState

    var selectedEyecondition by remember { mutableStateOf<Eyecondition?>(null) }

    if (selectedEyecondition != null) {
        DetailScreen(
            innerPadding = innerPadding,
            eyecondition = selectedEyecondition!!,
            onBack = { selectedEyecondition = null },
        )
    } else {
        when (eyeconditionApiState) {
            EyeconditionApiState.Error -> {}
            EyeconditionApiState.Loading -> {}
            EyeconditionApiState.Success -> {
                Box(modifier = Modifier.padding(innerPadding)) {
                    LazyColumn(
                        contentPadding = PaddingValues(8.dp),
                    ) {
                        for (eyecondition in eyeconditionListState) {
                            item {
                                Card(
                                    image = eyecondition.imageUrl ?: "",
                                    imageDescription = eyecondition.description,
                                    title = eyecondition.name,
                                    description = eyecondition.description,
                                    onClick = { selectedEyecondition = eyecondition },
                                )
                                Spacer(modifier = Modifier.height(20.dp))
                            }
                        }
                    }
                }
            }
        }
    }
}
