#pragma once
#using <mscorlib.dll>
#include "RunCommand.hpp"
using namespace System;

namespace CppCode
{
    public ref class WireGuardVPN
    {
    private:
        String^ deviceName = "Climing_Service";
        String^ persistentKeepalive = "25";
        String^ configPath;
        String^ clientPrivateKey;
        String^ clientPublicKey;

        RunCommand^ command = gcnew RunCommand();

        void createVPNConfig(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint);
        void installTunnel();
        void uninstallTunnel();
        void deleteVPNConfig();

    public:
        WireGuardVPN();
        String^ getClientPublicKey();
        void connectVPN(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint);
        void disconnectVPN();
    };
}