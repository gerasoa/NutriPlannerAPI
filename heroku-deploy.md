# Heroku Deploy Configuration

## Environment Variables needed for Heroku:
DATABASE_URL=your_production_database_connection_string
JWT_SECRET=your_jwt_secret_key_here
ELMAH_API_KEY=your_elmah_api_key_here
ELMAH_LOG_ID=your_elmah_log_id_here

## Heroku CLI Commands for setup:

# 1. Login to Heroku
heroku login

# 2. Create Heroku app
heroku create your-nutriplanner-api

# 3. Set environment variables
heroku config:set DATABASE_URL="your_database_connection_string"
heroku config:set JWT_SECRET="47D2E976-3006-44B9-87D2-0560D19B35D2"
heroku config:set ASPNETCORE_ENVIRONMENT=Production

# 4. Add PostgreSQL addon (recommended for production)
heroku addons:create heroku-postgresql:mini

# 5. Deploy to Heroku
git add .
git commit -m "Deploy to Heroku"
git push heroku main

# 6. Run database migrations (if needed)
heroku run dotnet ef database update --project src/CCRS.Api

## Alternative: Using Heroku Container Registry (Docker)
# heroku container:login
# heroku container:push web --app your-nutriplanner-api
# heroku container:release web --app your-nutriplanner-api