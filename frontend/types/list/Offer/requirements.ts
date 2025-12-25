import { language } from "./language"
import { skill } from "./skill"

export interface requirements {
  skills: skill[] | null
  education: string[] | null
  languages: language[] | null
}