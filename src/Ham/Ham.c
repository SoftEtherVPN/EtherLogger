// PacketiX VPN Source Code
// Hamster Test Code
// 
// Copyright (c) SoftEther Corporation.
// All Rights Reserved.
// 
// http://www.softether.co.jp/
// Author: Daiyuu Nobori

// Ham.c
// Hamster test program

#include <GlobalConst.h>



//#define	VISTA_HAM


#define	INITGUID

#define	HAM_C

#ifdef	WIN32
#define	HAM_WIN32
#define	_WIN32_WINNT		0x0600
#define	WINVER				0x0600
#include <winsock2.h>
#include <Ws2tcpip.h>
#include <windows.h>
#include <DbgHelp.h>
#include <Iphlpapi.h>
#include <wtsapi32.h>
#include <Ntsecapi.h>
#include <Setupapi.h>
#include "../pencore/resource.h"
#include <Fwpmu.h>
#include <Fwpmtypes.h>
#endif

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <wchar.h>
#include <stdarg.h>
#include <time.h>
#include <errno.h>
#include <math.h>
#include <openssl/ssl.h>
#include <openssl/err.h>
#include <openssl/rand.h>
#include <openssl/engine.h>
#include <openssl/bio.h>
#include <openssl/x509.h>
#include <openssl/pkcs7.h>
#include <openssl/pkcs12.h>
#include <openssl/rc4.h>
#include <openssl/md5.h>
#include <openssl/sha.h>
#include <Mayaqua/Mayaqua.h>
#include <Cedar/Cedar.h>

#ifdef	WIN32
#include <Wfp/Wfp.h>
#endif	// WIN32
// Test function definition list
typedef void (TEST_PROC)(UINT num, char **arg);

typedef struct TEST_LIST
{
	char *command_str;
	TEST_PROC *proc;
} TEST_LIST;



void em_test(UINT num, char **arg)
{
#ifdef	OS_WIN32
	EMExec();
#endif
}

void el_test(UINT num, char *arg[])
{
	Print("EtherLogger Service Test\n\n");

	ElInit();
	ElStart();
	GetLine(NULL, 0);
	ElStop();
	ElFree();
}

TEST_LIST test_list[] =
{
	{"el", el_test},
	{"em", em_test},
};

// Test function
void TestMain(char *cmd)
{
	char tmp[MAX_SIZE];
	bool first = true;
	bool exit_now = false;

#if	0
	// for keishicho
	if (true)
	{
		print_vg_hash_id_winui();
		return;
	}
#endif

	Print("Hamster Tester\n");

#ifdef	OS_WIN32
	MsSetEnableMinidump(false);
#endif	// OS_WIN32

	while (true)
	{
		Print("TEST>");
		if (first && StrLen(cmd) != 0 && g_memcheck == false)
		{
			first = false;
			StrCpy(tmp, sizeof(tmp), cmd);
			exit_now = true;
			Print("%s\n", cmd);
		}
		else
		{
#ifdef	VISTA_HAM
			_exit(0);
#endif
			GetLine(tmp, sizeof(tmp));
		}
		Trim(tmp);
		if (StrLen(tmp) != 0)
		{
			UINT i, num;
			bool b = false;
			TOKEN_LIST *token = ParseCmdLine(tmp);
			char *cmd = token->Token[0];
#ifdef	VISTA_HAM
			if (EndWith(cmd, "vlan") == false)
			{
				_exit(0);
			}
#endif
			if (!StrCmpi(cmd, "exit") || !StrCmpi(cmd, "quit") || !StrCmpi(cmd, "q"))
			{
				FreeToken(token);
				break;
			}
			if (StartWith(tmp, "vpncmd"))
			{
				wchar_t *s = CopyStrToUni(tmp);
				CommandMain(s);
				Free(s);
			}
			else if (StartWith(tmp, "vs"))
			{
//				CommandMain(L"vpncmd /server vpn1.softether.co.jp /password:PenCore32 /ADMINHUB:\"test\"");
//				CommandMain(L"vpncmd /server public1.softether.com /password:PenCore21 /ADMINHUB:Public");
			}
			else if (StartWith(tmp, "vc"))
			{
				CommandMain(L"vpncmd /client 127.0.0.1");
			}
			else if (StartWith(tmp, "vt"))
			{
				CommandMain(L"vpncmd /tools");
			}
			else
			{
				num = sizeof(test_list) / sizeof(TEST_LIST);
				for (i = 0;i < num;i++)
				{
					if (!StrCmpi(test_list[i].command_str, cmd))
					{
						char **arg = Malloc(sizeof(char *) * (token->NumTokens - 1));
						UINT j;
						for (j = 0;j < token->NumTokens - 1;j++)
						{
							arg[j] = CopyStr(token->Token[j + 1]);
						}
						test_list[i].proc(token->NumTokens - 1, arg);
						for (j = 0;j < token->NumTokens - 1;j++)
						{
							Free(arg[j]);
						}
						Free(arg);
						b = true;
						Print("\n");
						break;
					}
				}
				if (b == false)
				{
					Print("Invalid Command: %s\n\n", cmd);
				}
			}
			FreeToken(token);

			if (exit_now)
			{
				break;
			}
		}
	}
	Print("Exiting...\n\n");
}

