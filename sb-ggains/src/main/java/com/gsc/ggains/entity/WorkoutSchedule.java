package com.gsc.ggains.entity;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.gsc.ggains.entity.enums.WorkoutDay;
import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Date;
import java.util.UUID;

@Entity
@NoArgsConstructor
@Data
public class WorkoutSchedule {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;
    @OneToOne(mappedBy = "workoutSchedule")
    @JsonBackReference
    private User user;
    @Enumerated(EnumType.STRING)
    private WorkoutDay benchDay;
    @Enumerated(EnumType.STRING)
    private WorkoutDay squatDay;
    @Enumerated(EnumType.STRING)
    private WorkoutDay deadliftDay;
    @Enumerated(EnumType.STRING)
    private WorkoutDay shoulderPressDay;
    private Date cycleStartDate;

}
