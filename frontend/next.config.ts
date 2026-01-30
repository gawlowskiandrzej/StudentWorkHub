import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  // Force turbopack to use this directory as root to fix tailwindcss resolution issues during E2E tests
  turbopack: {
    root: __dirname,
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
