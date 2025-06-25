"use client";
import { useState } from "react";
import {
	NavigationMenu,
	NavigationMenuItem,
	NavigationMenuList,
} from "@/components/ui/navigation-menu";
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetTrigger } from "@/components/ui/sheet";

import { Button, buttonVariants } from "../ui/button";
import { Menu } from "lucide-react";
import ThemeSelector from "../general/ThemeSelector";
import LoginForm from "./LoginForm";

interface RouteProps {
	href: string;
	label: string;
}

const routeList: RouteProps[] = [
	{
		href: "#features",
		label: "Features",
	},
	{
		href: "#testimonials",
		label: "Testimonials",
	},
	{
		href: "#pricing",
		label: "Pricing",
	},
	{
		href: "#faq",
		label: "FAQ",
	},
];

export default function LandingNavbar() {
	const [isSheetOpen, setIsSheetOpen] = useState<boolean>(false);
	const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
	return (
		<header className="sticky border-b-[1px] top-0 z-40 w-full bg-white dark:border-b-slate-700 dark:bg-background">
			<NavigationMenu className="mx-auto">
				<NavigationMenuList className="container h-14 px-4 w-screen flex justify-between ">
					<NavigationMenuItem className="font-bold flex">
						<a
							rel="noreferrer noopener"
							href="/"
							className="ml-2 font-bold text-xl flex">
							Mendala
						</a>
					</NavigationMenuItem>

					{/* mobile */}
					<span className="flex md:hidden">
						<Sheet open={isSheetOpen} onOpenChange={setIsSheetOpen}>
							<SheetTrigger className="px-2">
								<Menu className="flex md:hidden h-5 w-5"></Menu>
							</SheetTrigger>

							<SheetContent side={"left"}>
								<SheetHeader>
									<SheetTitle className="font-bold text-xl">Mendala</SheetTitle>
								</SheetHeader>
								<nav className="flex flex-col justify-center items-center gap-2 mt-4">
									{routeList.map(({ href, label }: RouteProps) => (
										<a
											rel="noreferrer noopener"
											key={label}
											href={href}
											onClick={() => setIsSheetOpen(false)}
											className={buttonVariants({ variant: "ghost" })}>
											{label}
										</a>
									))}

									<Button className="w-[110px] border" variant={"secondary"}>
										Join
									</Button>
								</nav>
							</SheetContent>
						</Sheet>
					</span>

					{/* desktop */}
					<nav className="hidden md:flex gap-2">
						{routeList.map((route: RouteProps, i) => (
							<a
								rel="noreferrer noopener"
								href={route.href}
								key={i}
								className={`text-[17px] ${buttonVariants({
									variant: "ghost",
								})}`}>
								{route.label}
							</a>
						))}
					</nav>

					<div className="hidden md:flex gap-2">
						<Button className="border" variant={"secondary"} onClick={() => setIsModalOpen(true)}>
							Join
						</Button>
					</div>
					<ThemeSelector />
				</NavigationMenuList>
			</NavigationMenu>
			<LoginForm open={isModalOpen} setOpen={setIsModalOpen} />
		</header>
	);
}
