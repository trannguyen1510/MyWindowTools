@echo off
set _p=%~2
set _d=%_p:~0,2%
set _execute=%~3
call:%~1
goto :eof

:DefaultExecute
%_d%
cd %_p%
%_execute%
goto :eof

:eof