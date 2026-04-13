package com.nsu.climing_server_new.controller;

import com.nsu.climing_server_new.domain.Game;
import com.nsu.climing_server_new.dto.response.GameResponse;
import com.nsu.climing_server_new.service.GameService;
import lombok.AllArgsConstructor;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.List;

@RestController
@RequestMapping("/api/game")
@RequiredArgsConstructor
public class GameController {
    private final GameService service;

    @GetMapping("/list")
    public List<GameResponse> getGames() {
        return service.getAllGames();
    }
}
