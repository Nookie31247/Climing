#include "WireGuard.hpp"

using namespace System;
using namespace CppCode;

WireGuardVPN::WireGuardVPN()
{
    configPath = "C:\\Program Files\\WireGuard\\" + deviceName + ".conf";

    clientPrivateKey = command->run("wg genkey");
    clientPublicKey = command->run("echo " + clientPrivateKey + " | wg pubkey");
}

void WireGuardVPN::createVPNConfig(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint)
{
    IO::StreamWriter^ configFile = gcnew IO::StreamWriter(configPath);
    configFile->WriteLine("[Interface]");
    configFile->WriteLine("PrivateKey = " + clientPrivateKey);
    configFile->WriteLine("Address = 192.168.135." + userNum + "/24\n");
    configFile->WriteLine("[Peer]");
    configFile->WriteLine("PublicKey = " + serverPublicKey);
    configFile->WriteLine("AllowedIPS = " + serverIP);
    configFile->WriteLine("Endpoint = " + endpoint);
    configFile->WriteLine("PersistentKeepalive = " + persistentKeepalive);
    configFile->Close();
}

void WireGuardVPN::installTunnel()
{
    command->run("wireguard /installtunnelservice \"" + configPath + "\"");
}

void WireGuardVPN::uninstallTunnel()
{
    command->run("wireguard /uninstalltunnelservice " + deviceName);
}

void WireGuardVPN::deleteVPNConfig()
{
    IO::File::Delete(configPath);
}

String^ WireGuardVPN::getClientPublicKey()
{
    return clientPublicKey;
}

void WireGuardVPN::connectVPN(int userNum, String^ serverPublicKey, String^ serverIP, String^ endpoint)
{
    createVPNConfig(userNum, serverPublicKey, serverIP, endpoint);
    installTunnel();
}

void WireGuardVPN::disconnectVPN()
{
    uninstallTunnel();
    deleteVPNConfig();
}