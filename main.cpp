#include <iostream>
#include <string>
#include "iscsi.h"
#pragma comment(lib, "wbemuuid.lib")

int main(int argc, char* argv[]) {
    Iscsi iscsi;
    // 관리자 권한 체크
    if (!iscsi.isAdmin()) {
        iscsi.runAsAdmin();
    }

    if (argc < 3) {
        std::cout << "Usage: " << argv[0] << " <USER_CONN_NUM> <VPN_IP>" << std::endl;
        return 1;
    }

    std::string userConnNum = argv[1];
    std::string vpnIp = "192.168.135." + userConnNum;

    // iSCSI 타겟에 연결
    std::wstring command = L"Connect-IscsiTarget -NodeAddress 'iqn.2024-12.com.climing:" + std::wstring(userConnNum.begin(), userConnNum.end()) + L"' -TargetPortalAddress '" + std::wstring(vpnIp.begin(), vpnIp.end()) + L"'";
    iscsi.executeCommand(command);

    return 0;
}
