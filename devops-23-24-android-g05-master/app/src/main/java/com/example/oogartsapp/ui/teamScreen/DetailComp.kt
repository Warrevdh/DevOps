package com.example.oogartsapp.ui.teamScreen

import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.painter.Painter
import androidx.compose.ui.layout.ContentScale
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.viewmodel.compose.viewModel
import coil.compose.rememberImagePainter
import coil.transform.CircleCropTransformation
import com.example.oogartsapp.model.Employee

/**
 * DetailComp Composable function displaying details of a teammember.
 *
 * @param innerPadding Padding values.
 * @param employee [Employee] object to display.
 * @param onBack Callback function for handling the back action.
 * @param teamScreenViewModel ViewModel managing team data and API states.
 */
@Composable
fun DetailComp(
    innerPadding: PaddingValues,
    employee: Employee,
    onBack: () -> Unit,
    teamScreenViewModel: TeamScreenViewModel = viewModel(factory = TeamScreenViewModel.Factory),
) {
    val bioState by teamScreenViewModel.bioState.collectAsState()
    val teamApiState = teamScreenViewModel.teamApiState

    LaunchedEffect(Unit) {
        teamScreenViewModel.getBio(employee.id)
    }

    Box(
        modifier = Modifier
            .fillMaxSize()
            .padding(innerPadding),
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(16.dp),
        ) {
            IconButton(
                onClick = { onBack() },
            ) {
                Icon(
                    imageVector = Icons.Default.ArrowBack,
                    contentDescription = "Back",
                )
            }
            Row(
                modifier = Modifier
                    .padding(8.dp),
                verticalAlignment = Alignment.Top,
            ) {
                val painter: Painter = rememberImagePainter(
                    data = employee.image,
                    builder = {
                        transformations(CircleCropTransformation())
                    },
                )

                Column {
                    Image(
                        painter = painter,
                        contentDescription = null,
                        modifier = Modifier
                            .size(150.dp)
                            .clip(RoundedCornerShape(10.dp)),
                        contentScale = ContentScale.Crop,
                    )
                }
                Column {
                    Spacer(modifier = Modifier.height(8.dp))
                    Text(
                        text = "${employee.firstName} ${employee.lastName}",
                        textAlign = TextAlign.Center,
                        fontSize = 25.sp,
                        fontWeight = FontWeight.Bold,
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(5.dp),
                    )
                    Row(
                        modifier = Modifier
                            .padding(8.dp),
                    ) {
                        /*
                        if (employee.specializations != null) {
                            LazyColumn {
                                items(employee.specializations) { specialization ->
                                    Text(
                                        text = specialization,
                                        textAlign = TextAlign.Center,
                                        fontWeight = FontWeight.SemiBold,
                                        fontStyle = FontStyle.Italic,
                                        modifier = Modifier.fillMaxWidth()
                                    )
                                }
                            }

                        }
                        else {
                            Text(
                                text = employee.function,
                                textAlign = TextAlign.Center,
                                fontWeight = FontWeight.SemiBold,
                                fontStyle = FontStyle.Italic,
                                modifier = Modifier.fillMaxWidth()
                            )
                        }*/
                    }
                }
            }
            when (teamApiState) {
                TeamApiState.Error -> {
                    Text(
                        text = "De data kon niet geladen worden.",
                        textAlign = TextAlign.Center,
                        modifier = Modifier.fillMaxWidth(),
                    )
                }
                TeamApiState.Loading -> {
                    Text(
                        text = "De data wordt geladen...",
                        textAlign = TextAlign.Center,
                        modifier = Modifier.fillMaxWidth(),
                    )
                }
                TeamApiState.Success -> {
                    Row(
                        modifier = Modifier
                            .padding(8.dp),
                    ) {
                        Spacer(modifier = Modifier.height(8.dp))
                        Text(
                            text = bioState.info ?: "",
                            textAlign = TextAlign.Justify,
                            modifier = Modifier.fillMaxWidth(),
                        )
                    }
                }
            }
        }
    }
}
