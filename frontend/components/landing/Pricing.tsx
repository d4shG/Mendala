import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Check } from "lucide-react";
import { PricingProps, PopularPlanType } from "@/types/landing_types";



const pricingList: PricingProps[] = [
  {
    title: "Starter",
    popular: 0,
    price: 0,
    description: "Ideal for solo technicians or small teams getting started.",
    buttonText: "Get Started",
    benefitList: [
      "1 user",
      "Up to 20 repair tickets / month",
      "Basic issue tracking",
      "Email support",
      "Limited dashboard access",
    ],
  },
  {
    title: "Pro",
    popular: 1,
    price: 19,
    description: "Perfect for growing repair shops that need more power.",
    buttonText: "Start Free Trial",
    benefitList: [
      "Up to 5 users",
      "Unlimited tickets",
      "3rd-party integrations (API)",
      "Status automation",
      "Email + priority support",
    ],
  },
  {
    title: "Enterprise",
    popular: 0,
    price: 49,
    description: "Full control and flexibility for teams that need scale.",
    buttonText: "Contact Us",
    benefitList: [
      "Unlimited users",
      "Unlimited tickets",
      "Advanced analytics & exports",
      "Dedicated success manager",
      "24/7 support"
    ],
  },
];


export default function Pricing() {
  return (
    <section
      id="pricing"
      className="container px-3 py-24 sm:py-32 mx-auto"
    >
      <h2 className="text-3xl md:text-4xl font-bold text-center">
        Get
        <span className="bg-gradient-accent text-transparent bg-clip-text">
          {" "}
          Unlimited{" "}
        </span>
        Access
      </h2>
      <h3 className="text-xl text-center text-muted-foreground pt-4 pb-8">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Alias
        reiciendis.
      </h3>
      <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
        {pricingList.map((pricing: PricingProps) => (
          <Card
            key={pricing.title}
            className={
              pricing.popular === PopularPlanType.YES
                ? "drop-shadow-xl shadow-black/10 dark:shadow-white/15 scale-105"
                : ""
            }
          >
            <CardHeader>
              <CardTitle className="flex item-center justify-between">
                {pricing.title}
                {pricing.popular === PopularPlanType.YES ? (
                  <Badge
                    variant="secondary"
                    className="text-sm text-primary"
                  >
                    Most popular
                  </Badge>
                ) : null}
              </CardTitle>
              <div>
                <span className="text-3xl font-bold">${pricing.price}</span>
                <span className="text-muted-foreground"> /month</span>
              </div>

              <CardDescription>{pricing.description}</CardDescription>
            </CardHeader>

            <CardContent>
              <Button className="w-full">{pricing.buttonText}</Button>
            </CardContent>

            <hr className="w-4/5 m-auto mb-4" />

            <CardFooter className="flex">
              <div className="space-y-4">
                {pricing.benefitList.map((benefit: string) => (
                  <span
                    key={benefit}
                    className="flex"
                  >
                    <Check className="text-accent" />{" "}
                    <h3 className="ml-2">{benefit}</h3>
                  </span>
                ))}
              </div>
            </CardFooter>
          </Card>
        ))}
      </div>
    </section>
  );
};