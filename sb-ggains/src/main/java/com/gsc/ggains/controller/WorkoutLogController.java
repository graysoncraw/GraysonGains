package com.gsc.ggains.controller;

import com.gsc.ggains.entity.WorkoutLog;
import com.gsc.ggains.entity.api.ApiResponse;
import com.gsc.ggains.service.WorkoutLogService;
import com.gsc.ggains.util.ResponseUtil;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/workoutlog")
@AllArgsConstructor
@Slf4j
public class WorkoutLogController {

    private WorkoutLogService workoutLogService;

    @PostMapping("/add")
    public ResponseEntity<ApiResponse<WorkoutLog>> addWorkout(@RequestBody WorkoutLog workoutLog) {
        log.info("Received request to add workout log");
        return ResponseEntity.ok(ResponseUtil.success("Workout added successfully", workoutLogService.addWorkoutLog(workoutLog)));
    }

    @GetMapping("/{id}")
    public ResponseEntity<ApiResponse<List<WorkoutLog>>> getWorkoutsByUserId(@PathVariable UUID id) {
        log.info("Fetching workout logs for user id: {}", id);
        var workouts = workoutLogService.getWorkoutLogs(id);
        if (!workouts.isEmpty()) {
            log.info("Workout logs found");
            return ResponseEntity.ok(ResponseUtil.success("Workout logs fetched successfully", workouts));
        } else {
            log.warn("No workout logs found for user id: {}", id);
            return ResponseEntity.ok(ResponseUtil.error("No workout logs found"));
        }
    }

    @PutMapping("/{wlId}")
    public ResponseEntity<ApiResponse<WorkoutLog>> updateWorkoutLog(@PathVariable UUID wlId, @RequestBody WorkoutLog workoutLog) {
        log.info("Received request to update workout log with id: {}", wlId);
        var updatedWorkout = workoutLogService.updateWorkoutLog(wlId, workoutLog);
        if (updatedWorkout != null) {
            log.info("Workout log updated successfully");
            return ResponseEntity.ok(ResponseUtil.success("Workout log updated successfully", updatedWorkout));
        } else {
            log.warn("Workout log with id: {} not found", wlId);
            return ResponseEntity.ok(ResponseUtil.error("Workout log not found"));
        }
    }

    @DeleteMapping("/{wlId}")
    public ResponseEntity<ApiResponse<WorkoutLog>> deleteWorkoutLog(@PathVariable UUID wlId){
        log.info("Received request to delete workout log with id: {}", wlId);
        var deletedWorkout = workoutLogService.deleteWorkoutLog(wlId);
        if (deletedWorkout != null) {
            log.info("Workout log deleted successfully");
            return ResponseEntity.ok(ResponseUtil.success("Workout log deleted successfully", deletedWorkout));
        } else {
            log.warn("Workout log with id: {} not found", wlId);
            return ResponseEntity.ok(ResponseUtil.error("Workout log not found"));
        }
    }
}
