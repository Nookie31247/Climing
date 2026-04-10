package com.nsu.climing_server_new.repository;

import com.nsu.climing_server_new.domain.Member;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface MemberRepository extends JpaRepository<Member, Long> {
    /// 이메일과 비밀번호로 유저를 찾아온다. 로그인할 때 사용한다.
    Optional<Member> findByEmail(String email);

    /// 이메일을 입력하면 해당 이메일을 사용하고 있는 유저가 있는지 확인한다.
    boolean existsByEmail(String email);
}
