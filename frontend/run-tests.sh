#!/bin/bash

# Test runner script for frontend
# Usage: ./run-tests.sh [unit|e2e|all|watch]

set -e

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

print_header() {
    echo -e "${GREEN}================================${NC}"
    echo -e "${GREEN}$1${NC}"
    echo -e "${GREEN}================================${NC}"
}

print_error() {
    echo -e "${RED}❌ $1${NC}"
}

print_success() {
    echo -e "${GREEN}✅ $1${NC}"
}

print_info() {
    echo -e "${YELLOW}ℹ️  $1${NC}"
}

# Check if node_modules exists
if [ ! -d "node_modules" ]; then
    print_error "node_modules not found. Running npm install..."
    npm install
fi

# Parse command line arguments
TEST_TYPE=${1:-all}

case $TEST_TYPE in
    unit)
        print_header "Running Unit Tests"
        npm run test:unit
        print_success "Unit tests completed!"
        ;;
    
    watch)
        print_header "Running Tests in Watch Mode"
        npm run test:watch
        ;;
    
    e2e)
        print_header "Running E2E Tests"
        
        # Check if dev server is running
        if ! lsof -Pi :4000 -sTCP:LISTEN -t >/dev/null ; then
            print_info "Dev server not running. Starting it..."
            npm run dev &
            DEV_PID=$!
            
            # Wait for server to start
            print_info "Waiting for server to start..."
            sleep 10
            
            # Run E2E tests
            npm run test:e2e
            
            # Kill dev server
            kill $DEV_PID
        else
            print_info "Dev server already running on port 4000"
            npm run test:e2e
        fi
        
        print_success "E2E tests completed!"
        ;;
    
    all)
        print_header "Running All Tests"
        
        # Run unit tests
        print_info "Step 1/2: Unit tests"
        npm run test:unit
        print_success "Unit tests passed!"
        
        # Run E2E tests
        print_info "Step 2/2: E2E tests"
        
        if ! lsof -Pi :4000 -sTCP:LISTEN -t >/dev/null ; then
            print_info "Starting dev server..."
            npm run dev &
            DEV_PID=$!
            sleep 10
            
            npm run test:e2e
            kill $DEV_PID
        else
            npm run test:e2e
        fi
        
        print_success "All tests passed!"
        ;;
    
    coverage)
        print_header "Generating Coverage Report"
        npm run test:unit
        
        # Open coverage report in browser
        if [ -f "coverage/index.html" ]; then
            print_info "Opening coverage report..."
            if command -v xdg-open > /dev/null; then
                xdg-open coverage/index.html
            elif command -v open > /dev/null; then
                open coverage/index.html
            fi
        fi
        ;;
    
    ranking)
        print_header "Running Ranking Algorithm Tests Only"
        npm test -- ranking
        print_success "Ranking algorithm tests completed!"
        ;;
    
    *)
        print_error "Unknown test type: $TEST_TYPE"
        echo ""
        echo "Usage: ./run-tests.sh [unit|e2e|all|watch|coverage|ranking]"
        echo ""
        echo "Options:"
        echo "  unit      - Run unit tests only"
        echo "  e2e       - Run E2E tests only"
        echo "  all       - Run all tests (default)"
        echo "  watch     - Run tests in watch mode"
        echo "  coverage  - Generate and open coverage report"
        echo "  ranking   - Run only ranking algorithm tests"
        exit 1
        ;;
esac

print_success "Test run completed successfully!"
