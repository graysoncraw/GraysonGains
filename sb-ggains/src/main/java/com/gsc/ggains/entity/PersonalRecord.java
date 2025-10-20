package com.gsc.ggains.entity;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.UUID;

@Entity
@NoArgsConstructor
@Data
public class PersonalRecord {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;
    @OneToOne(mappedBy = "personalRecords")
    @JsonBackReference
    private User user;
    @Column(name = "benchpress_pr")
    private int benchPressPR;
    @Column(name = "squat_pr")
    private int squatPR;
    @Column(name = "deadlift_pr")
    private int deadliftPR;
    @Column(name = "shoulderpress_pr")
    private int shoulderPressPR;
}
