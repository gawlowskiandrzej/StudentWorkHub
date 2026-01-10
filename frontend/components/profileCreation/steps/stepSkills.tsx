"use client";

import { useState } from "react";
import { Plus, X } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { StepProps } from "@/constants/profileCreation/steps";
import { useTranslation } from "react-i18next";
import styles from '@/styles/ProfileCreationStyle.module.css';
import { FloatingLabelInput } from "@/components/ui/floatingInput";

interface Skill {
  id: string;
  name: string;
  months: string;
}

export default function StepSkills({ data, updateData }: StepProps) {
  const { t } = useTranslation("profileCreation");

  const [name, setName] = useState("");
  const [months, setMonths] = useState("");

  const currentSkills = data.skills || [];

  const handleAddSkill = () => {
    if (name && months && currentSkills.length < 4) {
      const newSkill = { id: crypto.randomUUID(), name, months };
      updateData("skills", [...currentSkills, newSkill]);
      setName("");
      setMonths("");
    }
  };

  const handleRemove = (id: string) => {
    const filtered = currentSkills.filter((s: Skill) => s.id !== id);
    updateData("skills", filtered);
  };

  return (
    <div className="flex flex-col items-center w-full max-w-2xl mx-auto space-y-8">
      <div className="text-center space-y-2">
        <h2 className="text-2xl font-semibold text-white">
          {t("skillsQuestion")} ({currentSkills.length}/4)
        </h2>
        <p className="text-sm text-zinc-400">{t("writeYourSkillAndExperience")} <br /> {t("exampleSkillAndExperience")}</p>
      </div>

      <div className="flex flex-col gap-4 w-full px-4 max-w-md mx-auto">
        <div className="flex gap-3">
          <FloatingLabelInput
            label={t("skillNamePlaceholder")}
            value={name}
            onChange={(e) => setName(e.target.value)}
            // className="bg-transparent border-zinc-800 h-12 rounded-lg focus-visible:ring-blue-500 text-white placeholder:text-white"
          />
          <FloatingLabelInput
            label={t("monthsOfExperience")}
            type="number"
            value={months}
            onChange={(e) => setMonths(e.target.value)}
            // className="bg-transparent border-zinc-800 h-12 w-24 rounded-lg focus-visible:ring-blue-500 text-white placeholder:text-white"
          />
        </div>

        <Button
          variant="outline"
          onClick={handleAddSkill}
          disabled={currentSkills.length >= 4 || !name || !months}
          className="rounded-full border-zinc-800 hover:bg-zinc-900 text-xs font-light text-white"
        >
          <Plus className="mr-2 h-4 w-4" /> {t("addSkill")}
        </Button>
      </div>

      <div className="flex flex-wrap justify-center gap-4 mt-4 px-4">
        {currentSkills.map((skill: Skill) => (
          <div 
            key={skill.id} 
            className="flex items-center gap-2 px-4 py-2 rounded-full bg-zinc-900 border border-zinc-800"
          >
            <span className="text-blue-500 font-medium text-sm">{skill.name}</span>
            <span className="text-zinc-400 text-sm">{skill.months}m</span>
            <button
              onClick={() => handleRemove(skill.id)}
              className="ml-1 p-0.5 hover:bg-zinc-800 rounded-full transition-colors"
            >
              <X className="h-3 w-3 text-red-500" />
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}