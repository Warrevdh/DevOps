package com.example.oogartsapp.ui

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.ElevatedCard
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp

/**
 * AppointmentScreen Composable function responsible for displaying future appointments.
 *
 * @param innerPadding Padding values.
 * @param function Callback function triggered on certain actions.
 */
@Composable
fun AppointmentScreen(innerPadding: PaddingValues, function: () -> Unit) {
    val futureAppointments = listOf(
        Appointment("Dr. Smith", "2023-12-01","10:00 AM", "Controle"),
        Appointment("Dr. Johnson", "2023-12-05", "12:30 PM", "Controle"),
        Appointment("Dr. Williams", "2023-12-10", "09:00 AM", "Pijn in rechteroog"),
        Appointment("Dr. Davis", "2023-12-10", "09:00 AM", "Wazig zicht in linker oog"),
        Appointment("Dr. Green", "2023-12-10", "09:00 AM", "Controle"),
        Appointment("Dr. Miller", "2023-12-10", "09:00 AM", "Controle"),
    )

    Box(modifier = Modifier
        .fillMaxSize()
        .padding(innerPadding)) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(16.dp),
            verticalArrangement = Arrangement.SpaceAround,
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            Text(text = "Toekomstige afspraken")
            Spacer(modifier = Modifier.height(8.dp))
            LazyColumn(
                modifier = Modifier
                    .weight(1f)
            ) {
                items(futureAppointments) { appointment ->
                    AppointmentCard(appointment = appointment)
                    Spacer(modifier = Modifier.height(8.dp))
                }
            }
        }
    }
}

/**
 * AppointmentCard Composable function for displaying appointments in cards.
 *
 * @param appointment Appointment object.
 */
@Composable
fun AppointmentCard(appointment: Appointment) {
    ElevatedCard(
        modifier = Modifier
            .fillMaxWidth(),
        elevation = CardDefaults.cardElevation(
            defaultElevation = 6.dp
        ),
        colors = CardDefaults.cardColors(
            containerColor = Color(0xFFE2F8F8),
        )
    ) {
        Column(
            modifier = Modifier
                .padding(16.dp)
        ) {
            Text(text = appointment.doctor, fontWeight = FontWeight.Bold)
            Spacer(modifier = Modifier.height(8.dp))
            Text(text = "Date: ${appointment.date}")
            Spacer(modifier = Modifier.height(8.dp))
            Text(text ="Time: ${appointment.time}")
            Spacer(modifier = Modifier.height(8.dp))
            Text(text = "Reason: ${appointment.reason}")
        }
    }
}

// Data class to represent an appointment
data class Appointment(val doctor: String, val date: String, val time: String, val reason: String)