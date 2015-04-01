set compPath=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v4*) do set msbuild.exe=%%D\MSBuild.exe

%msbuild.exe% AirPlaner.sln /property:Configuration=Release