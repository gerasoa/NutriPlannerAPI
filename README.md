# NutriPlannerAPI

Uma API RESTful para gerenciamento de consultas e planejamento nutricional, desenvolvida em .NET 8.0.

## 🚀 Deploy no Heroku

### Pré-requisitos

1. **Heroku CLI**: Instale o [Heroku CLI](https://devcenter.heroku.com/articles/heroku-cli)
2. **Git**: Certifique-se de que o Git está instalado e configurado
3. **Conta no Heroku**: Crie uma conta gratuita em [heroku.com](https://heroku.com)

### Deploy Automático

Execute um dos scripts de deploy:

**Para Windows (PowerShell):**
```powershell
.\deploy-heroku.ps1
```

**Para Linux/Mac (Bash):**
```bash
chmod +x deploy-heroku.sh
./deploy-heroku.sh
```

### Deploy Manual

1. **Login no Heroku:**
   ```bash
   heroku login
   ```

2. **Criar aplicação:**
   ```bash
   heroku create sua-nutriplanner-api
   ```

3. **Adicionar PostgreSQL:**
   ```bash
   heroku addons:create heroku-postgresql:mini
   ```

4. **Configurar variáveis de ambiente:**
   ```bash
   heroku config:set ASPNETCORE_ENVIRONMENT=Production
   heroku config:set JWT_SECRET="47D2E976-3006-44B9-87D2-0560D19B35D2"
   ```

5. **Deploy:**
   ```bash
   git add .
   git commit -m "Deploy to Heroku"
   git push heroku main
   ```

6. **Executar migrações:**
   ```bash
   heroku run "cd src/CCRS.Api && dotnet ef database update"
   ```

### Variáveis de Ambiente

As seguintes variáveis podem ser configuradas no Heroku:

- `DATABASE_URL`: String de conexão do banco (automaticamente configurada pelo addon PostgreSQL)
- `JWT_SECRET`: Chave secreta para JWT
- `ASPNETCORE_ENVIRONMENT`: Ambiente da aplicação (Production)
- `ELMAH_API_KEY`: Chave da API do Elmah.IO (opcional)
- `ELMAH_LOG_ID`: ID do log do Elmah.IO (opcional)

### Arquivos de Configuração

- `Procfile`: Define como executar a aplicação no Heroku
- `Dockerfile`: Container Docker para deploy
- `appsettings.Production.json`: Configurações para produção
- `HerokuConfig.cs`: Configurações específicas para Heroku

## 🛠️ Desenvolvimento Local

### Requisitos

- .NET 8.0 SDK
- SQL Server LocalDB ou SQL Server
- Visual Studio 2022 ou VS Code

### Executar localmente

1. Clone o repositório
2. Restaure os pacotes: `dotnet restore`
3. Execute as migrações: `dotnet ef database update --project src/CCRS.Api`
4. Execute a aplicação: `dotnet run --project src/CCRS.Api`

## 📚 API Documentation

A documentação da API está disponível via Swagger em `/swagger` quando a aplicação está rodando.

## 🏗️ Estrutura do Projeto

```
src/
├── CCRS.Api/          # Camada de apresentação (Controllers, Configuration)
├── CCRS.Business/     # Camada de negócio (Services, Models, Interfaces)
└── CCRS.Data/         # Camada de dados (Repositories, Context, Migrations)
```