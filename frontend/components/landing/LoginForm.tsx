"use client";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { LoginFormProps } from "@/types/landing_types";
import { useLoginModal } from "@/context/LoginModalContext";
import { X } from "lucide-react";

export default function LoginForm({ className, ...props }: LoginFormProps) {
	const { isModalOpen, setIsModalOpen } = useLoginModal();
	return (
		<div
			id="login-modal"
			aria-hidden="true"
			className={`${
				isModalOpen ? "" : "hidden "
			}fixed inset-0 flex min-h-svh w-full items-center justify-center p-6 md:p-10 overflow-hidden bg-background/90 backdrop-blur-md z-50`}>
			<div className="w-full max-w-sm">
				<div className={cn("flex flex-col gap-6", className)} {...props}>
					<div className="relative">
						<Button
							aria-label="Close login modal"
							onClick={() => setIsModalOpen(false)}
							className="absolute top-8 right-1.5"
							variant={"ghost"}>
							<X className="h-5 w-5" />
						</Button>
					</div>
					<Card>
						<CardHeader>
							<CardTitle>Login to your account</CardTitle>
							<CardDescription>
								Enter your email below to login to your account
							</CardDescription>
						</CardHeader>
						<CardContent>
							<form>
								<div className="flex flex-col gap-6">
									<div className="grid gap-3">
										<Label htmlFor="email">Email</Label>
										<Input
											id="email"
											type="email"
											className="bg-muted/50 dark:bg-muted/80"
											placeholder="name@example.com"
											required
										/>
									</div>
									<div className="grid gap-3">
										<div className="flex items-center">
											<Label htmlFor="password">Password</Label>
											<a
												href="#"
												className="ml-auto inline-block text-sm underline-offset-4 hover:underline">
												Forgot your password?
											</a>
										</div>
										<Input
											id="password"
											type="password"
											required
											className="bg-muted/50 dark:bg-muted/80"
											placeholder="P@ssW0rd!"
										/>
									</div>
									<div className="flex flex-col gap-3">
										<Button type="submit" className="w-full">
											Login
										</Button>
									</div>
								</div>
								<div className="mt-4 text-center text-sm">
									Don&apos;t have an account?{" "}
									<a href="#" className="underline underline-offset-4">
										Sign up
									</a>
								</div>
							</form>
						</CardContent>
					</Card>
				</div>
			</div>
		</div>
	);
}
