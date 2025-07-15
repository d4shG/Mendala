import Statistics from "./Statistics";
import Image from "next/image";

export default function About() {
  return (
    <section
      id="about"
      className="container mx-auto px-3 py-24 sm:py-32"
    >
      <div className="bg-muted/50 border rounded-lg py-12">
        <div className="px-6 flex flex-col-reverse md:flex-row gap-8 md:gap-12">
          <Image
            src="/about.svg"
            alt="about-pic"
            className="w-[300px] object-contain rounded-lg"
            width={300} height={300}
          />
          <div className="bg-green-0 flex flex-col justify-between">
            <div className="pb-6">
              <h2 className="text-3xl md:text-4xl font-bold">
                <span className="bg-gradient-accent text-transparent bg-clip-text">
                  About{" "}
                </span>
                our goal
              </h2>
              <p className="text-xl text-muted-foreground mt-4">
                At Mendala, we’re building the ERP module we always wished existed — simple, smart, and made for the real day-to-day of repair teams. No more clunky systems, endless tabs, or chasing info across tools. With Mendala, everything from issue tracking to invoicing lives in one clean, intuitive place. Whether you're fixing phones, laptops, or complex machinery, Mendala helps you stay organized, save time, and focus on what actually matters: getting things done.
              </p>
            </div>

            <Statistics />
          </div>
        </div>
      </div>
    </section>
  );
};