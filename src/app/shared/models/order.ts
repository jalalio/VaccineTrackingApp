import { User } from "./user";
import { Vaccine } from "./vaccines";

export interface Order {
    orderId: string,
    quantity: number,
    date: string,
    status: string,
    user: User,
    vaccine: Vaccine
}