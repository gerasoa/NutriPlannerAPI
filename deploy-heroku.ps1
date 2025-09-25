# Deploy script for Heroku (PowerShell version)
# Make sure you have Heroku CLI installed and you're logged in

$APP_NAME = "nutriplanner-api"  # Change this to your desired app name

Write-Host "🚀 Starting Heroku deployment for $APP_NAME..." -ForegroundColor Green

# Check if Heroku CLI is installed
try {
    heroku --version | Out-Null
} catch {
    Write-Host "❌ Heroku CLI not found. Please install it first:" -ForegroundColor Red
    Write-Host "   https://devcenter.heroku.com/articles/heroku-cli" -ForegroundColor Yellow
    exit 1
}

# Check if user is logged in to Heroku
try {
    heroku auth:whoami | Out-Null
} catch {
    Write-Host "🔐 Please login to Heroku:" -ForegroundColor Yellow
    heroku auth:login
}

# Create Heroku app if it doesn't exist
try {
    heroku apps:info $APP_NAME | Out-Null
    Write-Host "📱 App $APP_NAME already exists" -ForegroundColor Blue
    
    # Ensure .NET buildpack is set
    Write-Host "🔧 Ensuring .NET buildpack is set..." -ForegroundColor Green
    heroku buildpacks:set https://github.com/heroku/dotnet-buildpack --app $APP_NAME
} catch {
    Write-Host "📱 Creating Heroku app: $APP_NAME" -ForegroundColor Green
    heroku create $APP_NAME
    
    # Set .NET buildpack
    Write-Host "🔧 Setting .NET buildpack..." -ForegroundColor Green
    heroku buildpacks:set https://github.com/heroku/dotnet-buildpack --app $APP_NAME
    
    # Add PostgreSQL addon
    Write-Host "🐘 Adding PostgreSQL addon..." -ForegroundColor Green
    heroku addons:create heroku-postgresql:mini --app $APP_NAME
}

# Set environment variables
Write-Host "⚙️  Setting environment variables..." -ForegroundColor Green
heroku config:set ASPNETCORE_ENVIRONMENT=Production --app $APP_NAME
heroku config:set JWT_SECRET="47D2E976-3006-44B9-87D2-0560D19B35D2" --app $APP_NAME
heroku config:set PROJECT_FILE=src/CCRS.Api/CCRS.Api.csproj --app $APP_NAME
heroku config:set DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true --app $APP_NAME
heroku config:set DOTNET_CLI_TELEMETRY_OPTOUT=true --app $APP_NAME

# Optional: Set ELMAH.IO configuration (uncomment and set your values)
# heroku config:set ELMAH_API_KEY="your_elmah_api_key" --app $APP_NAME
# heroku config:set ELMAH_LOG_ID="your_elmah_log_id" --app $APP_NAME

# Add Heroku remote if not exists
try {
    git remote get-url heroku | Out-Null
} catch {
    Write-Host "🔗 Adding Heroku remote..." -ForegroundColor Green
    heroku git:remote -a $APP_NAME
}

# Deploy to Heroku
Write-Host "🚢 Deploying to Heroku..." -ForegroundColor Green
git add .
git commit -m "Deploy to Heroku - $(Get-Date)"
git push heroku main

# Run database migrations if needed
Write-Host "💾 Running database migrations..." -ForegroundColor Green
heroku run "cd src/CCRS.Api && dotnet ef database update" --app $APP_NAME

# Open the app
Write-Host "✅ Deployment complete!" -ForegroundColor Green
Write-Host "🌐 Opening app in browser..." -ForegroundColor Green
heroku open --app $APP_NAME

Write-Host "📋 App URL: https://$APP_NAME.herokuapp.com" -ForegroundColor Cyan
Write-Host "📊 View logs: heroku logs --tail --app $APP_NAME" -ForegroundColor Cyan