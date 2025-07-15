"use client"
import { Button } from "../ui/button";
import { Input } from "../ui/input";

export default function Newsletter () {
  const handleSubmit = (e: any) => {
    e.preventDefault();
    console.log("Subscribed!");
  };

  return (
    <section id="newsletter" className="container mx-auto px-3">
      <hr className="w-11/12 mx-auto bg-accent-muted" />

      <div className="container py-24 sm:py-32">
        <h3 className="text-center text-4xl md:text-5xl font-bold">
          Let’s Keep Your Repairs Running{" "}
          <span className="bg-gradient-accent text-transparent bg-clip-text">
            Smoother
          </span>
        </h3>
        <p className="text-xl text-muted-foreground text-center mt-4 mb-8">
          Get repair management tips, product updates, and practical insights — straight to your inbox.
        </p>

        <form
          className="flex flex-col w-full md:flex-row md:w-6/12 lg:w-4/12 mx-auto gap-4 md:gap-2"
          onSubmit={handleSubmit}
        >
          <Input
            placeholder="name@example.com"
            className="bg-muted/50 dark:bg-muted/80"
            aria-label="email"
          />
          <Button>Yes, I’m In!</Button>
        </form>
      </div>

      <hr className="w-11/12 mx-auto bg-accent-muted" />
    </section>
  );
};