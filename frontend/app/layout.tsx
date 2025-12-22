import type { Metadata } from "next";
import { Geist, Geist_Mono, Roboto } from "next/font/google";
import "./globals.css";
import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer";
import { SearchProvider } from "@/context/SearchContext";

const roboto = Roboto({
  weight: '400',
  subsets: ['latin'],
})

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "StudentWorkHub",
  description: "Find your perfect job as a student",
};

export default function RootLayout({ children }: { children: React.ReactNode }){
  return (
    <html lang="pl" className={roboto.className}>
      <body>
        <Navbar />
        <SearchProvider>
          <main className="min-h-screen">{children}</main>
        </SearchProvider>
        <Footer />
      </body>
    </html>
  );
}
