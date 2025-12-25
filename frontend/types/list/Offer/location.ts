import { coordinates } from "./cordinates"

export interface location {
  buildingNumber: string | null
  street: string | null
  city: string | null
  postalCode: string | null
  coordinates: coordinates | null
  isRemote: boolean | null
  isHybrid: boolean | null
}