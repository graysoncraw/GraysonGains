package com.gsc.ggains.util;

import com.gsc.ggains.entity.api.ApiResponse;
import lombok.experimental.UtilityClass;

@UtilityClass
public class ResponseUtil {
    public static <T> ApiResponse<T> success(String message, T data) {
        return new ApiResponse<>("200", message, data);
    }

    public static <T> ApiResponse<T> error(String message) {
        return new ApiResponse<>("200", message, null);
    }
}
