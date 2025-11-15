# 🚀 UpSkilling Platform - API RESTful

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Latest-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Entity Framework](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge)

**Plataforma de Upskilling/Reskilling para o Futuro do Trabalho 2030+**

[Sobre](#-sobre-o-projeto) • 
[Tecnologias](#-tecnologias-utilizadas) • 
[Arquitetura](#-arquitetura-de-software) • 
[Como Rodar](#-como-rodar-o-projeto) • 
[API](#-endpoints-da-api) • 
[Equipe](#-equipe)

</div>

---

## 📋 Sobre o Projeto

O **UpSkilling Platform** é uma API RESTful desenvolvida para gerenciar uma plataforma de qualificação e requalificação profissional. O sistema permite:

- 👤 **Gestão de Usuários**: Cadastro completo com perfil profissional
- 📚 **Trilhas de Aprendizagem**: Cursos estruturados por nível (Iniciante, Intermediário, Avançado)
- 🎯 **Competências**: Mapeamento de habilidades técnicas, humanas e de gestão
- 📝 **Matrículas**: Inscrição e gestão de alunos em trilhas de desenvolvimento

### 🎯 Funcionalidades Principais

- ✅ **CRUD completo** de Usuários, Trilhas e Matrículas
- ✅ **Sistema de inscrição** com validações de regras de negócio
- ✅ **Controle de status** de matrículas (Ativa, Concluída, Cancelada)
- ✅ **Consultas específicas** por usuário e por trilha
- ✅ **Arquitetura Clean** com separação de responsabilidades
- ✅ **Testes unitários** com 100% de cobertura das regras de negócio
- ✅ **Documentação Swagger** interativa
- ✅ **Containerização Docker** para fácil deploy

---

## 🛠️ Tecnologias Utilizadas

### **Backend & Framework**
- **[.NET 8.0](https://dotnet.microsoft.com/)** - Framework multiplataforma de alta performance
- **[ASP.NET Core Web API](https://docs.microsoft.com/aspnet/core/)** - Framework para criação de APIs RESTful
- **[C# 12.0](https://docs.microsoft.com/dotnet/csharp/)** - Linguagem de programação moderna e type-safe

### **Banco de Dados & ORM**
- **[MySQL 8.0](https://www.mysql.com/)** - Sistema de gerenciamento de banco de dados relacional
- **[Entity Framework Core 8.0](https://docs.microsoft.com/ef/core/)** - ORM (Object-Relational Mapper)
- **[Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)** - Provider MySQL para EF Core

### **Containerização**
- **[Docker](https://www.docker.com/)** - Plataforma de containerização
- **[Docker Compose](https://docs.docker.com/compose/)** - Orquestração de múltiplos containers

### **Documentação & Testes**
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentação interativa da API
- **[Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Geração automática de documentação Swagger
- **[MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest)** - Framework de testes unitários
- **[Moq](https://github.com/moq/moq4)** - Biblioteca de mocking para testes

### **Padrões e Práticas**
- **Clean Architecture** - Arquitetura em camadas com separação de responsabilidades
- **Domain-Driven Design (DDD)** - Modelagem focada no domínio do negócio
- **Repository Pattern** - Abstração do acesso a dados
- **Dependency Injection** - Injeção de dependências nativa do .NET
- **Fluent API** - Configuração declarativa do EF Core

---

## 🏗️ Arquitetura de Software

O projeto segue os princípios de **Clean Architecture** (Arquitetura Limpa) proposta por Robert C. Martin, organizando o código em camadas concêntricas onde cada camada tem responsabilidades bem definidas e dependências unidirecionais.

### 📊 Diagrama de Camadas

```
┌──────────────────────────────────────────────────────────┐
│                    API (Presentation)                     │
│  Controllers • Middleware • Request/Response Handling     │
└───────────────────────┬──────────────────────────────────┘
                        │ depends on
┌───────────────────────▼──────────────────────────────────┐
│                  Application (Use Cases)                  │
│     Services • DTOs • Business Logic • Validations        │
└───────────────────────┬──────────────────────────────────┘
                        │ depends on
┌───────────────────────▼──────────────────────────────────┐
│                    Domain (Core)                          │
│  Entities • Interfaces • Domain Logic • Exceptions        │
└───────────────────────▲──────────────────────────────────┘
                        │ implements
┌───────────────────────┴──────────────────────────────────┐
│              Infrastructure (External)                    │
│  Repositories • DbContext • Data Access • Configurations  │
└──────────────────────────────────────────────────────────┘
```

### 🎯 Descrição das Camadas

#### **1. API Layer (Camada de Apresentação)**
📁 `UpSkillingPlatform/API/`

**Responsabilidades:**
- Receber requisições HTTP
- Validar entrada de dados
- Invocar serviços da camada de aplicação
- Retornar respostas HTTP formatadas
- Tratamento de exceções global

**Componentes:**
- `Controllers/` - Endpoints REST (UsuariosController, TrilhasController)
- `Middleware/` - GlobalExceptionHandlerMiddleware

**Tecnologias:**
- ASP.NET Core Web API
- Model Binding & Validation
- Status Codes (200, 201, 400, 404, 409, 422, 500)

---

#### **2. Application Layer (Camada de Aplicação)**
📁 `UpSkillingPlatform/Application/`

**Responsabilidades:**
- Implementar casos de uso (use cases)
- Orquestrar operações entre repositórios
- Aplicar regras de negócio
- Transformar dados entre DTOs e Entidades
- Validações de negócio

**Componentes:**
- `Services/` - Lógica de aplicação (UsuarioService, TrilhaService)
- `DTOs/` - Data Transfer Objects (CreateDto, UpdateDto, ResponseDto)

**Padrões Aplicados:**
- Service Layer Pattern
- DTO Pattern
- Mapper Pattern (manual)

---

#### **3. Domain Layer (Camada de Domínio)**
📁 `UpSkillingPlatform/Domain/`

**Responsabilidades:**
- Modelar o negócio (Entities)
- Definir contratos (Interfaces)
- Regras de domínio
- Exceções de negócio

**Componentes:**
- `Entities/` - Entidades do domínio (Usuario, Trilha, Competencia, etc.)
- `Interfaces/` - Contratos de repositórios
- `Exceptions/` - Exceções customizadas

**Características:**
- ❌ **SEM dependências externas** (mais interna)
- ✅ POCOs (Plain Old CLR Objects)
- ✅ Entidades ricas com comportamento

---

#### **4. Infrastructure Layer (Camada de Infraestrutura)**
📁 `UpSkillingPlatform/Infrastructure/`

**Responsabilidades:**
- Acesso a dados (persistência)
- Implementar interfaces do Domain
- Configurar Entity Framework
- Gerenciar transações e migrations

**Componentes:**
- `Repositories/` - Implementações concretas dos repositórios
- `Data/AppDbContext.cs` - Contexto do Entity Framework
- `Data/Configurations/` - Configurações Fluent API

**Padrões Aplicados:**
- Repository Pattern
- Unit of Work (implícito no DbContext)
- Fluent API Configuration

---

### 🔄 Fluxo de Dados

```
┌─────────────┐
│   Client    │
│  (HTTP)     │
└──────┬──────┘
       │
       ▼
┌─────────────────────┐
│    Controller       │  ◄─── API Layer
│  - Recebe Request   │
│  - Valida Entrada   │
└──────┬──────────────┘
       │
       ▼
┌─────────────────────┐
│     Service         │  ◄─── Application Layer
│  - Lógica Negócio   │
│  - Orquestração     │
└──────┬──────────────┘
       │
       ▼
┌─────────────────────┐
│    Repository       │  ◄─── Infrastructure Layer
│  - Acesso a Dados   │
│  - Persistência     │
└──────┬──────────────┘
       │
       ▼
┌─────────────────────┐
│     Database        │
│     (MySQL)         │
└─────────────────────┘
```

### ✅ Benefícios da Arquitetura

1. **Separação de Responsabilidades** - Cada camada tem um propósito único
2. **Testabilidade** - Camadas desacopladas facilitam testes unitários
3. **Manutenibilidade** - Código organizado e fácil de encontrar
4. **Escalabilidade** - Facilita adição de novas funcionalidades
5. **Independência de Framework** - Domain não depende de tecnologias externas
6. **Regra de Dependência** - Camadas externas dependem das internas, nunca o contrário

---

## 🚀 Como Rodar o Projeto

### � **OPÇÃO 1: Com Docker (Recomendado)** ⭐

A forma mais rápida e fácil! Apenas **um comando** e tudo está funcionando.

```bash
# Clone o repositório
git clone <url-do-repositorio>
cd gs

# Suba toda a aplicação (API + MySQL)
docker compose up -d

# Aguarde ~30 segundos e acesse:
# 🌐 Swagger: http://localhost:5000/swagger
# 📊 API: http://localhost:5000/api/
```

**Pronto! A aplicação está rodando! 🎉**

Para mais detalhes sobre Docker, veja: **[📖 README-DOCKER.md](README-DOCKER.md)**

---

### 💻 **OPÇÃO 2: Desenvolvimento Local** 

Para rodar sem Docker ou fazer desenvolvimento ativo:

### �📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- ✅ [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (versão 8.0 ou superior)
- ✅ [Docker Desktop](https://www.docker.com/products/docker-desktop) (para Windows/Mac) ou Docker Engine (Linux)
- ✅ [Git](https://git-scm.com/) (para clonar o repositório)
- ⚙️ (Opcional) [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- ⚙️ (Opcional) [DBeaver](https://dbeaver.io/) ou MySQL Workbench (para visualizar o banco)

### 🔍 Verificar Instalações

```bash
# Verificar .NET SDK
dotnet --version
# Deve retornar: 8.0.x ou superior

# Verificar Docker
docker --version
docker-compose --version

# Verificar Git
git --version
```

---

### 📥 Passo 1: Clonar o Repositório

```bash
# Clone o repositório
git clone <url-do-repositorio>

# Entre na pasta do projeto
cd gs

# A estrutura do projeto está organizada em:
# src/
#   ├── UpSkillingPlatform.API/          # Camada de apresentação (Controllers, Middleware)
#   ├── UpSkillingPlatform.Application/  # Camada de aplicação (Services, DTOs)
#   ├── UpSkillingPlatform.Domain/       # Camada de domínio (Entities, Interfaces, Exceptions)
#   └── UpSkillingPlatform.Infrastructure/ # Camada de infraestrutura (Repositories, DbContext)
# tests/
#   └── UpSkillingPlatform.Tests/        # Testes unitários com MSTest
```

---

### 🐳 Passo 2: Iniciar o Banco de Dados com Docker

O projeto utiliza Docker Compose para subir um container MySQL configurado e pronto para uso.

```bash
# Certifique-se de estar na pasta 'gs'
cd /home/felipe/workspace/fiap/soa/gs

# Inicie o container MySQL
docker-compose up -d
```

**O que acontece:**
- 📦 Download da imagem MySQL 8.0 (primeira vez)
- 🚀 Criação e inicialização do container
- 🗄️ Criação automática do database `upskilling_db`
- 👤 Criação do usuário `upskilling_user`

**Aguarde ~30 segundos** para o MySQL estar completamente pronto.

**Verificar se o container está rodando:**
```bash
docker ps
# Deve aparecer: upskilling_mysql
```

**Credenciais do Banco:**
```
Host: localhost
Port: 3306
Database: upskilling_db
User: upskilling_user
Password: upskilling_pass
```

---

### 📦 Passo 3: Restaurar Dependências do Projeto

```bash
# A solução está organizada em múltiplos projetos
# Restaure todas as dependências de uma vez

cd /home/felipe/workspace/fiap/soa/gs
dotnet restore
```

**Projetos na solução:**
- **UpSkillingPlatform.Domain** - Entidades, Interfaces, Exceções
- **UpSkillingPlatform.Application** - Services, DTOs (depende do Domain)
- **UpSkillingPlatform.Infrastructure** - Repositories, DbContext, EF Core (depende do Domain)
- **UpSkillingPlatform.API** - Controllers, Middleware, Swagger (depende do Application e Infrastructure)
- **UpSkillingPlatform.Tests** - Testes MSTest com Moq (depende do Domain e Application)

**Pacotes que serão instalados:**
- Microsoft.EntityFrameworkCore (8.0.10)
- Microsoft.EntityFrameworkCore.Design (8.0.10)
- Pomelo.EntityFrameworkCore.MySql (8.0.2)
- Swashbuckle.AspNetCore (6.5.0)
- Moq (4.20.70) - Para testes

---

### 🗄️ Passo 4: Criar e Aplicar Migrations

O Entity Framework Core usa migrations para criar e versionar o schema do banco de dados.

```bash
# As migrations devem ser criadas a partir do projeto API (que tem a referência ao Infrastructure)
cd src/UpSkillingPlatform.API

# Criar a migration inicial
dotnet ef migrations add InitialCreate

# Aplicar a migration ao banco de dados
dotnet ef database update
```

**O que acontece:**
- ✅ Criação de todas as tabelas (usuarios, trilhas, competencias, etc.)
- ✅ Definição de chaves primárias e estrangeiras
- ✅ Criação de índices (ex: email único)
- ✅ Inserção de dados iniciais (seed data)

**Seed Data Incluído:**
- 8 Competências pré-cadastradas
- 4 Trilhas de aprendizagem de exemplo
- Relacionamentos entre trilhas e competências

---

### ▶️ Passo 5: Executar a Aplicação

```bash
# Execute a aplicação a partir do projeto API
cd src/UpSkillingPlatform.API
dotnet run

# Ou execute a partir da raiz usando a solução
cd /home/felipe/workspace/fiap/soa/gs
dotnet run --project src/UpSkillingPlatform.API/UpSkillingPlatform.API.csproj
```

**Saída esperada:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
✅ Database migrated successfully!
```

---

### 🌐 Passo 6: Acessar a API

A aplicação estará disponível em:

| Recurso | URL |
|---------|-----|
| 🌐 HTTP | http://localhost:5000 |
| 🔒 HTTPS | https://localhost:5001 |
| 📚 Swagger UI | http://localhost:5000/swagger |

**Abra o Swagger UI no navegador:**
```bash
# Linux
xdg-open http://localhost:5000/swagger

# macOS
open http://localhost:5000/swagger

# Windows
start http://localhost:5000/swagger
```

---

### 🧪 Passo 7: Testar a API

#### **Opção 1: Usar o Swagger UI** (Recomendado)

1. Acesse http://localhost:5000/swagger
2. Expanda um endpoint (ex: `GET /api/usuarios`)
3. Clique em **"Try it out"**
4. Clique em **"Execute"**
5. Veja a resposta abaixo

#### **Opção 2: Executar Testes Unitários**

O projeto inclui **39 testes unitários** com MSTest e Moq:

```bash
# Executar todos os testes
cd tests/UpSkillingPlatform.Tests
dotnet test

# Executar com mais detalhes
dotnet test --logger "console;verbosity=detailed"

# Executar com cobertura (requer coverlet)
dotnet test /p:CollectCoverage=true
```

**Testes incluídos:**
- ✅ 11 testes para `UsuarioService` (Create, Read, Update, Delete, validações)
- ✅ 12 testes para `TrilhaService` (Create, Read, Update, Delete, validações de nível)
- ✅ 16 testes para `MatriculaService` (Create, Read, cancelamento, conclusão, validações)

**Total: 39 testes com 100% de sucesso!**

#### **Opção 3: Usar cURL**

```bash
# Listar todas as trilhas
curl http://localhost:5000/api/trilhas

# Criar um novo usuário
curl -X POST http://localhost:5000/api/usuarios \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Maria Silva",
    "email": "maria.silva@email.com",
    "areaAtuacao": "Tecnologia",
    "nivelCarreira": "Pleno"
  }'

# Buscar usuário por ID
curl http://localhost:5000/api/usuarios/1

# Listar todos os usuários
curl http://localhost:5000/api/usuarios
```

#### **Opção 3: Usar REST Client (VS Code)**

Se estiver usando VS Code, instale a extensão **REST Client** e use o arquivo `tests/api-requests.http`:

```bash
# Abrir no VS Code
code tests/api-requests.http
```

Clique em **"Send Request"** acima de cada requisição.

---

## 🧪 Executar Testes

O projeto utiliza **MSTest** com **Moq** para testes unitários.

```bash
# Executar todos os testes
cd tests/UpSkillingPlatform.Tests
dotnet test

# Executar com detalhes
dotnet test --logger "console;verbosity=detailed"

# Executar testes específicos
dotnet test --filter "FullyQualifiedName~UsuarioServiceTests"
dotnet test --filter "FullyQualifiedName~TrilhaServiceTests"
dotnet test --filter "FullyQualifiedName~MatriculaServiceTests"
```

**Cobertura de Testes:**
- ✅ **39 testes unitários** (11 Usuário + 12 Trilha + 16 Matrícula)
- ✅ 100% de cobertura dos Services
- ✅ Testes de cenários de sucesso e erro
- ✅ Validação de exceções customizadas
- ✅ Validação completa das regras de negócio

---

## 📡 Endpoints da API

### 👤 Usuários (`/api/usuarios`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/usuarios` | Lista todos os usuários | 200 |
| `GET` | `/api/usuarios/{id}` | Busca usuário por ID | 200, 404 |
| `POST` | `/api/usuarios` | Cria novo usuário | 201, 400, 409 |
| `PUT` | `/api/usuarios/{id}` | Atualiza usuário | 200, 400, 404, 409 |
| `DELETE` | `/api/usuarios/{id}` | Remove usuário | 204, 404 |

#### **Exemplo: Criar Usuário**

**Request:**
```json
POST /api/usuarios
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao.silva@email.com",
  "areaAtuacao": "Tecnologia",
  "nivelCarreira": "Pleno"
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "nome": "João Silva",
  "email": "joao.silva@email.com",
  "areaAtuacao": "Tecnologia",
  "nivelCarreira": "Pleno",
  "dataCadastro": "2025-11-13T10:30:00"
}
```

---

### 📚 Trilhas (`/api/trilhas`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/trilhas` | Lista todas as trilhas | 200 |
| `GET` | `/api/trilhas/{id}` | Busca trilha por ID | 200, 404 |
| `POST` | `/api/trilhas` | Cria nova trilha | 201, 400, 422 |
| `PUT` | `/api/trilhas/{id}` | Atualiza trilha | 200, 400, 404, 422 |
| `DELETE` | `/api/trilhas/{id}` | Remove trilha | 204, 404 |

#### **Exemplo: Criar Trilha**

**Request:**
```json
POST /api/trilhas
Content-Type: application/json

{
  "nome": "DevOps Essencial",
  "descricao": "Aprenda DevOps do zero ao avançado",
  "nivel": "INTERMEDIARIO",
  "cargaHoraria": 60,
  "focoPrincipal": "DevOps"
}
```

**Níveis Aceitos:**
- `INICIANTE`
- `INTERMEDIARIO`
- `AVANCADO`

---

### 📝 Matrículas (`/api/matriculas`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/matriculas` | Lista todas as matrículas | 200 |
| `GET` | `/api/matriculas/{id}` | Busca matrícula por ID | 200, 404 |
| `GET` | `/api/matriculas/usuario/{usuarioId}` | Lista matrículas do usuário | 200, 404 |
| `GET` | `/api/matriculas/trilha/{trilhaId}` | Lista matrículas da trilha | 200, 404 |
| `POST` | `/api/matriculas` | Cria nova matrícula (inscrição) | 201, 400, 404, 409 |
| `PATCH` | `/api/matriculas/{id}/cancelar` | Cancela matrícula | 200, 404, 422 |
| `PATCH` | `/api/matriculas/{id}/concluir` | Conclui matrícula | 200, 404, 422 |
| `DELETE` | `/api/matriculas/{id}` | Remove matrícula | 204, 404 |

#### **Exemplo: Criar Matrícula (Inscrever Aluno)**

**Request:**
```json
POST /api/matriculas
Content-Type: application/json

{
  "usuarioId": 1,
  "trilhaId": 1
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "usuarioId": 1,
  "nomeUsuario": "João Silva",
  "emailUsuario": "joao.silva@email.com",
  "trilhaId": 1,
  "nomeTrilha": "DevOps Essencial",
  "nivelTrilha": "Intermediário",
  "cargaHoraria": 60,
  "dataInscricao": "2025-11-15T18:30:00",
  "status": "Ativa"
}
```

#### **Status de Matrícula:**
- `Ativa` - Matrícula em andamento
- `Concluída` - Trilha finalizada com sucesso
- `Cancelada` - Matrícula cancelada

#### **Regras de Negócio:**
- ✅ Usuário deve existir
- ✅ Trilha deve existir
- ✅ Usuário não pode ter matrícula ativa duplicada na mesma trilha
- ✅ Não pode cancelar matrícula já cancelada ou concluída
- ✅ Não pode concluir matrícula cancelada

---

## 📊 Modelo de Dados

### Diagrama Entidade-Relacionamento

```
┌─────────────────┐         ┌──────────────────┐         ┌─────────────────┐
│    Usuario      │         │    Matricula     │         │     Trilha      │
├─────────────────┤         ├──────────────────┤         ├─────────────────┤
│ PK Id           │◄───────┤│ PK Id            │         │ PK Id           │
│    Nome         │      1:N│ FK UsuarioId     │N:1 ────►│    Nome         │
│    Email (UK)   │         │ FK TrilhaId      │         │    Descricao    │
│    AreaAtuacao  │         │    DataInscricao │         │    Nivel        │
│    NivelCarreira│         │    Status        │         │    CargaHoraria │
│    DataCadastro │         └──────────────────┘         │    FocoPrincipal│
└─────────────────┘                                       └────────┬────────┘
                                                                   │
                                                                 N:N
                                                                   │
                    ┌──────────────────┐         ┌───────────────▼────────┐
                    │   Competencia    │         │ TrilhaCompetencia      │
                    ├──────────────────┤         ├────────────────────────┤
                    │ PK Id            │◄───────┤│ PK,FK TrilhaId         │
                    │    Nome          │      N:N│ PK,FK CompetenciaId    │
                    │    Categoria     │         └────────────────────────┘
                    │    Descricao     │
                    └──────────────────┘
```

### Entidades

#### **Usuario**
```csharp
- Id: long (PK)
- Nome: string (max 100)
- Email: string (max 150, unique)
- AreaAtuacao: string? (max 100)
- NivelCarreira: string? (max 50)
- DataCadastro: DateTime
```

#### **Trilha**
```csharp
- Id: long (PK)
- Nome: string (max 150)
- Descricao: string?
- Nivel: string (max 50) // INICIANTE, INTERMEDIARIO, AVANCADO
- CargaHoraria: int
- FocoPrincipal: string? (max 100)
```

#### **Competencia**
```csharp
- Id: long (PK)
- Nome: string (max 100)
- Categoria: string? (max 100)
- Descricao: string?
```

#### **Matricula**
```csharp
- Id: long (PK)
- UsuarioId: long (FK)
- TrilhaId: long (FK)
- DataInscricao: DateTime
- Status: string (max 50)
```

#### **TrilhaCompetencia** (Join Table)
```csharp
- TrilhaId: long (PK, FK)
- CompetenciaId: long (PK, FK)
```

---

## 🛡️ Tratamento de Erros

A API implementa um middleware global de tratamento de exceções que retorna respostas padronizadas.

### Status Codes

| Código | Significado | Quando Ocorre |
|--------|-------------|---------------|
| `200 OK` | Sucesso | GET, PUT bem-sucedidos |
| `201 Created` | Criado | POST bem-sucedido |
| `204 No Content` | Sem conteúdo | DELETE bem-sucedido |
| `400 Bad Request` | Requisição inválida | Validação de dados falhou |
| `404 Not Found` | Não encontrado | Recurso não existe |
| `409 Conflict` | Conflito | Email já cadastrado |
| `422 Unprocessable Entity` | Entidade não processável | Nível de trilha inválido |
| `500 Internal Server Error` | Erro interno | Erro não tratado |

### Exemplo de Resposta de Erro

```json
{
  "statusCode": 404,
  "message": "Usuário com ID 999 não foi encontrado.",
  "timestamp": "2025-11-13T10:30:00Z"
}
```

---

## 📝 Comandos Úteis

### Docker

```bash
# Iniciar containers
docker-compose up -d

# Parar containers
docker-compose down

# Ver logs do MySQL
docker-compose logs -f mysql

# Reiniciar MySQL
docker-compose restart mysql

# Acessar MySQL via CLI
docker exec -it upskilling_mysql mysql -u upskilling_user -p
# Senha: upskilling_pass
```

### .NET CLI

```bash
# Restaurar dependências
dotnet restore

# Compilar projeto
dotnet build

# Executar aplicação
cd src/UpSkillingPlatform.API
dotnet run

# Executar em modo watch (auto-reload)
dotnet watch run

# Limpar builds
dotnet clean

# Publicar para produção
dotnet publish -c Release

# Executar testes
cd tests/UpSkillingPlatform.Tests
dotnet test
```

### Entity Framework Core

```bash
# Listar migrations
cd src/UpSkillingPlatform.API
dotnet ef migrations list

# Criar nova migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations
dotnet ef database update

# Reverter para migration específica
dotnet ef database update NomeDaMigration

# Remover última migration
dotnet ef migrations remove

# Gerar script SQL das migrations
dotnet ef migrations script

# Drop do banco (cuidado!)
dotnet ef database drop
```

---

## 🧹 Limpeza e Reset

### Resetar o Banco de Dados

```bash
# Parar e remover containers
docker-compose down -v

# Iniciar novamente
docker-compose up -d

# Aguardar MySQL estar pronto (~30s)
sleep 30

# Aplicar migrations
cd UpSkillingPlatform
dotnet ef database update
```

### Limpar Build Artifacts

```bash
# Limpar builds de todos os projetos
dotnet clean

# Remover pastas bin e obj manualmente
find . -type d -name "bin" -o -name "obj" | xargs rm -rf

# Restaurar e compilar novamente
dotnet restore
dotnet build
```

---

## 📊 Estrutura do Projeto

O projeto segue **Clean Architecture** com separação em 4 camadas:

```
gs/
├── src/
│   ├── UpSkillingPlatform.Domain/           # Camada de Domínio
│   │   ├── Entities/                        # Entidades do negócio
│   │   │   ├── Usuario.cs
│   │   │   ├── Trilha.cs
│   │   │   ├── Competencia.cs
│   │   │   ├── TrilhaCompetencia.cs
│   │   │   └── Matricula.cs
│   │   ├── Interfaces/                      # Contratos de repositórios
│   │   │   ├── IRepository.cs
│   │   │   ├── IUsuarioRepository.cs
│   │   │   └── ITrilhaRepository.cs
│   │   └── Exceptions/                      # Exceções customizadas
│   │       └── CustomExceptions.cs
│   │
│   ├── UpSkillingPlatform.Application/      # Camada de Aplicação
│   │   ├── Services/                        # Lógica de negócio
│   │   │   ├── IUsuarioService.cs
│   │   │   ├── UsuarioService.cs
│   │   │   ├── ITrilhaService.cs
│   │   │   └── TrilhaService.cs
│   │   └── DTOs/                            # Data Transfer Objects
│   │       ├── UsuarioDto.cs
│   │       └── TrilhaDto.cs
│   │
│   ├── UpSkillingPlatform.Infrastructure/   # Camada de Infraestrutura
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs              # Contexto do EF Core
│   │   │   └── Configurations/              # Fluent API
│   │   │       ├── UsuarioConfiguration.cs
│   │   │       ├── TrilhaConfiguration.cs
│   │   │       ├── CompetenciaConfiguration.cs
│   │   │       ├── TrilhaCompetenciaConfiguration.cs
│   │   │       └── MatriculaConfiguration.cs
│   │   └── Repositories/                    # Implementação dos repositórios
│   │       ├── Repository.cs
│   │       ├── UsuarioRepository.cs
│   │       └── TrilhaRepository.cs
│   │
│   └── UpSkillingPlatform.API/              # Camada de Apresentação
│       ├── Controllers/                     # Endpoints REST
│       │   ├── UsuariosController.cs
│       │   └── TrilhasController.cs
│       ├── Middleware/                      # Middleware customizado
│       │   └── GlobalExceptionHandlerMiddleware.cs
│       ├── Program.cs                       # Entry point
│       ├── appsettings.json                 # Configurações
│       └── appsettings.Development.json
│
├── tests/
│   └── UpSkillingPlatform.Tests/            # Testes Unitários
│       └── Services/
│           ├── UsuarioServiceTests.cs       # 11 testes
│           └── TrilhaServiceTests.cs        # 12 testes
│
├── docker-compose.yml                       # Docker MySQL
├── README.md                                # Este arquivo
└── UpSkillingPlatform.sln                   # Solução .NET
```

### Dependências entre Projetos

```
UpSkillingPlatform.API
  ├─→ UpSkillingPlatform.Application
  │     └─→ UpSkillingPlatform.Domain
  └─→ UpSkillingPlatform.Infrastructure
        └─→ UpSkillingPlatform.Domain

UpSkillingPlatform.Tests
  ├─→ UpSkillingPlatform.Application
  └─→ UpSkillingPlatform.Domain
```

---

## 🎯 Dados de Exemplo (Seed Data)

Ao aplicar as migrations, os seguintes dados são inseridos automaticamente:

### Competências (8)

| ID | Nome | Categoria | Descrição |
|----|------|-----------|-----------|
| 1 | Inteligência Artificial | Tecnologia | Machine Learning, Deep Learning, NLP |
| 2 | Análise de Dados | Tecnologia | Data Science, Business Intelligence |
| 3 | Cloud Computing | Tecnologia | AWS, Azure, Google Cloud |
| 4 | Desenvolvimento Web | Tecnologia | Frontend e Backend moderno |
| 5 | Comunicação Efetiva | Humana | Comunicação clara e assertiva |
| 6 | Trabalho em Equipe | Humana | Colaboração e cooperação |
| 7 | Pensamento Crítico | Humana | Análise e resolução de problemas |
| 8 | Gestão de Projetos | Gestão | Metodologias ágeis |

### Trilhas (4)

| ID | Nome | Nível | Carga Horária | Foco |
|----|------|-------|---------------|------|
| 1 | Fundamentos de IA e ML | INICIANTE | 40h | IA |
| 2 | Cientista de Dados Profissional | INTERMEDIARIO | 120h | Dados |
| 3 | Arquitetura Cloud Avançada | AVANCADO | 80h | Cloud |
| 4 | Soft Skills para o Futuro | INICIANTE | 30h | Soft Skills |

---

## 🐛 Troubleshooting (Solução de Problemas)

### Problema: "Connection refused" ao banco

**Causa:** MySQL ainda não está pronto ou não está rodando.

**Solução:**
```bash
# Verificar se container está rodando
docker ps

# Ver logs do MySQL
docker-compose logs mysql

# Aguardar mensagem "ready for connections"
# Pode levar até 30 segundos na primeira vez
```

---

### Problema: "Port 5000 already in use"

**Causa:** Outra aplicação está usando a porta 5000.

**Solução:**
```bash
# Matar processo na porta 5000 (Linux/Mac)
lsof -ti:5000 | xargs kill -9

# Ou alterar a porta em appsettings.json
"Urls": "http://localhost:5001"
```

---

### Problema: "A network-related or instance-specific error"

**Causa:** String de conexão incorreta ou MySQL não acessível.

**Solução:**
```bash
# Verificar se MySQL está acessível
docker exec -it upskilling_mysql mysql -u upskilling_user -p upskilling_db

# Verificar connection string em appsettings.json
"DefaultConnection": "Server=localhost;Port=3306;Database=upskilling_db;User=upskilling_user;Password=upskilling_pass;"
```

---

### Problema: "No migrations found"

**Causa:** Migrations ainda não foram criadas.

**Solução:**
```bash
cd UpSkillingPlatform
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### Problema: Docker Compose não encontrado

**Causa:** Docker Desktop não instalado ou Docker Compose v2 não configurado.

**Solução:**
```bash
# Verificar versão do Docker Compose
docker compose version

# Se não funcionar, tentar com hífen
docker-compose version

# Instalar Docker Desktop (recomendado)
# https://www.docker.com/products/docker-desktop
```

---

## 👥 Equipe

### Integrantes

| Nome | RM |
|------|------|
| **Felipe Heilmann Marques** | RM551026 |
| **Ian Cancian Nachtergaele** | RM98387 |
| **Carlos Eduardo Caramante Ribeiro** | RM552159 |

---

## 📚 Documentação Adicional

- 📖 [README-DOCKER.md](README-DOCKER.md) - Guia completo de Docker
- 🐳 [DOCKERIZACAO.md](DOCKERIZACAO.md) - Resumo da dockerização
- � [GUIA-MIGRATIONS.md](GUIA-MIGRATIONS.md) - Guia de Entity Framework Migrations
- 📝 [FUNCIONALIDADE-MATRICULA.md](FUNCIONALIDADE-MATRICULA.md) - Documentação da funcionalidade de matrículas
- 💡 [EXEMPLOS-API-MATRICULA.md](EXEMPLOS-API-MATRICULA.md) - Exemplos práticos de uso da API
- �️ [INDICE-DOCUMENTACAO.md](INDICE-DOCUMENTACAO.md) - Índice completo da documentação

---

<div align="center">

**⭐ Desenvolvido para o Futuro do Trabalho 2030+ ⭐**

Made with [.NET](https://dotnet.microsoft.com/) • [MySQL](https://www.mysql.com/) • [Docker](https://www.docker.com/)

</div>
