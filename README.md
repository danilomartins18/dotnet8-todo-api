# ToDo App com Kubernetes, MongoDB, e .NET Services

Este projeto consiste em uma API ToDo construída com .NET 8, usando Kubernetes para orquestração de contêineres e MongoDB para armazenamento de dados.

## Tecnologias Utilizadas

- [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/) para construção da API de To Do
- [Docker](https://www.docker.com/) para conteinerização dos serviços
- [Kubernetes](https://kubernetes.io/pt-br/docs/home/) para orquestração dos contêineres
- [MongoDB](https://www.mongodb.com/) para armazenar as tarefas registradas
- [K6](https://grafana.com/docs/k6/latest/) para testes de carga

**Em fase inicial:**

- [OpenTelemetry](https://opentelemetry.io/docs/) para observabilidade coletando métricas, logs e traces.
- [Datadog](https://opentelemetry.io/docs/) para armazenamento e gerenciamento das métricas, logs e traces coletados pelo OpenTelemetry.

## Conceitos Utilizados

- Clean Code
- Clean Architecture
- Observabilidade

**Futuramente:**

- Testes unitários com xUnit
- Testes de integração com xUnit & [Testcontainers](https://dotnet.testcontainers.org/)

## Pré-requisitos

- [Docker Desktop com Kubernetes](https://www.docker.com/) instalado
- [Kubectl](https://kubernetes.io/docs/tasks/tools/) instalado
- [MongoDB Compass](https://www.mongodb.com/try/download/shell) ou cliente MongoDB para acesso ao banco de dados
- [K6](https://grafana.com/docs/k6/latest/) para testes de carga

**Opcionais:**

- [K9s](https://k9scli.io/) para geerenciar clusters kubernetes

## Estrutura do Projeto

- `ToDo.API`: Serviço .NET Core para operações CRUD de tarefas.
- `mongo-deployment.yaml`: Arquivo YAML para o deployment do MongoDB no Kubernetes.
- Outros arquivos de configuração Kubernetes.

## Configuração

1. Clone este repositório.
2. Use o comando docker dentro da pasta **src** para fazer o build da imagem Docker com o nome **todo-api** (`docker build -t todo-api .`)
3. Use o comando a seguir para aplicar todos os serviços para publicação no cluster Kubernetes local: `powershell .\apply.ps1`.
4. Faça o comando do kubectl port-forward para mapear a porta 8080 da sua máquina com a porta 8080 do pod da aplicação
5. Faça o comando do kubectl port-forward para mapear a porta 27017 da sua máquina com a porta 27017 do pod do MongoDB

## Uso

- Use a url do Swagger para acessar a documentação da API: `http://localhost:8080/swagger`.
- Conecte-se ao banco de dados usando a string de conexão `mongodb://localhost:27017/toDo`.
