package com.gsc.ggains.repository;

import com.gsc.ggains.entity.WorkoutSchedule;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface WorkoutScheduleRepository extends JpaRepository<WorkoutSchedule, UUID> {
}
