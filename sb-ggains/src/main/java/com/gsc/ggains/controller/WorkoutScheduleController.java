package com.gsc.ggains.controller;

import com.gsc.ggains.entity.WorkoutSchedule;
import com.gsc.ggains.entity.api.ApiResponse;
import com.gsc.ggains.service.WorkoutScheduleService;
import com.gsc.ggains.util.ResponseUtil;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/api/workoutdays")
@AllArgsConstructor
@Slf4j
public class WorkoutScheduleController {
    private WorkoutScheduleService workoutScheduleService;

    @GetMapping("/{id}")
    public ResponseEntity<ApiResponse<WorkoutSchedule>> getWorkoutDaysById(@PathVariable UUID id) {
        log.info("Fetching workout days by id: {}", id);
        var wd = workoutScheduleService.getWorkoutDaysById(id);
        if (wd != null) {
            log.info("Workout days found for id: {}", wd.getId());
            return ResponseEntity.ok(ResponseUtil.success("Workout days fetched successfully", wd));
        } else {
            log.warn("Workout days not found with id: {}", id);
            return ResponseEntity.ok(ResponseUtil.error("Workout days not found"));
        }
    }

    @PutMapping("/{wdId}")
    public ResponseEntity<ApiResponse<WorkoutSchedule>> updateWorkoutDays(@PathVariable UUID wdId, @RequestBody WorkoutSchedule workoutSchedule) {
        log.info("Updating workout days for wd id: {}", wdId);
        var updatedWorkoutDays = workoutScheduleService.updateWorkoutDays(wdId, workoutSchedule);
        if (updatedWorkoutDays != null) {
            log.info("Workout days updated successfully");
            return ResponseEntity.ok(ResponseUtil.success("Workout days updated successfully", updatedWorkoutDays));
        } else {
            log.warn("Workout days not found with id: {}", wdId);
            return ResponseEntity.ok(ResponseUtil.error("Workout days not found"));
        }
    }
}
