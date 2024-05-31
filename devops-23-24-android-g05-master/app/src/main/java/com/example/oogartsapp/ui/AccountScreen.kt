package com.example.oogartsapp.ui

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.State
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.auth0.android.result.UserProfile

/**
 * AccountScreen Composable function responsible for displaying user account details.
 *
 * @param innerPadding Padding values.
 * @param cachedUserProfile State holding the user profile information.
 * @param doesUsernameExist Function to check if the username exists in the database.
 */
@Composable
fun AccountScreen(innerPadding: PaddingValues, cachedUserProfile: State<UserProfile?>, doesUsernameExist: suspend (String) -> Boolean) {
    val scrollState = rememberScrollState()
    Box(
        modifier = Modifier
            .fillMaxSize()
            .padding(innerPadding)
            .verticalScroll(scrollState)
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(16.dp)
        ) {
            Row(
                modifier = Modifier
                    .padding(8.dp),
                verticalAlignment = Alignment.Top
            ) {
                Column {
                    Spacer(modifier = Modifier.height(8.dp))
                    Text(
                        text = "Username: ${cachedUserProfile.value?.name ?: ""}\n" + "Email: ${cachedUserProfile.value?.email ?: ""}",
                        textAlign = TextAlign.Center,
                        fontSize = 20.sp,
                        fontWeight = FontWeight.Bold,
                        modifier = Modifier.fillMaxWidth()
                            .padding(5.dp)
                    )
                }
            }
            var doesUsernameExistState by remember { mutableStateOf(false) }

            LaunchedEffect(Unit) {
                //doesUsernameExistState = doesUsernameExist("markvandoren@gmail.com" ?: "")
                doesUsernameExistState = doesUsernameExist(cachedUserProfile.value?.email ?: "")
            }

            Row(
                modifier = Modifier
                    .padding(8.dp),
            ) {
                Spacer(modifier = Modifier.height(8.dp))
                Text(
                    text = if (doesUsernameExistState) {
                        "Deze user zit in de database"
                    } else {
                        "Log in als u info over uw user wilt zien"
                    },
                    textAlign = TextAlign.Center,
                    modifier = Modifier.fillMaxWidth()
                )
            }
        }
    }
}