#ifdef	WIN32
int main(int argc, char *argv[]);
// Winmain function
int PASCAL WinMain(HINSTANCE hInst, HINSTANCE hPrev, char *CmdLine, int CmdShow)
{
	main(0, NULL);
}
#endif

// Main function
int main(int argc, char *argv[])
{
	bool memchk = false;
	UINT i;
	char cmd[MAX_SIZE];
	char *s;

	IKE_SA *sa = NULL;
	IKE_PACKET *pk = NULL;
	IKE_PACKET_PAYLOAD *pp = NULL;
	IKE_CAPS *a1 = NULL;
	L2TP_PACKET *aaa1 = NULL;
	SSTP_PACKET *t1 = NULL;
	SSTP_ATTRIBUTE *t2 = NULL;
	SSTP_SERVER *t3 = NULL;
	OPENVPN_PACKET *t4 = NULL;
	OPENVPN_SESSION *t5 = NULL;
	OPENVPN_SERVER *t6 = NULL;
	OPENVPN_SERVER_UDP *t7 = NULL;
	OPENVPN_KEY_METHOD_2 *t8 = NULL;
	PPP_PACKET *t9 = NULL;
	L2TP_TUNNEL *t10 = NULL;
	L2TP_SERVER *t11 = NULL;
	L2TP_SESSION *t12 = NULL;
	ACCESS *t13 = NULL;
	RADIUS_PACKET *t14 = NULL;
	EAP_MESSAGE *t15 = NULL;
	EAP_CLIENT *t16 = NULL;
	RADIUS_AVP *t17 = NULL;
	PPP_SESSION *t18 = NULL;

//	AbortExitEx("");
//	exit(0);

	//printf("Test Program.\n");

	//VgUseStaticLink();



	cmd[0] = 0;
	if (argc >= 2)
	{
		for (i = 1;i < (UINT)argc;i++)
		{
			s = argv[i];
			if (s[0] == '/')
			{
				if (!StrCmpi(s, "/memcheck"))
				{
					memchk = true;
				}
			}
			else
			{
				StrCpy(cmd, sizeof(cmd), &s[0]);
			}
		}
	}

	InitMayaqua(memchk, true, argc, argv);
	EnableProbe(true);
	InitCedar();
	SetHamMode();
	TestMain(cmdline);
	FreeCedar();
	FreeMayaqua();

	return 0;
}


/*
// WinMain function
int main_(int argc, char *argv[])
{
//	return UnixService(argc, argv, "VPNSERVER", StartProcess, StopProcess);
}

*/




