package com.gsc.ggains.service;

import com.gsc.ggains.entity.User;
import com.gsc.ggains.repository.UserRepository;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
@AllArgsConstructor
public class UserService {

    private UserRepository userRepository;

    public User createUser(User user) {
        return userRepository.save(user);
    }
    public User updateUser(UUID id, User user) {
        if (userRepository.existsById(id)){
            return userRepository.save(user);
        }
        return null;
    }

    public List<User> getAllUsers() {
        return userRepository.findAll();
    }

    public User getUserByName(String name) {
        return userRepository.findByUsernameIgnoreCase(name).orElse(null);
    }

    public User getUserById(UUID id) {
        return userRepository.findById(id).orElse(null);
    }


    public User deleteUser(UUID id) {
        var user = getUserById(id);
        userRepository.deleteById(id);
        return user;
    }
}
