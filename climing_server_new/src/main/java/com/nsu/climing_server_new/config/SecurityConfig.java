package com.nsu.climing_server_new.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

/*
@Configuration
    - 해당 클래스가 하나 이상의 @Bean 메서드를 포함하고 있으며
    Spring 컨테이너가 이 메서드들을 호출해 Bean을 생성할 것이라는 것을 알려준다.
    - 객체의 SingleTon을 보장해준다.
 */
@Configuration
public class SecurityConfig {
    @Bean
    public BCryptPasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder();
    }
}
