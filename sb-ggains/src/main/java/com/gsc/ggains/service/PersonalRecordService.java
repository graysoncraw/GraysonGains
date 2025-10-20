package com.gsc.ggains.service;

import com.gsc.ggains.entity.PersonalRecord;
import com.gsc.ggains.repository.PersonalRecordRepository;
import lombok.AllArgsConstructor;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
@AllArgsConstructor
public class PersonalRecordService {

    private PersonalRecordRepository personalRecordRepository;

    public PersonalRecord getPRsById(UUID id) {
        return personalRecordRepository.findById(id).orElse(null);
    }

    public PersonalRecord updatePersonalRecords(UUID prId, PersonalRecord newPersonalRecords) {
        var existingPersonalRecords = getPRsById(prId);
        if (existingPersonalRecords != null) {
            BeanUtils.copyProperties(newPersonalRecords, existingPersonalRecords, "id", "userId");
            return personalRecordRepository.save(existingPersonalRecords);
        }
        return null;
    }
}
