import StepEarnings from "@/components/profileCreation/steps/stepEarnings";
import StepJobStatus from "@/components/profileCreation/steps/stepJobStatus";
import StepLanguages from "@/components/profileCreation/steps/stepLanguages";
import StepLocation from "@/components/profileCreation/steps/stepLocation";
import StepMajor from "@/components/profileCreation/steps/stepMajor";
import StepReview from "@/components/profileCreation/steps/stepReview";
import StepSkills from "@/components/profileCreation/steps/stepSkills";

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