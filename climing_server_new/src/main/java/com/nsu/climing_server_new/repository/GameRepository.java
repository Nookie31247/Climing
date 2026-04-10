package com.nsu.climing_server_new.repository;

import com.nsu.climing_server_new.domain.Game;
import org.springframework.data.jpa.repository.JpaRepository;

public interface GameRepository extends JpaRepository<Game, Long> {
}
