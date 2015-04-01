set compPath=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set msbuild.exe=%%D\MSBuild.exe

start /d %msbuild.exe% "%~dp0AirPlaner.csproj"