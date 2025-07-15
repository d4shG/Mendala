export interface LoginFormProps extends React.ComponentProps<"div"> {
  open: boolean;
  setOpen: (open: boolean) => void;
}

export interface RouteProps {
	href: string;
	label: string;
}

export interface statsProps {
    quantity: string;
    description: string;
  }

export interface FeatureProps {
  title: string;
  description: string;
  image: string;
}

export enum PopularPlanType {
  NO = 0,
  YES = 1,
}

export interface PricingProps {
  title: string;
  popular: PopularPlanType;
  price: number;
  description: string;
  buttonText: string;
  benefitList: string[];
}

export interface FAQProps {
  question: string;
  answer: string;
  value: string;
}

export interface LoginModalContextType {
  isModalOpen: boolean;
  setIsModalOpen: (open: boolean) => void;
}