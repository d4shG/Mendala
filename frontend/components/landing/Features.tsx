import { Badge } from "../ui/badge";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import Image from "next/image";
import { FeatureProps } from "@/types/landing_types";



const features: FeatureProps[] = [
  {
    title: "Seamless Job Tracking",
    description:
      "Easily log, update, and monitor repair issues from intake to resolution — all in one place.",
    image: "/feature1.svg",
  },
  {
    title: "Clean, Intuitive Interface",
    description:
      "Designed to feel effortless, so your team can stay focused on the job — not fighting the system.",
    image: "/feature2.svg",
  },
  {
    title: "Smart Analytics",
    description:
      "Get real-time insights into team performance, ticket flow, and turnaround times with zero setup.",
    image: "/feature3.svg",
  },
];

const featureList: string[] = [
  "Dark & Light Mode",
  "Real-Time Status Tracking",
  "Role-Based Access",
  "Mobile-Friendly Design",
  "Repair History Logs",
];

export default function Features() {
  return (
    <section
      id="features"
      className="container mx-auto px-3 py-24 sm:py-32 space-y-8"
    >
      <h2 className="text-3xl lg:text-4xl font-bold md:text-center">
        Many{" "}
        <span className="bg-gradient-accent text-transparent bg-clip-text">
          Great Features
        </span>
      </h2>

      <div className="flex flex-wrap md:justify-center gap-4">
        {featureList.map((feature: string) => (
          <div key={feature}>
            <Badge
              variant="secondary"
              className="text-sm"
            >
              {feature}
            </Badge>
          </div>
        ))}
      </div>

      <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
        {features.map(({ title, description, image }: FeatureProps) => (
          <Card key={title}>
            <CardHeader>
              <CardTitle>{title}</CardTitle>
            </CardHeader>

            <CardContent>{description}</CardContent>

            <CardFooter>
              <Image
                src={image}
                alt="About feature"
                className="w-[200px] lg:w-[300px] mx-auto"
                width={300}
                height={300}
              />
            </CardFooter>
          </Card>
        ))}
      </div>
    </section>
  );
};