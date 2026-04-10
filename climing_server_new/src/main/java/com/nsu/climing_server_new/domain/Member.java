package com.nsu.climing_server_new.domain;

import jakarta.persistence.*;
import lombok.AccessLevel;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Entity
@Table(name = "members")        // user나 users는 DB 예약어로 지정되어 있을 확률이 높기 때문에 사용하지 않는다.
@Getter
@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class Member {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(unique = true)
    private String email;

    @Column(nullable = false)
    private String password;

    @Column(nullable = false)
    private String username;

    @Column(nullable = false)
    private boolean enabled;

    @Builder
    public Member(String email, String password, String username) {
        this.email = email;
        this.password = password;
        this.username = username;
        this.enabled = true;
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
    public void updatePassword(String newPassword) {
        this.password = newPassword;
    }

    /// 유저 회원 탈퇴 시 사용
    public void disable() {
        this.enabled = false;
    }
}
