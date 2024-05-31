package com.example.oogartsapp.ui.homeScreen

import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.ViewModelProvider.AndroidViewModelFactory.Companion.APPLICATION_KEY
import androidx.lifecycle.viewModelScope
import androidx.lifecycle.viewmodel.initializer
import androidx.lifecycle.viewmodel.viewModelFactory
import com.example.oogartsapp.Application
import com.example.oogartsapp.data.ArticleRepository
import com.example.oogartsapp.model.Article
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import java.io.IOException

/**
 * ViewModel class responsible for data related to the homescreen and API states.
 * Manages API states and provides data streams for articles.
 *
 * @param articleRepository Repository handling [Article] data operations.
 */
class HomeScreenViewModel(
    private val articleRepository: ArticleRepository,
) : ViewModel() {
    /**
     * State representing the API request status for the homepage.
     */
    var homeApiState: HomeApiState by mutableStateOf(HomeApiState.Loading)
        private set

    /**
     * StateFlow representing the [List] of [Article] available.
     */
    lateinit var uiListState: StateFlow<List<Article>>

    /**
     * StateFlow representing a single [Article] for detailed view.
     */
    lateinit var articleState: StateFlow<Article>

    /**
     * Initialization block to fetch initial data of the HomeScreen.
     * Fetch data of articles
     */
    init {
        getRepoArticles()
        getArticle(1)
    }

    /**
     * Function to fetch an [Article] by its ID.
     *
     * @param id Id of an [Article].
     */
    fun getArticle(id: Int) {
        try {
            homeApiState = HomeApiState.Loading
            viewModelScope.launch {
                articleRepository.insertArticle(id)
                articleState = articleRepository.getArticle(id).stateIn(
                    scope = viewModelScope,
                    started = SharingStarted.WhileSubscribed(5_000L),
                    initialValue = Article(
                        id = 0,
                        title = "",
                        imageUrl = "",
                        description = "",
                        content = "",
                        author = "",
                        created = "",
                    ),
                )
                homeApiState = HomeApiState.Success
            }
        } catch (e: IOException) {
            homeApiState = HomeApiState.Error
        }
    }

    /**
     * Function to fetch a [List] of [Article].
     */
    private fun getRepoArticles() {
        try {
            viewModelScope.launch { articleRepository.refresh() }
            uiListState = articleRepository.getArticles().stateIn(
                scope = viewModelScope,
                started = SharingStarted.WhileSubscribed(5_000L),
                initialValue = listOf(),
            )

            homeApiState = HomeApiState.Success
        } catch (e: IOException) {
            homeApiState = HomeApiState.Error
        }
    }

    /**
     * Companion object providing a Factory to create instances of [HomeScreenViewModel].
     */
    companion object {
        val Factory: ViewModelProvider.Factory = viewModelFactory {
            initializer {
                val application = this[APPLICATION_KEY] as Application
                val articlesRepository = application.container.articleRepository
                HomeScreenViewModel(articlesRepository)
            }
        }
    }
}
