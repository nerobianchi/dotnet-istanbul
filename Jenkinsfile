pipeline {
  agent any
  stages {
    stage('build') {
      steps {
        sh 'dotnet restore'
        sh 'dotnet build'
      }
    }
    stage('test') {
      steps {
        sh 'dotnet test'
      }
    }
    stage('publish') {
      steps {
        sh 'dotnet publish'
      }
    }
  }
}