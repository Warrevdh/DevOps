package com.example.oogartsapp.data

import android.content.Context
import androidx.room.Room
import com.example.oogartsapp.data.database.article.ArticleDao
import com.example.oogartsapp.data.database.article.ArticleDatabase
import com.example.oogartsapp.data.database.bio.BioDao
import com.example.oogartsapp.data.database.bio.BioDatabase
import com.example.oogartsapp.data.database.employee.EmployeeDao
import com.example.oogartsapp.data.database.employee.EmployeeDatabase
import com.example.oogartsapp.data.database.eyecondition.EyeconditionDao
import com.example.oogartsapp.data.database.eyecondition.EyeconditionDatabase
import com.example.oogartsapp.data.database.group.GroupDao
import com.example.oogartsapp.data.database.group.GroupDatabase
import com.example.oogartsapp.network.service.ArticleApiService
import com.example.oogartsapp.network.service.BioApiService
import com.example.oogartsapp.network.service.EmployeeApiService
import com.example.oogartsapp.network.service.EyeconditionApiService
import com.example.oogartsapp.network.service.GroupApiService
import com.jakewharton.retrofit2.converter.kotlinx.serialization.asConverterFactory
import kotlinx.serialization.json.Json
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import java.util.concurrent.TimeUnit

/**
 * Interface representing the dependencies required by the application.
 */
interface AppContainer {
    val articleRepository: ArticleRepository
    val teamRepository: TeamRepository
    val groupRepository: GroupRepository
    val bioRepository: BioRepository
    val eyeconditionRepository: EyeconditionRepository
}

/**
 * Default implementation of the [AppContainer] providing instances of repositories
 * and services required by the application.
 *
 * @property applicationContext The application context.
 */
class DefaultAppContainer(
    private val applicationContext: Context,
) : AppContainer {

    private val baseUrl = "https://devops-g5.lamdev.be/api/"
//    private val baseUrl = "https://10.0.2.2:5001/api/"

    private val okHttpClient: OkHttpClient by lazy {
        OkHttpClient.Builder()
            .connectTimeout(30, TimeUnit.SECONDS)
            .readTimeout(30, TimeUnit.SECONDS)
            .build()
    }

    private val retrofit = Retrofit.Builder()
        .addConverterFactory(
            Json.asConverterFactory("application/json".toMediaType()),
        )
        .baseUrl(baseUrl)
        .client(okHttpClient)
        .build()

    private val retrofitArticleService: ArticleApiService by lazy {
        retrofit.create(ArticleApiService::class.java)
    }

    private val retrofitTeamService: EmployeeApiService by lazy {
        retrofit.create(EmployeeApiService::class.java)
    }

    private val retrofitGroupService: GroupApiService by lazy {
        retrofit.create(GroupApiService::class.java)
    }

    private val retrofitBioService: BioApiService by lazy {
        retrofit.create(BioApiService::class.java)
    }

    private val retrofitEyeconditionService: EyeconditionApiService by lazy {
        retrofit.create(EyeconditionApiService::class.java)
    }

    private val articleDb: ArticleDatabase by lazy {
        Room.databaseBuilder(applicationContext, ArticleDatabase::class.java, "article_database")
            .build()
    }

    private val employeeDb: EmployeeDatabase by lazy {
        Room.databaseBuilder(applicationContext, EmployeeDatabase::class.java, "employee_database")
            .build()
    }

    private val groupDb: GroupDatabase by lazy {
        Room.databaseBuilder(applicationContext, GroupDatabase::class.java, "group_database")
            .build()
    }

    private val bioDb: BioDatabase by lazy {
        Room.databaseBuilder(applicationContext, BioDatabase::class.java, "bio_database")
            .build()
    }

    private val eyeconditionDb: EyeconditionDatabase by lazy {
        Room.databaseBuilder(applicationContext, EyeconditionDatabase::class.java, "eyecondition_database")
            .build()
    }

    private val articleDao: ArticleDao by lazy {
        articleDb.articleDao()
    }

    private val employeeDao: EmployeeDao by lazy {
        employeeDb.employeeDao()
    }

    private val groupDao: GroupDao by lazy {
        groupDb.groupDao()
    }

    private val bioDao: BioDao by lazy {
        bioDb.bioDao()
    }

    private val eyeconditionDao: EyeconditionDao by lazy {
        eyeconditionDb.eyeconditionDao()
    }

    override val articleRepository: ArticleRepository by lazy {
        CachingArticleRepository(articleDao = articleDao, articleApiService = retrofitArticleService)
    }

    override val teamRepository: TeamRepository by lazy {
        CachingTeamRepository(employeeDao = employeeDao, employeeApiService = retrofitTeamService)
    }

    override val groupRepository: GroupRepository by lazy {
        CachingGroupRepository(groupDao = groupDao, groupApiService = retrofitGroupService)
    }

    override val bioRepository: BioRepository by lazy {
        CachingBioRepository(bioDao = bioDao, bioApiService = retrofitBioService)
    }

    override val eyeconditionRepository: EyeconditionRepository by lazy {
        CachingEyeconditionRepository(eyeconditionDao = eyeconditionDao, eyeconditionApiService = retrofitEyeconditionService)
    }
}
