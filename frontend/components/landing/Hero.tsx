"use client"
import { Button } from "../ui/button";
import Image from "next/image";
import { useLoginModal } from "@/context/LoginModalContext";

export default function Hero() {
	const { setIsModalOpen } = useLoginModal();
	return (
		<section className="container px-3 mx-auto grid items-center lg:grid-cols-2 place-items-center py-20 md:py-32 gap-10">
			<div className="text-center lg:text-start space-y-6">
				<main className="text-5xl md:text-6xl font-bold">
					<h1 className="inline">
						<span className="inline bg-gradient-accent text-transparent bg-clip-text">
							Mendala
						</span>{" "}
						ERP module
					</h1>{" "}
					for{" "}
					<h2 className="inline">
						repair shops
					</h2>
				</main>

				<p className="text-xl text-muted-foreground md:w-10/12 mx-auto lg:mx-0">
					Whether you're managing tickets, customers, or invoices — Mendala helps your repair shop stay organized, efficient, and ready for anything.
				</p>

				<div className="space-y-4 md:space-y-0 md:space-x-4">
					<Button className="w-full md:w-1/3" onClick={() => setIsModalOpen(true)}>Try it for Free</Button>
				</div>
			</div>

			<div className="relative w-full h-full flex items-center justify-center">
				<div className="shadow" aria-hidden="true"></div>
				<Image aria-hidden src="/hero.svg" alt="hero pic" width={750} height={750} />
			</div>
		</section>
	);
}
