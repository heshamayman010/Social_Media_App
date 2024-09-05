import { Photo } from "./Photo"

export interface Member {
  id: number
  userName: string
  age: number
  photoUrl: string
  knownAs: string
  created: Date
  lAstActiv: string
  gender: string
  introduction: string
  interstes: any
  lookingFor: string
  city: string
  country: string
  Photos:Photo[]
}
