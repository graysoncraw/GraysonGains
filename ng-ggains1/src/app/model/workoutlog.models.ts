import { UUID } from "node:crypto";
import { User } from "./user.models";

export interface WorkoutLog {
    id: UUID;
    user: User;
    exerciseName: string;
    exerciseWeight: number;
    exerciseReps: number;
    exerciseDate: Date;
}
