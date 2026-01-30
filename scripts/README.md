# 🚀 Quick Start Guide - Local CI Testing

## Podstawowe użycie

```bash
# Uruchom wszystkie testy (bez E2E)
./scripts/test-ci-locally.sh

# Pokaż pomoc
./scripts/test-ci-locally.sh --help
```

## Opcje

| Opcja | Opis |
|-------|------|
| `--skip-lint` | Pomiń linting i sprawdzanie typów (ESLint, TypeScript) |
| `--skip-tests` | Pomiń testy jednostkowe (Vitest) |
| `--skip-backend` | Pomiń build i testy backendu (.NET) |
| `--skip-docker` | Pomiń budowanie obrazów Docker |
| `--run-e2e` | Uruchom testy E2E (wymaga Docker) |
| `--help` / `-h` | Pokaż pomoc |

## Przykłady

```bash
# Szybki test - tylko testy jednostkowe
./scripts/test-ci-locally.sh --skip-lint --skip-backend --skip-docker

# Test przed pushem - wszystko oprócz E2E
./scripts/test-ci-locally.sh

# Pełny test z E2E
./scripts/test-ci-locally.sh --run-e2e

# Tylko backend
./scripts/test-ci-locally.sh --skip-lint --skip-tests --skip-docker
```

## Co robi skrypt?

1. **Linting & Type Check** (frontend)
   - ESLint - sprawdza jakość kodu
   - TypeScript - sprawdza typy

2. **Unit Tests** (frontend)
   - Vitest - 500+ testów jednostkowych
   - Oczekiwane: 508 passed, 14 failed (bug exposure tests)

3. **Backend Build & Tests**
   - Kompilacja .NET
   - Uruchomienie testów .NET

4. **Docker Build**
   - Buduje obrazy Docker dla frontend i backend
   - Weryfikuje czy Dockerfile są poprawne

5. **E2E Tests** (opcjonalne)
   - Uruchamia Docker Compose
   - Playwright E2E tests
   - Zatrzymuje kontenery po testach

## Notatki

- **Linting errors** są oznaczone jako **non-blocking** - skrypt kontynuuje nawet jeśli są błędy ESLint
- **14 failed tests** w testach jednostkowych to **celowe "bug exposure tests"** - nie blokują CI
- **E2E tests** są domyślnie wyłączone (długie) - włącz z `--run-e2e`
- Skrypt automatycznie wraca do katalogu projektu i używa poprawnych ścieżek

## Troubleshooting

### Błąd: `cd: frontend: No such file or directory`
Uruchom skrypt z dowolnego miejsca - automatycznie znajdzie projekt root.

### npm/dotnet not found
Upewnij się, że masz zainstalowane:
- Node.js 20.x
- .NET 8.0
- Docker & Docker Compose (dla E2E i Docker builds)

### Testy timeout
Zwiększ timeout: `timeout 600 ./scripts/test-ci-locally.sh`
