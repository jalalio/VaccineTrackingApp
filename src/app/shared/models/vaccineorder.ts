import { Order } from "./order";
import { Vaccine } from "./vaccines";

export interface VaccineOrder {
    orderId: string,
    order: Order,
    vaccineId: string,
    vaccine: Vaccine
}