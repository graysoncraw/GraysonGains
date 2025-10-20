package com.gsc.ggains.service;

import com.gsc.ggains.entity.WorkoutSchedule;
import com.gsc.ggains.repository.WorkoutScheduleRepository;
import lombok.AllArgsConstructor;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
@AllArgsConstructor
public class WorkoutScheduleService {

    private WorkoutScheduleRepository workoutScheduleRepository;

    public WorkoutSchedule getWorkoutDaysById(UUID id) {
        return workoutScheduleRepository.findById(id).orElse(null);
    }

    public WorkoutSchedule updateWorkoutDays(UUID wdId, WorkoutSchedule newWorkoutSchedule) {
        var existingWorkoutSchedule = getWorkoutDaysById(wdId);
        if (existingWorkoutSchedule != null) {
            BeanUtils.copyProperties(newWorkoutSchedule, existingWorkoutSchedule, "id", "userId");
            return workoutScheduleRepository.save(existingWorkoutSchedule);
        }
        return null;
    }
}
