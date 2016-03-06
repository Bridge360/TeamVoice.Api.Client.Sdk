@echo off
@pushd "%temp%"
@makecab /D RptFileName=~.rpt /D InfFileName=~.inf /f nul >nul
@for /f "tokens=3-7" %%a in ('find /i "makecab"^<~.rpt') do @( 
	set "current-date=%%e-%%b-%%c";
	set "current-time=%%d"
	set "weekday=%%a"
	set "year=%%e"
)
@del ~.*
@cd %~dp0
@echo namespace TeamVoice.Api { public abstract partial class Current { public const string Year = "%year%"; } } > "CurrentYear.cs"
@popd