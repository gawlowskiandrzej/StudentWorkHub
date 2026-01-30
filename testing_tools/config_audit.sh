#!/bin/bash

# Configuration Audit for StudentWorkHub
# This script identifies common security flaws and misconfigurations in the orchestration layer.

RED='\033[0;31m'
GREEN='\033[0;32m'
NC='\033[0m' # No Color

echo "--- Starting Security Configuration Audit ---"
FAILED=0

# 1. Check for hardcoded Gemini API Key
echo -n "[1/5] Checking for hardcoded GEMINI_KEY... "
if grep -q "GEMINI_KEY=[a-zA-Z0-9]" docker-compose.yml; then
    echo -e "${RED}FAILED${NC} (Hardcoded key found in docker-compose.yml)"
    FAILED=$((FAILED + 1))
else
    echo -e "${GREEN}PASSED${NC}"
fi

# 2. Check for public port exposure for Databases
echo -n "[2/5] Checking for public DB exposure (0.0.0.0)... "
if grep -P "5432:5432|6379:6379" docker-compose.yml | grep -q "0.0.0.0" || grep -P "5432:5432|6379:6379" docker-compose.yml && ! grep -q "127.0.0.1:" docker-compose.yml; then
    echo -e "${RED}WARNING${NC} (Postgres/Redis exposed on all interfaces)"
    # FAILED=$((FAILED + 1)) # Warning only for dev configs
else
    echo -e "${GREEN}PASSED${NC}"
fi

# 3. Check for root user in Dockerfiles
echo -n "[3/5] Checking for USER directive in Dockerfiles... "
ROOT_CONTAINERS=$(find . -name "Dockerfile*" -exec grep -L "USER " {} \;)
if [ ! -z "$ROOT_CONTAINERS" ]; then
    echo -e "${RED}FAILED${NC} (Containers running as root: $(echo $ROOT_CONTAINERS | wc -w))"
    FAILED=$((FAILED + 1))
else
    echo -e "${GREEN}PASSED${NC}"
fi

# 4. Check for Hardcoded Passwords
echo -n "[4/5] Checking for hardcoded POSTGRES_PASSWORD... "
if grep -q "POSTGRES_PASSWORD=password" docker-compose.yml; then
    echo -e "${RED}FAILED${NC} (Default/Hardcoded password found)"
    FAILED=$((FAILED + 1))
else
    echo -e "${GREEN}PASSED${NC}"
fi

# 5. Check for Healthchecks
echo -n "[5/5] Checking for missing healthchecks... "
if ! grep -q "healthcheck:" docker-compose.yml; then
    echo -e "${RED}FAILED${NC} (No healthchecks defined in docker-compose.yml)"
    FAILED=$((FAILED + 1))
else
    echo -e "${GREEN}PASSED${NC}"
fi

echo "--- Audit Finished ---"
if [ $FAILED -gt 0 ]; then
    echo -e "${RED}Audit failed with $FAILED issues.${NC}"
    exit 1
else
    echo -e "${GREEN}Audit passed successfully.${NC}"
    exit 0
fi
