package com.nsu.climing_server_new.domain;

import jakarta.persistence.*;
import lombok.AccessLevel;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;

@Entity     // 이 클래스를 JPA Entity로 선언
@Table(name = "games")   // 매핑할 테이블 이름 지정
@Getter
// JPA는 기본 생성자가 필요하다.
// 외부에서 마음대로 생성하지 못하게 (JPA만 사용할 수 있도록) PROTECTED를 사용한다.
@NoArgsConstructor(access = AccessLevel.PROTECTED)
/// 게임을 저장하기 위한 객체 클래스
public class Game {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    /// 게임 식별 id
    private Long id;

    @Column(nullable = false)
    /// 게임 이름
    private String name;

    @Column(nullable = false)
    /// 게임의 포스트 이미지를 가져올 url 경로
    private String imageUrl;

    @Column
    /// 게임 제작사
    private String company;

    // @ElementCollection은 여러 값을 가진 컬렉션(List)임을 JPA에게 알리기 위해 사용한다.
    // FetchType.LAZY: 지연 로딩 - 게임 정보를 불러오더라도, getGenre를 사용하기 전까진 데이터를 불러오지 않는다.
    // 즉시 로딩을 사용하고 싶으면 FetchType.EAGER를 사용한다.
    @ElementCollection(targetClass = GameGenre.class, fetch = FetchType.LAZY)
    @CollectionTable(
            name = "game_genres",   // 게임 장르를 저장할 별도의 테이블 이름

             // 게임 테이블의 Primary Key와 연결될 Foreign Key의 컬럼명
            joinColumns = @JoinColumn(name = "game_id")
    )
    // Enum Type을 JPA가 어떻게 인식하고 저장할지 설정한다.
    // ORDINAL로 설정하면 enum 순서를 기반으로 인식하기 때문에
    // 나중에 enum에 있는 데이터를 변경할 때 데이터가 불일치해질 수 있다.
    @Enumerated(EnumType.STRING)
    @Column
    /// 게임 장르를 담은 리스트
    private List<GameGenre> genres;

    @Column(nullable = false)
    /// 게임 버전
    private String version;

    @Column(nullable = false)
    /// 게임 실행 경로
    private String dirPath;

    @Builder
    /// 게임 엔티티 Builder
    public Game(String name, String imageUrl, String company, List<GameGenre> genres, String version, String dirPath) {
        this.name = name;
        this.imageUrl = imageUrl;
        this.company = company;
        this.genres = (genres == null) ? new ArrayList<>() : genres;
        this.version = (version == null || version.isBlank()) ? "1.0.0" : version;
        this.dirPath = dirPath;
    }

    /// 게임 정보 업데이트
    public void updateInfo(String name, String company, List<GameGenre> genres) {
        this.name = name;
        this.company = company;
        this.genres = genres;
    }

    /// 게임 포스트 이미지 경로 업데이트
    public void updateImageUrl(String imageUrl) {
        this.imageUrl = imageUrl;
    }

    /// 게임 실행 파일 경로 업데이트
    public void updateDirPath(String dirPath) {
        this.dirPath = dirPath;
    }

    /// 게임 버전 업데이트
    public void updateVersion(String newVersion) {
        this.version = newVersion;
    }
}