package com.gsc.ggains.controller;

import com.gsc.ggains.entity.api.ApiResponse;
import com.gsc.ggains.entity.User;
import com.gsc.ggains.service.UserService;
import com.gsc.ggains.util.ResponseUtil;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/api/user")
@AllArgsConstructor
@Slf4j
public class UserController {
    private UserService userService;

    @PostMapping("/create")
    public ResponseEntity<ApiResponse<User>> createUser(@RequestBody User user) {
        log.info("Received request to create user");
        var createdUser = userService.createUser(user);
        if (createdUser != null) {
            log.info("User created successfully: id={}, username={}", createdUser.getId(), createdUser.getUsername());
            return ResponseEntity.ok(ResponseUtil.success("User created successfully", createdUser));
        } else {
            log.warn("User creation failed for username: {}", user.getUsername());
            return ResponseEntity.ok(ResponseUtil.error("User creation failed"));
        }
    }

    @GetMapping
    public ResponseEntity<ApiResponse<List<User>>> getAllUsers() {
        log.info("Fetching all users");
        return ResponseEntity.ok(ResponseUtil.success("Users fetched successfully", userService.getAllUsers()));
    }

    @GetMapping("/username/{username}")
    public ResponseEntity<ApiResponse<User>> getUserByUsername(@PathVariable String username) {
        log.info("Fetching user by name: {}", username);
        var user = userService.getUserByName(username);
        if (user != null) {
            log.info("User found: id={}, username={}", user.getId(), user.getUsername());
            return ResponseEntity.ok(ResponseUtil.success("User fetched successfully", user));
        } else {
            log.warn("User not found with username: {}", username);
            return ResponseEntity.ok(ResponseUtil.error("User not found"));
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<ApiResponse<User>> getUserById(@PathVariable UUID id) {
        log.info("Fetching user by id: {}", id);
        var user = userService.getUserById(id);
        if (user != null) {
            log.info("User found: id={}, username={}", user.getId(), user.getUsername());
            return ResponseEntity.ok(ResponseUtil.success("User fetched successfully", user));
        } else {
            log.warn("User not found with id: {}", id);
            return ResponseEntity.ok(ResponseUtil.error("User not found"));
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<ApiResponse<User>> updateUserInformation(@PathVariable UUID id, @RequestBody User user) {
        log.info("Received request to update user with id {}", id);
        return ResponseEntity.ok(ResponseUtil.success("User updated successfully", userService.updateUser(id, user)));
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<ApiResponse<User>> deleteUser(@PathVariable UUID id) {
        log.info("Received request to delete user with id {}", id);
        var user = userService.deleteUser(id);
        return ResponseEntity.ok(ResponseUtil.success("User deleted successfully", user));
    }
}
