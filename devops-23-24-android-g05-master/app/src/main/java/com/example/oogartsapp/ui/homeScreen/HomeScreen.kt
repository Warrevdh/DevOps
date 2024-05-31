package com.example.oogartsapp.ui.homeScreen

import androidx.compose.foundation.Image
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.Divider
import androidx.compose.material3.ElevatedCard
import androidx.compose.material3.MaterialTheme
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
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.lifecycle.viewmodel.compose.viewModel
import coil.compose.rememberImagePainter
import coil.transform.CircleCropTransformation
import com.example.oogartsapp.R
import com.example.oogartsapp.model.Article
import com.example.oogartsapp.ui.components.Card

/**
 * Previews the HomeScreen composable.
 */
@Preview(showBackground = true, widthDp = 300, heightDp = 600)
@Composable
fun BlogPreview() {
    HomeScreen(innerPadding = PaddingValues(0.dp))
}

/**
 * HomeScreen Composable function representing the home screen of the app.
 *
 * @param innerPadding Padding values.
 */
@Composable
fun HomeScreen(
    innerPadding: PaddingValues,
) {
    var selectedArticle by remember { mutableStateOf<Article?>(null) }

    if (selectedArticle != null) {
        DetailScreen(
            innerPadding = innerPadding,
            article = selectedArticle!!,
            onBack = { selectedArticle = null },
        )
    } else {
        BlogScreen(innerPadding = innerPadding) {
            selectedArticle = it
        }
    }
}

/**
 * BlogScreen Composable displaying a list of articles.
 *
 * @param homeScreenViewModel ViewModel managing blog data and API states.
 * @param innerPadding Padding values.
 * @param onArticleClick Callback function invoked when an [Article] is clicked.
 */
@Composable
fun BlogScreen(
    homeScreenViewModel: HomeScreenViewModel = viewModel(factory = HomeScreenViewModel.Factory),
    innerPadding: PaddingValues,
    onArticleClick: (Article) -> Unit,
) {
    val articleListState by homeScreenViewModel.uiListState.collectAsState()
    val homeApiState = homeScreenViewModel.homeApiState

    Column(
        modifier = Modifier.padding(innerPadding),
    ) {
        Spacer(modifier = Modifier.height(30.dp))
        Welcome(modifier = Modifier)
        Spacer(modifier = Modifier.height(30.dp))
        when (homeApiState) {
            is HomeApiState.Error -> {
                Row(
                    Modifier.fillMaxWidth(),
                    horizontalArrangement = Arrangement.Center,
                ) {
                    Text(text = "De data kon niet geladen worden.")
                }
            }
            is HomeApiState.Loading -> {
                Row(
                    Modifier.fillMaxWidth(),
                    horizontalArrangement = Arrangement.Center,
                ) {
                    Text(text = "Laden...")
                }
            }
            is HomeApiState.Success -> {
                BlogColumn(modifier = Modifier, articleList = articleListState, onArticleClick = onArticleClick)
            }
        }
    }
}

/**
 * Welcome Composable rendering the welcome message.
 *
 * @param modifier Modifier to be applied to the layout.
 */
@Composable
fun Welcome(modifier: Modifier) {
    Column(
        modifier = modifier
            .fillMaxWidth()
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
    ) {
        Image(
            painter = painterResource(id = R.drawable.logo),
            contentDescription = null,
            modifier = Modifier
                .size(65.dp),
            contentScale = ContentScale.Crop,
        )
        Text(
            text = "Welkom bij\nOogcentrum Vision",
            fontSize = 25.sp,
            fontWeight = FontWeight.Bold,
            modifier = Modifier.padding(8.dp),
            textAlign = TextAlign.Center,
        )
    }
}

/**
 * BlogColumn Composable displaying a column of articles.
 *
 * @param modifier Modifier to be applied to the layout.
 * @param articleList [List] of consisting of [Article] objects to display.
 * @param onArticleClick Callback function invoked when an article is clicked.
 */
@Composable
fun BlogColumn(modifier: Modifier, articleList: List<Article>, onArticleClick: (Article) -> Unit) {
    Column(
        modifier = modifier
            .fillMaxWidth()
            .padding(16.dp),
    ) {
        Text(
            text = "Nieuws",
            style = MaterialTheme.typography.headlineMedium,
            fontWeight = FontWeight.Bold,
            modifier = Modifier.padding(8.dp),
        )
        Spacer(modifier = Modifier.height(6.dp))
        Divider(color = Color.LightGray, thickness = 1.dp)
        Spacer(modifier = Modifier.height(25.dp))
        LazyColumn {
            items(articleList) { article ->
                Card(image = article.imageUrl, imageDescription = article.title, title = article.title, description = article.description, onClick = { onArticleClick(article) })
                Spacer(modifier = Modifier.height(20.dp))
            }
        }
    }
}

/**
 * BlogCard Composable rendering a card representing an article.
 *
 * @param article [Article] object to display.
 * @param modifier Modifier to be applied to the layout.
 * @param onArticleClick Callback function invoked when the card is clicked.
 */

@Composable
fun BlogCard(article: Article, modifier: Modifier, onArticleClick: (Article) -> Unit) {
    ElevatedCard(
        modifier = modifier
            .fillMaxWidth()
            .height(85.dp)
            .clickable { onArticleClick(article) },
        elevation = CardDefaults.cardElevation(
            defaultElevation = 6.dp,
        ),
        colors = CardDefaults.cardColors(
            containerColor = Color(0xFFE2F8F8),
        ),
    ) {
        Box(
            modifier = Modifier
                .padding(8.dp)
                .align(Alignment.CenterHorizontally),
        ) {
            Row(verticalAlignment = Alignment.Top) {
                val painter: Painter = rememberImagePainter(
                    data = article.imageUrl,
                    builder = {
                        transformations(CircleCropTransformation())
                    },
                )

                Image(
                    painter = painter,
                    contentDescription = null,
                    modifier = Modifier
                        .size(75.dp)
                        .clip(MaterialTheme.shapes.medium),
                    contentScale = ContentScale.Crop,
                )
                Spacer(modifier = Modifier.width(8.dp))
                Text(
                    text = article.title,
                    fontWeight = FontWeight.SemiBold,
                    modifier = Modifier
                        .padding(4.dp),
                )
            }
        }
    }
}
