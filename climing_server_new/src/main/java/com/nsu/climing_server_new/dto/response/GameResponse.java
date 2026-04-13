package com.nsu.climing_server_new.dto.response;

import com.nsu.climing_server_new.domain.Game;
import com.nsu.climing_server_new.domain.GameGenre;
import lombok.Builder;
import lombok.Getter;

import java.util.List;

@Getter
@Builder
public class GameResponse {
    private String name;
    private String imageUrl;
    private String company;
    private List<GameGenre> genres;
    private String version;
    private String dirPath;

    public static GameResponse from(Game game) {
        return GameResponse.builder()
                .name(game.getName())
                .imageUrl(game.getImageUrl())
                .company(game.getCompany())
                .genres(game.getGenres())
                .version(game.getVersion())
                .dirPath(game.getDirPath())
                .build();
    }
}
