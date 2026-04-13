package com.nsu.climing_server_new.service;

import com.nsu.climing_server_new.domain.Game;
import com.nsu.climing_server_new.dto.response.GameResponse;
import com.nsu.climing_server_new.repository.GameRepository;
import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
@AllArgsConstructor
public class GameService {
    private final GameRepository repository;

    /// 데이터베이스에 있는 모든 게임 정보들을 반환한다.
    public List<GameResponse> getAllGames() {
        List<GameResponse> result = new ArrayList<>();
        repository.findAll().forEach(game -> {
            result.add(GameResponse.from(game));
        });

        return result;
    }
}
