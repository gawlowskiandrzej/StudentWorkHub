# Stage 1: Builder
FROM node:20-alpine AS builder
WORKDIR /app

# Kopiujemy tylko package.json i package-lock.json (lub yarn.lock)
COPY package*.json ./

# Instalujemy wszystkie zależności (dev + prod)
RUN npm ci

# Kopiujemy resztę projektu
COPY . .

# Budujemy Next.js (generuje folder .next)
RUN npm run build

# Stage 2: Runner (produkcja)
FROM node:20-alpine AS runner
WORKDIR /app

# Instalujemy tylko dependencies produkcyjne
COPY package*.json ./
RUN npm ci --omit=dev

# Kopiujemy build z etapu builder
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public
COPY --from=builder /app/next.config.ts ./next.config.ts
COPY --from=builder /app/package.json ./package.json

# Ustawiamy port Next.js
EXPOSE 5000

# Uruchamiamy aplikację w trybie produkcyjnym
CMD ["npm", "start"]
