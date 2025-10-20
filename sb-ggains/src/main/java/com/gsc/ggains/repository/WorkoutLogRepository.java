package com.gsc.ggains.repository;

import com.gsc.ggains.entity.WorkoutLog;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.UUID;

@Repository
public interface WorkoutLogRepository extends JpaRepository<WorkoutLog, UUID> {
    List<WorkoutLog> findAllByUserId(UUID id);
}
