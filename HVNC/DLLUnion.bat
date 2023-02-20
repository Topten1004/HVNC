@echo off

:: this script needs https://www.nuget.org/packages/ilmerge

:: set your target executable name (typically [projectname].exe)
SET APP_NAME=HVNC.exe

:: Set build, used for directory. Typically Release or Debug
SET ILMERGE_BUILD=Release

:: Set platform, typically x64
SET ILMERGE_PLATFORM=x64

:: set your NuGet ILMerge Version, this is the number from the package manager install, for example:
:: PM> Install-Package ilmerge -Version 3.0.29
:: to confirm it is installed for a given project, see the packages.config file
SET ILMERGE_VERSION=3.0.29

:: the full ILMerge should be found here:
SET ILMERGE_PATH=%USERPROFILE%\.nuget\packages\ilmerge\%ILMERGE_VERSION%\tools\net452
:: dir "%ILMERGE_PATH%"\ILMerge.exe




echo Merging %APP_NAME% ...

:: add project DLL's starting with replacing the FirstLib with this project's DLL
"%ILMERGE_PATH%"\ILMerge.exe bin\%ILMERGE_BUILD%\%APP_NAME%  ^
  /lib:bin\%ILMERGE_BUILD%\ ^
  /out:HVNCdll.exe ^
  BouncyCastle.Crypto.dll ^
  cGeoIp.dll ^
  dnlib.dll ^
  IconExtractor.dll ^
  Newtonsoft.Json.dll ^
  protobuf-net.Core.dll ^
  protobuf-net.dll ^
  Siticone.Desktop.UI.dll ^
  System.Buffers.dll ^
  System.Collections.Immutable.dll ^
  System.Memory.dll ^
  System.Numerics.Vectors.dll ^
  System.Runtime.CompilerServices.Unsafe.dll ^
  Vestris.ResourceLib.dll 


:Done
dir %APP_NAME%