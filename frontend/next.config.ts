import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  experimental: {
    // Force turbopack to use this directory as root to fix tailwindcss resolution issues during E2E tests
    turbo: {
      root: __dirname,
    } as any,
  },
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: `${process.env.BACKEND_URL || "http://localhost:5059"}/api/:path*`,
      },
    ];
  },
};

export default nextConfig;
