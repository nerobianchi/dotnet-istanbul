pipeline {
  agent any
  stages {
    stage('info') {
      steps {
        sh 'pwd'
      }
    }
    stage('build') {
      steps {
        sh '''docker run --rm -v /var/run/docker.sock:/var/run/docker.sock -v ~/dotnet-docker-samples:/repo -w /repo --name myapp-0001 microsoft/dotnet-buildtools-prereqs:image-builder-jessie-20171020090934 build --manifest manifest.json
'''
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