#!/bin/bash

# Deploy script for Heroku
# Make sure you have Heroku CLI installed and you're logged in

APP_NAME="nutriplanner-api"  # Change this to your desired app name

echo "🚀 Starting Heroku deployment for $APP_NAME..."

# Check if Heroku CLI is installed
if ! command -v heroku &> /dev/null; then
    echo "❌ Heroku CLI not found. Please install it first:"
    echo "   https://devcenter.heroku.com/articles/heroku-cli"
    exit 1
fi

# Check if user is logged in to Heroku
if ! heroku auth:whoami &> /dev/null; then
    echo "🔐 Please login to Heroku:"
    heroku auth:login
fi

# Create Heroku app if it doesn't exist
if ! heroku apps:info $APP_NAME &> /dev/null; then
    echo "📱 Creating Heroku app: $APP_NAME"
    heroku create $APP_NAME
    
    # Add PostgreSQL addon
    echo "🐘 Adding PostgreSQL addon..."
    heroku addons:create heroku-postgresql:mini --app $APP_NAME
else
    echo "📱 App $APP_NAME already exists"
fi

# Set environment variables
echo "⚙️  Setting environment variables..."
heroku config:set ASPNETCORE_ENVIRONMENT=Production --app $APP_NAME
heroku config:set JWT_SECRET="47D2E976-3006-44B9-87D2-0560D19B35D2" --app $APP_NAME

# Optional: Set ELMAH.IO configuration (uncomment and set your values)
# heroku config:set ELMAH_API_KEY="your_elmah_api_key" --app $APP_NAME
# heroku config:set ELMAH_LOG_ID="your_elmah_log_id" --app $APP_NAME

# Add Heroku remote if not exists
if ! git remote get-url heroku &> /dev/null; then
    echo "🔗 Adding Heroku remote..."
    heroku git:remote -a $APP_NAME
fi

# Deploy to Heroku
echo "🚢 Deploying to Heroku..."
git add .
git commit -m "Deploy to Heroku - $(date)"
git push heroku main

# Run database migrations if needed
echo "💾 Running database migrations..."
heroku run "cd src/CCRS.Api && dotnet ef database update" --app $APP_NAME

# Open the app
echo "✅ Deployment complete!"
echo "🌐 Opening app in browser..."
heroku open --app $APP_NAME

echo "📋 App URL: https://$APP_NAME.herokuapp.com"
echo "📊 View logs: heroku logs --tail --app $APP_NAME"