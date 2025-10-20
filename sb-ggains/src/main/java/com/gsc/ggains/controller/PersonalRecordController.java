package com.gsc.ggains.controller;

import com.gsc.ggains.entity.api.ApiResponse;
import com.gsc.ggains.entity.PersonalRecord;
import com.gsc.ggains.service.PersonalRecordService;
import com.gsc.ggains.util.ResponseUtil;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@RestController
@RequestMapping("/api/pr")
@AllArgsConstructor
@Slf4j
public class PersonalRecordController {

    private PersonalRecordService personalRecordService;

    @GetMapping("/{id}")
    public ResponseEntity<ApiResponse<PersonalRecord>> getPRsById(@PathVariable UUID id) {
        log.info("Fetching personal record by id: {}", id);
        var pr = personalRecordService.getPRsById(id);
        if (pr != null) {
            log.info("Personal record found");
            return ResponseEntity.ok(ResponseUtil.success("Personal record fetched successfully", pr));
        } else {
            log.warn("Personal record not found with id: {}", id);
            return ResponseEntity.ok(ResponseUtil.error("Personal record not found"));
        }
    }

    @PutMapping("/{prId}")
    public ResponseEntity<ApiResponse<PersonalRecord>> updatePRs(@PathVariable UUID prId, @RequestBody PersonalRecord personalRecord) {
        log.info("Updating personal record for pr id: {}", prId);
        var updatedPersonalRecords = personalRecordService.updatePersonalRecords(prId, personalRecord);
        try {
            log.info("Personal record updated successfully");
            return ResponseEntity.ok(ResponseUtil.success("Personal record updated successfully", updatedPersonalRecords));
        } catch (Exception e) {
            log.error("Error updating personal record with id: {}", prId, e);
            return ResponseEntity.ok(ResponseUtil.error("Failed to update personal record"));
        }
    }
}
