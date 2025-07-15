import LandingNavbar from "@/components/landing/LandingNavbar";
import Hero from "@/components/landing/Hero";
import About from "@/components/landing/About";
import Features from "@/components/landing/Features";
import Newsletter from "@/components/landing/Newsletter";
import Pricing from "@/components/landing/Pricing";
import FAQ from "@/components/landing/Faq";
import Footer from "@/components/landing/Footer";
import LoginForm from "@/components/landing/LoginForm";

export default function Home() {
	return (
		<>
			<LandingNavbar />
			<Hero />
			<About />
			<Features />
			<Newsletter />
			<Pricing />
			<FAQ />
			<Footer />
			<LoginForm />
		</>
	);
}
