package com.example.oogartsapp.data

import android.util.Log
import com.example.oogartsapp.data.database.employee.EmployeeDao
import com.example.oogartsapp.data.database.employee.asDbEmployee
import com.example.oogartsapp.data.database.employee.asDomainObjects
import com.example.oogartsapp.model.Employee
import com.example.oogartsapp.network.asDomainObjects
import com.example.oogartsapp.network.service.EmployeeApiService
import com.example.oogartsapp.network.service.getTeamAsFlow
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map
import java.net.SocketTimeoutException

/**
 * Interface defining operations to interact with the team repository.
 */
interface TeamRepository {
    /**
     * Inserts an [Employee] into the repository.
     *
     * @param item The [Employee] to be inserted.
     */
    suspend fun insert(item: Employee)

    /**
     * Retrieves a [Flow] of lists of [Employee]s from the repository.
     *
     * @return A [Flow] emitting lists of [Employee]s.
     */
    fun getTeam(): Flow<List<Employee>>

    /**
     * Refreshes the repository data by fetching the team data.
     */
    suspend fun refresh()
}

/**
 * Implementation of the [TeamRepository] interface that caches team data.
 *
 * @property employeeDao The DAO for accessing [Employee] data in the local database.
 * @property employeeApiService The API service for fetching [Employee] data from the remote source.
 */
class CachingTeamRepository(
    private val employeeDao: EmployeeDao,
    private val employeeApiService: EmployeeApiService,
) : TeamRepository {
    /**
     * Inserts an [Employee] into the local database.
     *
     * @param item The [Employee] to be inserted.
     */
    override suspend fun insert(item: Employee) {
        employeeDao.insert(item.asDbEmployee())
    }

    /**
     * Retrieves a [Flow] of lists of [Employee]s from the local database.
     *
     * @return A [Flow] emitting lists of [Employee]s.
     */
    override fun getTeam(): Flow<List<Employee>> {
        return employeeDao.getTeam().map {
            it.asDomainObjects()
        }
    }

    /**
     * Refreshes the local database by fetching team data from the remote API source.
     * In case of a SocketTimeoutException, logs an error message indicating API call timeout.
     */
    override suspend fun refresh() {
        try {
            employeeApiService.getTeamAsFlow().collect {
                for (employee in it.asDomainObjects()) {
                    Log.i("TeamRepository", "refresh: $employee")
                    insert(employee)
                }
            }
        } catch (e: SocketTimeoutException) {
            Log.e("TeamRepository", "refresh: API call timed out")
        }
    }
}
