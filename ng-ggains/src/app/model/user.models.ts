import { UUID } from "node:crypto";

export interface User {
    id: UUID;
    firstName: string;
    lastName: string;
    username: string;
    passwordHash: string;
    email: string;
    gender: string;
    heightFt: number;
    heightIn: number;
    weight: number;
    dateCreated: Date;
    personalRecords: PersonalRecord;
    workoutSchedule: WorkoutSchedule;
    workoutLogs: WorkoutLog[];
}

interface PersonalRecord {
    id: UUID;
    benchPressPR: number;
    squatPR: number;
    deadliftPR: number;
    shoulderPressPR: number;
}

interface WorkoutSchedule {
    id: UUID;
    benchDay: string;
    squatDay: string;
    deadliftDay: string;
    shoulderPressDay: string;
    cycleStartDate: Date;
}

interface WorkoutLog {
    id: UUID;
    exerciseName: string;
    exerciseWeight: number;
    exerciseReps: number;
    exerciseDate: Date;
}
