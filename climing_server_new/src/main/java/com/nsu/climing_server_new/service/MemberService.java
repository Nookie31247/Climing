package com.nsu.climing_server_new.service;

import com.nsu.climing_server_new.domain.Member;
import com.nsu.climing_server_new.exception.ErrorCode;
import com.nsu.climing_server_new.exception.ServiceException;
import com.nsu.climing_server_new.repository.MemberRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
@RequiredArgsConstructor
public class MemberService {
    private final MemberRepository repository;
    private final BCryptPasswordEncoder passwordEncoder;

    /// 로그인
    /// 로그인 성공 시 username을 반환한다.
    /// 로그인 실패 시 예외를 발생시킨다.
    public String login(String email, String password) {
        // 데이터베이스에서 정보를 가져와서 로그인 시도
        // 유저를 찾을 수 없을 시(잘못된 이메일이 입력되었을 시) 예외 발생
        Member loginMember = repository.findByEmail(email)
                .orElseThrow(() -> new ServiceException(ErrorCode.EMAIL_NOT_FOUND));

        // 잘못된 비밀번호가 입력되었을 때 예외 발생
        if(!passwordEncoder.matches(password, loginMember.getPassword())) {
            throw new ServiceException(ErrorCode.INVALID_PASSWORD);
        }

        // 계정 활성화 여부 확인 (회원탈퇴 여부 확인)
        if(!loginMember.isEnabled()) {
            throw new ServiceException(ErrorCode.ACCOUNT_DISABLED);
        }

        // 로그인 성공 시 유저 닉네임 반환
        return loginMember.getUsername();
    }

    /// 회원가입
    /// 회원가입 성공 여부를 boolean으로 반환한다.
    // TODO 이메일 형식 확인, 비밀번호 길이 확인, 유저 닉네임 길이 확인/사용할 수 없는 문자 로직 만들기
    public boolean register(String email, String password, String username) {
        String hashedPassword = passwordEncoder.encode(password);   // 비밀번호 해싱

        // 동일한 id를 가진 유저가 있나 확인하기
        if(repository.existsByEmail(email)) {
            throw new ServiceException(ErrorCode.EMAIL_ALREADY_EXIST);
        }

        return false;
    }

    /// 회원탈퇴

}
