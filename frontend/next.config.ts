import type { NextConfig } from "next";

const nextConfig: NextConfig = {
    async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: "http://offer_manager:5059/api/:path*",
      },
    ];
  },
};

export default nextConfig;
