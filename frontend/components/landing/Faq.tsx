import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import { FAQProps } from "@/types/landing_types";

const FAQList: FAQProps[] = [
  {
    question: "Is Mendala really free to start?",
    answer:
      "Yes! You can start using Mendala with our free tier, perfect for small teams or solo technicians.",
    value: "item-1",
  },
  {
    question: "What kind of repairs does Mendala support?",
    answer:
      "Mendala is designed to handle all types of repair workflows — from electronics and appliances to IT hardware and beyond. You can customize issue types to fit your business.",
    value: "item-2",
  },
  {
    question: "Can I track issue history and updates?",
    answer:
      "Absolutely. Every issue has a detailed history log, so you can track status changes, comments, and assignments over time.",
    value: "item-3",
  },
  {
    question: "Does Mendala work for teams?",
    answer:
      "Yes, Mendala supports multi-user workflows, role-based access, and team collaboration — so everyone stays in sync.",
    value: "item-4",
  },
  {
    question: "Can I integrate Mendala with my existing ERP or invoicing system?",
    answer:
      "Mendala is built to be modular and integration-ready. API and data export features make it easy to connect with your existing systems.",
    value: "item-5",
  },
];


export default function FAQ() {
  return (
    <section
      id="faq"
      className="container px-3 py-24 sm:py-32 mx-auto"
    >
      <h2 className="text-3xl md:text-4xl font-bold mb-4">
        Frequently Asked{" "}
        <span className="bg-gradient-accent text-transparent bg-clip-text">
          Questions
        </span>
      </h2>

      <Accordion
        type="single"
        collapsible
        className="w-full AccordionRoot"
      >
        {FAQList.map(({ question, answer, value }: FAQProps) => (
          <AccordionItem
            key={value}
            value={value}
          >
            <AccordionTrigger className="text-left">
              {question}
            </AccordionTrigger>

            <AccordionContent>{answer}</AccordionContent>
          </AccordionItem>
        ))}
      </Accordion>

      <h3 className="font-medium mt-4">
        Still have questions?{" "}
        <a
          rel="noreferrer noopener"
          href="#"
          className="text-primary transition-all border-primary hover:border-b-2"
        >
          Contact us
        </a>
      </h3>
    </section>
  );
};