import serial
import pyautogui

arduino = serial.Serial('COM4', 115200)  # 아두이노 포트에 맞게 변경

while True:
    if arduino.in_waiting:
        line = arduino.readline().decode().strip()
        if line == "SPACE":
            pyautogui.press('space')