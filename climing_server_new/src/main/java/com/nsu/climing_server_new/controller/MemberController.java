package com.nsu.climing_server_new.controller;

import com.nsu.climing_server_new.service.MemberService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/member")
@RequiredArgsConstructor
public class MemberController {
    private final MemberService service;

    // TODO @RequestParam 대신 @RequestBody 사용하기

    @PostMapping("/login")
    public String login(
            @RequestParam String email,
            @RequestParam String password
    ) {
        return service.login(email, password);
    }

    @PostMapping("/register")
    public ResponseEntity<Void> register(
            @RequestParam String email,
            @RequestParam String password,
            @RequestParam String username
    ) {
        service.register(email, password, username);
        return ResponseEntity.ok().build();
    }

    @PostMapping("/unregister")
    public ResponseEntity<Void> unregister(
            @RequestParam String email,
            @RequestParam String password
    ) {
        service.unregister(email, password);
        return ResponseEntity.ok().build();
    }
}
