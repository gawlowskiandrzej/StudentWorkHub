#!/bin/bash

# Local CI/CD Testing Script
# This script simulates CI pipeline locally before pushing

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Get script directory and project root
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"

# Change to project root
cd "$PROJECT_ROOT"

# Parse arguments
SKIP_LINT=false
SKIP_TESTS=false
SKIP_BACKEND=false
SKIP_DOCKER=false
RUN_E2E=false

while [[ $# -gt 0 ]]; do
  case $1 in
    --skip-lint)
      SKIP_LINT=true
      shift
      ;;
    --skip-tests)
      SKIP_TESTS=true
      shift
      ;;
    --skip-backend)
      SKIP_BACKEND=true
      shift
      ;;
    --skip-docker)
      SKIP_DOCKER=true
      shift
      ;;
    --run-e2e)
      RUN_E2E=true
      shift
      ;;
    --help|-h)
      echo -e "${BLUE}Local CI/CD Testing Script${NC}"
      echo ""
      echo "Usage: $0 [OPTIONS]"
      echo ""
      echo "Options:"
      echo "  --skip-lint       Skip frontend linting and type checking"
      echo "  --skip-tests      Skip frontend unit tests"
      echo "  --skip-backend    Skip backend build and tests"
      echo "  --skip-docker     Skip Docker build tests"
      echo "  --run-e2e         Run E2E tests (requires Docker)"
      echo "  -h, --help        Show this help message"
      echo ""
      echo "Examples:"
      echo "  $0                           # Run all checks except E2E"
      echo "  $0 --skip-lint               # Skip linting"
      echo "  $0 --run-e2e                 # Include E2E tests"
      echo "  $0 --skip-backend --run-e2e  # Skip backend, run E2E"
      exit 0
      ;;
    *)
      echo -e "${RED}Unknown option: $1${NC}"
      echo "Use --help for usage information"
      exit 1
      ;;
  esac
done

echo -e "${BLUE}🚀 Starting local CI/CD simulation...${NC}"
echo ""

# Function to print status
print_status() {
    if [ $1 -eq 0 ]; then
        echo -e "${GREEN}✅ $2${NC}"
    else
        echo -e "${RED}❌ $2${NC}"
        exit 1
    fi
}

print_warning() {
    echo -e "${YELLOW}⚠️  $1${NC}"
}

# Track if any critical step failed
CRITICAL_FAILURE=false

if [ "$SKIP_LINT" = false ]; then
echo "==================================="
echo "1. Frontend Linting & Type Check"
echo "==================================="

cd "$PROJECT_ROOT/frontend"

echo "Running ESLint..."
npm run lint || print_warning "ESLint found issues (non-blocking)"

echo "Running TypeScript type check..."
npx tsc --noEmit || print_warning "TypeScript found type errors (non-blocking)"
fi

if [ "$SKIP_TESTS" = false ]; then
echo ""
echo "==================================="
echo "2. Frontend Unit Tests"
echo "==================================="

cd "$PROJECT_ROOT/frontend"

echo "Running Vitest unit tests..."
npm run test:unit
if [ $? -ne 0 ]; then
    print_status 1 "Unit tests failed"
    CRITICAL_FAILURE=true
else
    print_status 0 "Unit tests passed"
fi
fi

if [ "$SKIP_BACKEND" = false ]; then
echo ""
echo "==================================="
echo "3. Backend Build & Tests"
echo "==================================="

cd "$PROJECT_ROOT/offer_collector"

echo "Building .NET project..."
dotnet build -c Release
if [ $? -ne 0 ]; then
    print_status 1 ".NET build failed"
    CRITICAL_FAILURE=true
else
    print_status 0 ".NET build passed"
fi

echo "Running .NET tests..."
dotnet test --no-build -c Release
if [ $? -ne 0 ]; then
    print_status 1 ".NET tests failed"
    CRITICAL_FAILURE=true
else
    print_status 0 ".NET tests passed"
fi
fi

if [ "$SKIP_DOCKER" = false ]; then
echo ""
echo "==================================="
echo "4. Docker Build Test"
echo "==================================="

cd "$PROJECT_ROOT"

echo "Building frontend Docker image..."
docker build -t studentworkhub/frontend:test -f frontend/Dockerfile frontend/
if [ $? -ne 0 ]; then
    print_status 1 "Frontend Docker build failed"
    CRITICAL_FAILURE=true
else
    print_status 0 "Frontend Docker build passed"
fi

echo "Building backend Docker image..."
docker build -t studentworkhub/offer_manager:test -f offer_collector/Dockerfile.offer_manager offer_collector/
if [ $? -ne 0 ]; then
    print_status 1 "Backend Docker build failed"
    CRITICAL_FAILURE=true
else
    print_status 0 "Backend Docker build passed"
fi
fi

echo ""
echo "==================================="
echo "5. Security Scan (Optional)"
echo "==================================="

if command -v trivy &> /dev/null; then
    echo "Running Trivy security scan on frontend..."
    trivy fs --severity HIGH,CRITICAL frontend/
    print_status $? "Frontend security scan passed"
    
    echo "Running Trivy security scan on backend..."
    trivy fs --severity HIGH,CRITICAL offer_collector/
    print_status $? "Backend security scan passed"
else
    print_warning "Trivy not installed. Skipping security scan."
    echo "Install with: curl -sfL https://raw.githubusercontent.com/aquasecurity/trivy/main/contrib/install.sh | sh -s -- -b /usr/local/bin"
fi

echo ""
echo "==================================="
echo "6. E2E Tests"
echo "==================================="

if [ "$RUN_E2E" = true ]; then
    cd "$PROJECT_ROOT"
    echo "Starting Docker services..."
    docker compose up -d
    
    echo "Waiting for services to be ready..."
    sleep 10
    
    cd "$PROJECT_ROOT/frontend"
    echo "Running Playwright E2E tests..."
    npm run test:e2e
    E2E_RESULT=$?
    
    cd "$PROJECT_ROOT"
    echo "Stopping Docker services..."
    docker compose down
    
    if [ $E2E_RESULT -ne 0 ]; then
        print_status 1 "E2E tests failed"
        CRITICAL_FAILURE=true
    else
        print_status 0 "E2E tests passed"
    fi
else
    print_warning "E2E tests skipped (use --run-e2e to enable)"
fi

echo ""
echo "==================================="

if [ "$CRITICAL_FAILURE" = true ]; then
    echo -e "${RED}❌ CI checks completed with failures${NC}"
    echo ""
    echo "Fix the issues above before pushing to repository."
    exit 1
else
    echo -e "${GREEN}✅ All CI checks passed!${NC}"
    echo "==================================="
    echo ""
    echo -e "${GREEN}Ready to push to repository! 🚀${NC}"
    echo ""
    echo "Next steps:"
    echo "  git add ."
    echo "  git commit -m 'your commit message'"
    echo "  git push"
    exit 0
fi
