#define	VPN_EXE

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <wchar.h>
#include <stdarg.h>
#include <time.h>
#include <Mayaqua/Mayaqua.h>
#include <Cedar/Cedar.h>

void StartProcess()
{
	InitCedar();
	ElStart();
}

void StopProcess()
{
	ElStop();
	FreeCedar();
}

int main(int argc, char *argv[])
{
#ifdef	OS_WIN32
	return MsService("ELOGSVC", StartProcess, StopProcess, ICO_TOWER, argv[0]);
#else	// OS_WIN32
	return UnixService(argc, argv, "ELOGSVC", StartProcess, StopProcess);
#endif	// OS_WIN32
}


