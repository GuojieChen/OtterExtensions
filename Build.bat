@echo off
set msbuild=%programfiles(x86)%\MSBuild\14.0\Bin\MsBuild.exe

"%msbuild%" %cd%\OtterExtensions.AppConfig\OtterExtensions.AppConfig.csproj /p:Configuration=Release /t:Clean;Rebuild /p:OutputPath=..\bin\OtterExtensions.AppConfig\
del %cd%\bin\OtterExtensions.AppConfig.otterx
7z a -tzip %cd%\bin\OtterExtensions.AppConfig.otterx %cd%\bin\OtterExtensions.AppConfig\*.dll

"%msbuild%" %cd%\OtterExtensions.Chocolatey\OtterExtensions.Chocolatey.csproj /p:Configuration=Release /t:Clean;Rebuild /p:OutputPath=..\bin\OtterExtensions.Chocolatey\
del %cd%\bin\OtterExtensions.Chocolatey.otterx
7z a -tzip %cd%\bin\OtterExtensions.Chocolatey.otterx %cd%\bin\OtterExtensions.Chocolatey\*.dll

"%msbuild%" %cd%\OtterExtensions.Redis\OtterExtensions.Redis.csproj /p:Configuration=Release /t:Clean;Rebuild /p:OutputPath=..\bin\OtterExtensions.Redis\
del %cd%\bin\OtterExtensions.Redis.otterx
7z a -tzip %cd%\bin\OtterExtensions.Redis.otterx %cd%\bin\OtterExtensions.Redis\*.dll

"%msbuild%" %cd%\OtterExtensions.Svn\OtterExtensions.Svn.csproj /p:Configuration=Release /t:Clean;Rebuild /p:OutputPath=..\bin\OtterExtensions.Svn\
del %cd%\bin\OtterExtensions.Svn.otterx
7z a -tzip %cd%\bin\OtterExtensions.Svn.otterx %cd%\bin\OtterExtensions.Svn\*.dll

"%msbuild%" %cd%\OtterExtensions.System\OtterExtensions.System.csproj /p:Configuration=Release /t:Clean;Rebuild /p:OutputPath=..\bin\OtterExtensions.System\
del %cd%\bin\OtterExtensions.System.otterx
7z a -tzip %cd%\bin\OtterExtensions.System.otterx %cd%\bin\OtterExtensions.System\*.dll

pause