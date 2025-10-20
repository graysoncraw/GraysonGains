package com.gsc.ggains.service;

import com.gsc.ggains.entity.WorkoutLog;
import com.gsc.ggains.repository.WorkoutLogRepository;
import lombok.AllArgsConstructor;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
@AllArgsConstructor
public class WorkoutLogService {

    private WorkoutLogRepository workoutLogRepository;

    public WorkoutLog addWorkoutLog(WorkoutLog workoutLog) {
        return workoutLogRepository.save(workoutLog);
    }

    public List<WorkoutLog> getWorkoutLogs(UUID id) {
        return workoutLogRepository.findAllByUserId(id);
    }

    public WorkoutLog updateWorkoutLog(UUID wlId, WorkoutLog workoutLog) {
        WorkoutLog existingLog = workoutLogRepository.findById(wlId).orElse(null);
        if (existingLog != null) {
            BeanUtils.copyProperties(workoutLog, existingLog, "id", "userId");
            return workoutLogRepository.save(existingLog);
    }
        return null;
    }

    public WorkoutLog deleteWorkoutLog(UUID wlId) {
        WorkoutLog wl = workoutLogRepository.findById(wlId).orElse(null);
        workoutLogRepository.deleteById(wlId);
        return wl;
    }
}
