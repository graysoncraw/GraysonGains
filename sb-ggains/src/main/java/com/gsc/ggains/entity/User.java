package com.gsc.ggains.entity;

import com.fasterxml.jackson.annotation.JsonManagedReference;
import com.gsc.ggains.entity.enums.Gender;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    private UUID id;
    private String firstName;
    private String lastName;
    private String username;
    private String passwordHash;
    private String email;
    @Enumerated(EnumType.STRING)
    private Gender gender;
    private int heightFt;
    private int heightIn;
    private double weight;
    private LocalDateTime dateCreated;
    @OneToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "prId", referencedColumnName = "id")
    @JsonManagedReference
    private PersonalRecord personalRecords;
    @OneToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "wdId", referencedColumnName = "id")
    @JsonManagedReference
    private WorkoutSchedule workoutSchedule;
    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    @JsonManagedReference
    private List<WorkoutLog> workoutLogs = new ArrayList<>();
}
