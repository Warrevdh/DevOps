package com.example.oogartsapp.ui.eyeConditions

import android.text.Html
import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.layout.ContentScale
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.viewmodel.compose.viewModel
import coil.compose.rememberImagePainter
import coil.transform.CircleCropTransformation
import com.example.oogartsapp.model.Eyecondition

/**
 * DetailScreen Composable function for displaying an eyecondition.
 *
 * @param innerPadding Padding values.
 * @param eyecondition [Eyecondition] object to display.
 * @param onBack Callback function for handling the back action.
 * @param eyeConditionsOverviewViewModel ViewModel for the details of an [Eyecondition].
 */
@Composable
fun DetailScreen(
    innerPadding: PaddingValues,
    eyecondition: Eyecondition,
    onBack: () -> Unit,
    eyeConditionsOverviewViewModel: EyeConditionsOverviewViewModel = viewModel(factory = EyeConditionsOverviewViewModel.Factory),
) {
    val eyeconditionState = eyeConditionsOverviewViewModel.eyeconditionState.collectAsState()
    val eyeconditionApiState = eyeConditionsOverviewViewModel.eyeconditionApiState

    LaunchedEffect(Unit) {
        eyeConditionsOverviewViewModel.getEyecondition(eyecondition.id)
    }

    Box(
        modifier = Modifier
            .fillMaxWidth()
            .padding(innerPadding),
    ) {
        Column(
            modifier = Modifier
                .fillMaxWidth(),
        ) {
            IconButton(
                onClick = { onBack() },
            ) {
                Icon(
                    imageVector = Icons.Default.ArrowBack,
                    contentDescription = "Back",
                )
            }
        }
        when (eyeconditionApiState) {
            EyeconditionApiState.Error -> {
                Box(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(innerPadding),
                    contentAlignment = Alignment.Center,
                ) {
                    Text(
                        text = "De data kon niet geladen worden.",
                        fontSize = 24.sp,
                        fontWeight = FontWeight.Bold,
                        textAlign = TextAlign.Center,
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(bottom = 16.dp),
                    )
                }
            }
            EyeconditionApiState.Loading -> {
                Box(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(innerPadding),
                    contentAlignment = Alignment.Center,
                ) {
                    Text(
                        text = "De data wordt geladen...",
                        fontSize = 24.sp,
                        fontWeight = FontWeight.Bold,
                        textAlign = TextAlign.Center,
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(bottom = 16.dp),
                    )
                }
            }
            EyeconditionApiState.Success -> {
                Column(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(top = 50.dp)
                        .verticalScroll(rememberScrollState()),
                    horizontalAlignment = Alignment.CenterHorizontally,
                ) {
                    Text(
                        text = eyeconditionState.value.name,
                        fontSize = 24.sp,
                        fontWeight = FontWeight.Bold,
                        textAlign = TextAlign.Center,
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(bottom = 16.dp),
                    )
                    Image(
                        painter = rememberImagePainter(
                            data = eyeconditionState.value.imageUrl,
                            builder = {
                                transformations(CircleCropTransformation())
                            },
                        ),
                        contentDescription = null,
                        modifier = Modifier
                            .size(200.dp)
                            .clip(RoundedCornerShape(8.dp)),
                        contentScale = ContentScale.Crop,
                    )
                    Spacer(modifier = Modifier.height(16.dp))
                    Text(
                        text = eyeconditionState.value.description,
                        fontSize = 16.sp,
                        textAlign = TextAlign.Center,
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(bottom = 16.dp),
                    )
                    Text(
                        text = Html.fromHtml(eyeconditionState.value.body, Html.FROM_HTML_MODE_LEGACY)
                            .toString(),
                        fontSize = 16.sp,
                        textAlign = TextAlign.Center,
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(bottom = 16.dp),
                    )
                    if (eyeconditionState.value.brochureUrl!!.isNotBlank()) {
                        Text(
                            text = eyeconditionState.value.brochureUrl!!,
                            fontSize = 16.sp,
                            textAlign = TextAlign.Center,
                            modifier = Modifier
                                .fillMaxWidth()
                                .padding(bottom = 16.dp),
                        )
                    }
                }
            }
        }
    }
}
