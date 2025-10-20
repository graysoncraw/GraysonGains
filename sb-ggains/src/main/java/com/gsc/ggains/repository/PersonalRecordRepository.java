package com.gsc.ggains.repository;

import com.gsc.ggains.entity.PersonalRecord;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.UUID;

@Repository
public interface PersonalRecordRepository extends JpaRepository<PersonalRecord, UUID> {

}
