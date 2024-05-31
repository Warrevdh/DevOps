package com.example.oogartsapp.ui.teamScreen

import androidx.compose.foundation.Image
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material3.Divider
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.painter.Painter
import androidx.compose.ui.layout.ContentScale
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.viewmodel.compose.viewModel
import coil.compose.rememberImagePainter
import coil.transform.CircleCropTransformation
import com.example.oogartsapp.model.Employee

/**
 * GroupCard Composable function that creates a card for displaying a group title.
 *
 * @param group The title of the group to be displayed in the card.
 */
@Composable
fun GroupCard(group: String) {
    Column {
        Text(
            text = group,
            modifier = Modifier
                .padding(start = 16.dp),
            style = TextStyle(
                fontWeight = FontWeight.Bold,
                fontStyle = FontStyle.Italic,
                fontSize = 16.sp,
            ),
        )
        Spacer(modifier = Modifier.padding(start = 16.dp))
        Divider(color = Color.LightGray, thickness = 1.dp)
        Spacer(modifier = Modifier.padding(end = 16.dp))
    }
}

/**
 * TeamMemberCard Composable rendering a card representing an employee.
 *
 * @param employee [Employee] object to display.
 * @param onMemberClick Callback function triggered when the team member card is clicked. Receives an employee object.
 */
@Composable
fun TeamMemberCard(employee: Employee, onMemberClick: (employee: Employee) -> Unit) {
    val nameDisplay = employee.firstName + " " + employee.lastName

    Box(
        modifier = Modifier
            .fillMaxSize()
            .padding(8.dp),
        /*        .background(MaterialTheme.colorScheme.surfaceVariant)*/
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(8.dp)
                .clickable { onMemberClick(employee) },
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center,
        ) {
            val painter: Painter = rememberImagePainter(
                data = employee.image,
                builder = {
                    transformations(CircleCropTransformation())
                },
            )

            Image(
                painter = painter,
                contentDescription = null,
                modifier = Modifier
                    .size(100.dp)
                    .clip(CircleShape),
                contentScale = ContentScale.Crop,
            )
            Spacer(modifier = Modifier.height(8.dp))
            Text(
                text = nameDisplay,
                fontWeight = FontWeight.Bold,
                color = Color.Black,
                textAlign = TextAlign.Center,
                modifier = Modifier
                    .fillMaxWidth(),
                style = TextStyle(
                    fontFamily = FontFamily.Default,
                    lineHeight = 12.sp,
                ),
            )
        }
    }
}

/**
 * TeamMemberRow Composable function displaying a row of team member cards horizontally.
 *
 * @param employeeList [List] of [Employee] objects representing employees.
 * @param onMemberClick Callback function triggered when a team member card is clicked. Receives an employee object.
 */
@Composable
fun TeamMemberRow(employeeList: List<Employee>, onMemberClick: (employee: Employee) -> Unit) {
    LazyRow {
        items(employeeList) { employee ->
            TeamMemberCard(
                employee = employee,
                onMemberClick = onMemberClick,
            )
        }
    }
}

/**
 * HomeComp Composable function displaying a home component with groups and employees.
 *
 * @param innerPadding Padding values.
 * @param teamScreenViewModel ViewModel managing team-related data and API states.
 * @param onMemberClick Callback function triggered when a team member card is clicked. Receives an employee object.
 */
@Composable
fun HomeComp(
    innerPadding: PaddingValues,
    teamScreenViewModel: TeamScreenViewModel = viewModel(factory = TeamScreenViewModel.Factory),
    onMemberClick: (employee: Employee) -> Unit,
) {
    val employeeListState by teamScreenViewModel.employeeListState.collectAsState()
    val groupListState by teamScreenViewModel.groupListState.collectAsState()
    val teamApiState = teamScreenViewModel.teamApiState

    Box(
        modifier = Modifier
            .fillMaxSize()
            .fillMaxHeight()
            .padding(innerPadding),
    ) {
        when (teamApiState) {
            TeamApiState.Error -> { }
            TeamApiState.Loading -> { }
            TeamApiState.Success -> {
                LazyColumn(
                    modifier = Modifier
                        // .fillMaxSize()
                        .align(Alignment.TopStart)
                        .padding(12.dp),
                    verticalArrangement = Arrangement.spacedBy(15.dp),
                ) {
                    items(groupListState) { group ->
                        GroupCard(
                            group = group.name,
                        )
                        TeamMemberRow(
                            employeeList = employeeListState.filter { it.group.id == group.id },
                            onMemberClick = onMemberClick,
                        )
                    }
                }
            }
        }
    }
}

/**
 * TeamScreen Composable function for displaying employees
 *
 * @param innerPadding Padding values.
 */
@Composable
fun TeamScreen(innerPadding: PaddingValues) {
    var selectedMember by remember { mutableStateOf<Employee?>(null) }

    if (selectedMember != null) {
        DetailComp(innerPadding = innerPadding, selectedMember!!, onBack = { selectedMember = null })
    } else {
        HomeComp(innerPadding = innerPadding) { selectedMember = it }
    }
}
