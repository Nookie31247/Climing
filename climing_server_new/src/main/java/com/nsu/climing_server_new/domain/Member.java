package com.nsu.climing_server_new.domain;

import jakarta.persistence.*;
import lombok.AccessLevel;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Entity
@Getter
// user나 users는 DB 예약어로 지정되어 있을 확률이 높기 때문에 사용하지 않는다.
@Table(name = "members")
@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class Member {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false)
    private String email;

    @Column(nullable = false)
    private String passwordHash;

    @Column(nullable = false)
    private String username;

    @Column(nullable = false)
    private boolean active = true;


    @Builder
    public Member(String email, String password, String username) {
        this.email = email;
        this.passwordHash = password;
        this.username = username;
    }

    /// 유저 이메일 변경 시 사용
    public void updateEmail(String email) {
        this.email = email;
    }

    /// 유저 이름 변경 시 사용
    public void updateUsername(String username) {
        this.username = username;
    }

    /// 유저 비밀번호 변경 시 사용
    public void updatePassword(String newPasswordHash) {
        this.passwordHash = newPasswordHash;
    }

    /// 유저 회원 탈퇴 시 사용
    public void deactivate() {
        this.active = false;
    }
}
