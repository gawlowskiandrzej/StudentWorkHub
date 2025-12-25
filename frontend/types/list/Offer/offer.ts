import { category } from "./category"
import { company } from "./company"
import { dates } from "./dates"
import { employment } from "./employment"
import { requirements } from "./requirements"
import { salary } from "./salary"
import { location } from "./location"


export interface offer {
    id: number
    source: string
    url: string
    jobTitle: string
    company: company
    description: string | null
    salary: salary
    location: location
    category: category
    requirements: requirements
    employment: employment
    dates: dates
    benefits: string[] | null
    isUrgent: boolean
    isForUkrainians: boolean
}