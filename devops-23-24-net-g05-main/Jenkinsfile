pipeline {
    agent {
        docker {
            // Use the .NET SDK 6.0 image
            image 'mcr.microsoft.com/dotnet/sdk:6.0'
        }
    }
    
    stages {
        stage('Checkout') {
            steps {
                // Checkout the source code from the repository
                checkout scm
            }
        }

        stage('Restore and Build') {
            steps {
                // Restore NuGet packages and build the application
                script {
                    sh 'dotnet restore'
                    sh 'dotnet build -c Release'
                }
            }
        }

        stage('Run Tests') {
            steps {
                // Run tests if your project has them
                script {
                    sh 'dotnet test'
                }
            }
        }

        stage('Publish') {
            steps {
                // Publish the application
                script {
                    sh 'dotnet publish -c Release -o publish'
                }
            }
        }
    }

    post {
        success {
            // This block is executed if the pipeline is successful
            echo 'Pipeline succeeded! Additional tasks can be added here.'
        }
        failure {
            // This block is executed if the pipeline fails
            echo 'Pipeline failed! Take necessary actions.'
        }
    }
}
