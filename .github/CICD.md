# CI/CD Configuration for StudentWorkHub

This repository uses GitHub Actions for continuous integration and deployment.

## Workflows

### 🔄 CI Pipeline (`ci.yml`)
**Triggers:** Push to `main`/`develop`, Pull Requests

Runs on every push and pull request to ensure code quality:
- **Frontend Lint & Type Check** - ESLint and TypeScript validation
- **Frontend Unit Tests** - Vitest unit tests with coverage
- **Frontend E2E Tests** - Playwright end-to-end tests
- **Backend Tests** - .NET test suite
- **Docker Build** - Validates Docker images build successfully
- **Security Scan** - Trivy vulnerability scanning

### 🚀 CD Pipeline (`cd.yml`)
**Triggers:** Push to `main`, Tags, Manual dispatch

Automates deployment to production/staging:
- **Build & Push** - Builds and pushes Docker images to GitHub Container Registry
- **Deploy to Production** - SSH deployment to production server
- **Deploy to Staging** - Manual deployment to staging environment
- **Rollback** - Automatic rollback on deployment failure

### 💾 Database Backup (`backup.yml`)
**Triggers:** Daily at 2 AM UTC, Manual dispatch

Automated database backups:
- Dumps PostgreSQL databases (general and offers)
- Compresses and stores backups
- Retains last 30 days of backups
- Optional cloud storage upload

### 📊 Performance Monitoring (`performance.yml`)
**Triggers:** Every 6 hours, Manual dispatch

Monitors application performance:
- **Lighthouse CI** - Performance, accessibility, SEO audits
- **Load Testing** - k6 load tests (manual trigger)

### 🔧 Dependency Updates (`dependencies.yml`)
**Triggers:** Weekly on Monday, Manual dispatch

Automated dependency management:
- Updates npm packages (frontend)
- Updates NuGet packages (backend)
- Creates PRs with updates
- Runs tests before creating PR

## Required Secrets

Configure these secrets in your GitHub repository settings:

### Production Deployment
- `PROD_SERVER_HOST` - Production server hostname/IP
- `PROD_SERVER_USER` - SSH username for production
- `PROD_SERVER_SSH_KEY` - SSH private key for production

### Staging Deployment (Optional)
- `STAGING_SERVER_HOST` - Staging server hostname/IP
- `STAGING_SERVER_USER` - SSH username for staging
- `STAGING_SERVER_SSH_KEY` - SSH private key for staging

### Performance Monitoring (Optional)
- `K6_CLOUD_TOKEN` - k6 Cloud token for load testing

## Environment Variables

The following environment variables are used in CI/CD:

```bash
# Node.js
NODE_VERSION=20.x

# .NET
DOTNET_VERSION=8.0.x

# Docker Registry
REGISTRY=ghcr.io
```

## Branch Strategy

- `main` - Production branch (auto-deploys)
- `develop` - Development branch
- `feature/*` - Feature branches
- `deps/*` - Dependency update branches

## Manual Deployment

You can trigger deployments manually:

1. Go to **Actions** tab
2. Select **CD - Deploy to Production** workflow
3. Click **Run workflow**
4. Choose environment (production/staging)
5. Click **Run workflow**

## Monitoring CI/CD Status

Check the status of your workflows:
- Go to the **Actions** tab in your repository
- View workflow runs and logs
- Download artifacts (test reports, coverage, etc.)

## Local Testing

Before pushing, you can run tests locally:

```bash
# Frontend tests
cd frontend
npm run test:unit
npm run test:e2e

# Backend tests
cd offer_collector
dotnet test

# Docker build test
docker compose build
```

## Rollback Procedure

If a deployment fails:
1. Automatic rollback will be triggered
2. Previous version will be restored
3. Check logs in Actions tab for failure details

Manual rollback:
```bash
# SSH to production server
ssh user@prod-server

cd /opt/StudentWorkHub
git log --oneline -10  # Find previous stable commit
git checkout <commit-hash>
docker compose -f docker-compose-prod.yml down
docker compose -f docker-compose-prod.yml up -d
```

## Health Checks

After deployment, automatic health checks verify:
- Frontend accessibility
- Backend API responsiveness
- Database connectivity

## Security

- All secrets are encrypted by GitHub
- SSH keys use strong encryption
- Docker images are scanned for vulnerabilities
- Dependencies are automatically updated weekly

## Troubleshooting

### CI Tests Failing
1. Check test logs in Actions tab
2. Run tests locally to reproduce
3. Fix issues and push again

### Deployment Failing
1. Check SSH connectivity to server
2. Verify secrets are configured correctly
3. Check server disk space and resources
4. Review deployment logs

### Docker Build Failing
1. Verify Dockerfile syntax
2. Check base image availability
3. Ensure all dependencies are installed
4. Test build locally

## Contributing

When contributing:
1. Create feature branch from `develop`
2. Make changes and commit
3. Push and create Pull Request
4. CI will automatically run tests
5. Merge after approval and successful CI

## Support

For CI/CD issues:
1. Check Actions tab for detailed logs
2. Review this documentation
3. Open an issue with CI/CD label
