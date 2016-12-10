
REM Name   : 工程清理工具
REM Version: V0.3
REM Author : xlog


@ECHO off
ECHO 正在清理VS工程中不需要的文件
ECHO 请确保本文件放置在工程目录之中并关闭VS
ECHO 开始清理请稍等......


ECHO 清理解决方案目录

::ECHO 清理sdf文件
::DEL /q/a/f/s *.sdf

ECHO 清理ipch文件
DEL /q/a/f/s ipch\*.*

::ECHO 清理suo文件
::DEL /q/a/f/s *.suo


ECHO 清理项目目录

REM 枚举每个目录和子目录
FOR /f "delims=" %%a IN ('DIR /ad /b /n /s "*obj" "*bin" "*Debug" "*Release"') DO (
    ECHO 枚举目录："%%a"
    IF EXIST "%%a" (
        REM 清理"Debug"目录
        IF /i "%%~na" == "Debug" (
            ECHO 清理 "%%a" 目录

            REM 进入"Debug"目录
            CD "%%a"

            REM 清理"*.tlog"目录
            FOR /f "delims=" %%i IN ('DIR /ad /b "*.tlog*"') DO (
                ECHO Debug准备删除目录："%%i"
                IF EXIST "%%i" ( RD /s /q "%%i" )
            )

            REM 清理文件
            DEL /q/a/f/s *.obj
            DEL /q/a/f/s *.tlog
            DEL /q/a/f/s *.log
            DEL /q/a/f/s *.res
            DEL /q/a/f/s *.idb
            DEL /q/a/f/s *.pdb
            DEL /q/a/f/s *.ilk
            DEL /q/a/f/s *.pch
            DEL /q/a/f/s *.bsc
            DEL /q/a/f/s *.sbr
            DEL /q/a/f/s *.ipdb
            DEL /q/a/f/s *.iobj
            DEL /q/a/f/s *.manifest
            DEL /q/a/f/s *.xml

            ECHO 清理 "%%a" 目录完成
            ECHO.
        )
        
        REM 清理"Release"目录
        IF /i "%%~na" == "Release" (
            ECHO 清理 "%%a" 目录

            REM 进入"Release"目录
            CD "%%a"

            REM 清理"*.tlog"目录
            FOR /f "delims=" %%i IN ('DIR /ad /b "*.tlog*"') DO (
                ECHO Release准备删除目录："%%i"
                IF EXIST "%%i" ( RD /s /q "%%i" )
            )

            REM 清理文件
            DEL /q/a/f/s *.obj
            DEL /q/a/f/s *.tlog
            DEL /q/a/f/s *.log
            DEL /q/a/f/s *.res
            DEL /q/a/f/s *.idb
            DEL /q/a/f/s *.pdb
            DEL /q/a/f/s *.ilk
            DEL /q/a/f/s *.pch
            DEL /q/a/f/s *.ipdb
            DEL /q/a/f/s *.iobj
            DEL /q/a/f/s *.manifest
            DEL /q/a/f/s *.xml

            ECHO 清理 "%%a" 目录完成
            ECHO.
        )
        
        REM 清理"obj"目录
        IF /i "%%~na" == "obj" (
            ECHO 清理 "%%a" 目录

            REM REM 进入"obj"目录
            REM CD "%%a"

            REM REM 清理所有文件
            REM DEL /q/a/f/s *.*
            REM REM 删除所有文件夹
            REM FOR /d %%i IN (*) DO ( RD /s /q "%%i" )

            REM 删除本目录
            ECHO obj准备删除目录："%%a"
            RD /s /q "%%a"

            ECHO 清理 "%%a" 目录完成
            ECHO.
        )
    )
)

::PAUSE

REM 返回主目录
CD "%~p0"

ECHO 文件清理完毕！本程序将在3秒后退出！现在进入倒计时.........
@ECHO off
ECHO WScript.Sleep 500 > %temp%.\tmp$$$.vbs
SET /a i = 3
:Timeout
IF %i% == 0 goto Next
    SETLOCAL
    SET /a i = %i% - 1
    ECHO 倒计时……%i%
    CSCRIPT //nologo %temp%.\tmp$$$.vbs
    GOTO Timeout
    GOTO End
:Next
CLS &
ECHO.