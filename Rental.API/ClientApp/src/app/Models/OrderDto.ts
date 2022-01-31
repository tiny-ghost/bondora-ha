import { RentalItem } from "./RentalItem";

export interface OrderDto {
  CustomerId: number,
  RentalItems: RentalItem[]
}
