import StepEarnings from "@/components/feature/profileCreation/steps/stepEarnings";
import StepJobStatus from "@/components/feature/profileCreation/steps/stepJobStatus";
import StepLanguages from "@/components/feature/profileCreation/steps/stepLanguages";
import StepLocation from "@/components/feature/profileCreation/steps/stepLocation";
import StepMajor from "@/components/feature/profileCreation/steps/stepMajor";
import StepReview from "@/components/feature/profileCreation/steps/stepReview";
import StepSkills from "@/components/feature/profileCreation/steps/stepSkills";

export interface StepProps {
  data: any;
  updateData: (key: string, value: any) => void;
}

export const STEPS = [
  { id: "major", component: StepMajor },
  { id: "skills", component: StepSkills },
  { id: "earnings", component: StepEarnings },
  { id: "job-status", component: StepJobStatus},
  { id: "languages", component: StepLanguages},
  { id: "location", component: StepLocation},
  { id: "review", component: StepReview },
] as const;