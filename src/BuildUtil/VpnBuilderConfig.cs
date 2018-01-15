// SoftEther VPN Source Code - Developer Edition Master Branch
// Build Utility
// 
// SoftEther VPN Server, Client and Bridge are free software under GPLv2.
// 
// Copyright (c) Daiyuu Nobori.
// Copyright (c) SoftEther VPN Project, University of Tsukuba, Japan.
// Copyright (c) SoftEther Corporation.
// 
// All Rights Reserved.
// 
// http://www.softether.org/
// 
// Author: Daiyuu Nobori, Ph.D.
// Comments: Tetsuo Sugiyama, Ph.D.
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// version 2 as published by the Free Software Foundation.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License version 2
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
// THE LICENSE AGREEMENT IS ATTACHED ON THE SOURCE-CODE PACKAGE
// AS "LICENSE.TXT" FILE. READ THE TEXT FILE IN ADVANCE TO USE THE SOFTWARE.
// 
// 
// THIS SOFTWARE IS DEVELOPED IN JAPAN, AND DISTRIBUTED FROM JAPAN,
// UNDER JAPANESE LAWS. YOU MUST AGREE IN ADVANCE TO USE, COPY, MODIFY,
// MERGE, PUBLISH, DISTRIBUTE, SUBLICENSE, AND/OR SELL COPIES OF THIS
// SOFTWARE, THAT ANY JURIDICAL DISPUTES WHICH ARE CONCERNED TO THIS
// SOFTWARE OR ITS CONTENTS, AGAINST US (SOFTETHER PROJECT, SOFTETHER
// CORPORATION, DAIYUU NOBORI OR OTHER SUPPLIERS), OR ANY JURIDICAL
// DISPUTES AGAINST US WHICH ARE CAUSED BY ANY KIND OF USING, COPYING,
// MODIFYING, MERGING, PUBLISHING, DISTRIBUTING, SUBLICENSING, AND/OR
// SELLING COPIES OF THIS SOFTWARE SHALL BE REGARDED AS BE CONSTRUED AND
// CONTROLLED BY JAPANESE LAWS, AND YOU MUST FURTHER CONSENT TO
// EXCLUSIVE JURISDICTION AND VENUE IN THE COURTS SITTING IN TOKYO,
// JAPAN. YOU MUST WAIVE ALL DEFENSES OF LACK OF PERSONAL JURISDICTION
// AND FORUM NON CONVENIENS. PROCESS MAY BE SERVED ON EITHER PARTY IN
// THE MANNER AUTHORIZED BY APPLICABLE LAW OR COURT RULE.
// 
// USE ONLY IN JAPAN. DO NOT USE THIS SOFTWARE IN ANOTHER COUNTRY UNLESS
// YOU HAVE A CONFIRMATION THAT THIS SOFTWARE DOES NOT VIOLATE ANY
// CRIMINAL LAWS OR CIVIL RIGHTS IN THAT PARTICULAR COUNTRY. USING THIS
// SOFTWARE IN OTHER COUNTRIES IS COMPLETELY AT YOUR OWN RISK. THE
// SOFTETHER VPN PROJECT HAS DEVELOPED AND DISTRIBUTED THIS SOFTWARE TO
// COMPLY ONLY WITH THE JAPANESE LAWS AND EXISTING CIVIL RIGHTS INCLUDING
// PATENTS WHICH ARE SUBJECTS APPLY IN JAPAN. OTHER COUNTRIES' LAWS OR
// CIVIL RIGHTS ARE NONE OF OUR CONCERNS NOR RESPONSIBILITIES. WE HAVE
// NEVER INVESTIGATED ANY CRIMINAL REGULATIONS, CIVIL LAWS OR
// INTELLECTUAL PROPERTY RIGHTS INCLUDING PATENTS IN ANY OF OTHER 200+
// COUNTRIES AND TERRITORIES. BY NATURE, THERE ARE 200+ REGIONS IN THE
// WORLD, WITH DIFFERENT LAWS. IT IS IMPOSSIBLE TO VERIFY EVERY
// COUNTRIES' LAWS, REGULATIONS AND CIVIL RIGHTS TO MAKE THE SOFTWARE
// COMPLY WITH ALL COUNTRIES' LAWS BY THE PROJECT. EVEN IF YOU WILL BE
// SUED BY A PRIVATE ENTITY OR BE DAMAGED BY A PUBLIC SERVANT IN YOUR
// COUNTRY, THE DEVELOPERS OF THIS SOFTWARE WILL NEVER BE LIABLE TO
// RECOVER OR COMPENSATE SUCH DAMAGES, CRIMINAL OR CIVIL
// RESPONSIBILITIES. NOTE THAT THIS LINE IS NOT LICENSE RESTRICTION BUT
// JUST A STATEMENT FOR WARNING AND DISCLAIMER.
// 
// 
// SOURCE CODE CONTRIBUTION
// ------------------------
// 
// Your contribution to SoftEther VPN Project is much appreciated.
// Please send patches to us through GitHub.
// Read the SoftEther VPN Patch Acceptance Policy in advance:
// http://www.softether.org/5-download/src/9.patch
// 
// 
// DEAR SECURITY EXPERTS
// ---------------------
// 
// If you find a bug or a security vulnerability please kindly inform us
// about the problem immediately so that we can fix the security problem
// to protect a lot of users around the world as soon as possible.
// 
// Our e-mail address for security reports is:
// softether-vpn-security [at] softether.org
// 
// Please note that the above e-mail address is not a technical support
// inquiry address. If you need technical assistance, please visit
// http://www.softether.org/ and ask your question on the users forum.
// 
// Thank you for your cooperation.
// 
// 
// NO MEMORY OR RESOURCE LEAKS
// ---------------------------
// 
// The memory-leaks and resource-leaks verification under the stress
// test has been passed before release this source code.


using System;
using System.Threading;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using CoreUtil;

namespace BuildUtil
{
	// Build settings
	public static class BuildConfig
	{
		public static readonly int NumMultipleCompileTasks = 1;
	}

	// Software List
	public static class BuildSoftwareList
	{
		// List creation date and time
		public static DateTime ListCreatedDateTime = DateTime.Now;

		// ========== Windows ==========
		public static readonly BuildSoftware etherlogger_win32_x86x64_ja =
			new BuildSoftwareWin32(Software.elog, 0, 0, "", CpuList.intel, OSList.Windows);

		static BuildSoftwareList()
		{
		}

		public static BuildSoftware[] List
		{
			get
			{
				List<BuildSoftware> o = new List<BuildSoftware>();
				foreach (FieldInfo fi in typeof(BuildSoftwareList).GetFields(BindingFlags.Static | BindingFlags.Public))
					if (fi.FieldType == typeof(BuildSoftware))
						o.Add((BuildSoftware)fi.GetValue(null));
				return o.ToArray();
			}
		}

		public static BuildSoftware Find(Software soft, OS os, Cpu cpu)
		{
			foreach (BuildSoftware s in List)
			{
				if (s.Software == soft && s.Os == os && s.Cpu == cpu)
				{
					return s;
				}
			}
			return null;
		}
	}

	// OS List
	public static class OSList
	{
		// Windows
		public static readonly OS Windows = new OS("windows", "Windows",
			"Windows 98 / 98 SE / ME / NT 4.0 SP6a / 2000 SP4 / XP SP2, SP3 / Vista SP1, SP2 / 7 SP1 / 8 / 8.1 / 10 / Server 2003 SP2 / Server 2008 SP1, SP2 / Hyper-V Server 2008 / Server 2008 R2 SP1 / Hyper-V Server 2008 R2 / Server 2012 / Hyper-V Server 2012 / Server 2012 R2 / Hyper-V Server 2012 R2 / Server 2016",
			new Cpu[]
			{
				CpuList.intel,
			});

		// Linux
		public static readonly OS Linux = new OS("linux", "Linux",
			"Linux Kernel 2.4 / 2.6 / 3.x / 4.x",
			new Cpu[]
			{
				CpuList.x86,
				CpuList.x64,
				CpuList.mipsel,
				CpuList.ppc32,
				CpuList.ppc64,
				CpuList.sh4,
				CpuList.arm,
				CpuList.armeabi,
			});

		// FreeBSD
		public static readonly OS FreeBSD = new OS("freebsd", "FreeBSD",
			"FreeBSD 5 / 6 / 7 / 8 / 9 / 10",
			new Cpu[]
			{
				CpuList.x86,
				CpuList.x64,
			});

		// OpenBSD
		public static readonly OS OpenBSD = new OS("openbsd", "OpenBSD",
			"OpenBSD 5 / 6 / 7 / 8 / 9 / 10",
			new Cpu[]
			{
				CpuList.x86,
				CpuList.x64,
			});

		// Solaris
		public static readonly OS Solaris = new OS("solaris", "Solaris",
			"Solaris 8 / 9 / 10 / 11",
			new Cpu[]
			{
				CpuList.x86,
				CpuList.x64,
				CpuList.sparc32,
				CpuList.sparc64,
			});

		// Mac OS X
		public static readonly OS MacOS = new OS("macos", "Mac OS X",
			"Mac OS X 10.4 Tiger / 10.5 Leopard / 10.6 Snow Leopard / 10.7 Lion / 10.8 Mountain Lion / 10.9 Mavericks",
			new Cpu[]
			{
				CpuList.x86,
				CpuList.x64,
				CpuList.ppc32,
				CpuList.ppc64,
			});

		static OSList()
		{
			OSList.Windows.IsWindows = true;
		}

		public static OS[] List
		{
			get
			{
				List<OS> o = new List<OS>();
				foreach (FieldInfo fi in typeof(OSList).GetFields(BindingFlags.Static | BindingFlags.Public))
					if (fi.FieldType == typeof(OS))
						o.Add((OS)fi.GetValue(null));
				return o.ToArray();
			}
		}

		public static OS FindByName(string name)
		{
			foreach (OS os in List)
			{
				if (os.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return os;
				}
			}

			throw new ApplicationException(name);
		}
	}

	// CPU List
	public static class CpuList
	{
		public static readonly Cpu x86 = new Cpu("x86", "Intel x86", CPUBits.Bits32);
		public static readonly Cpu x64 = new Cpu("x64", "Intel x64 / AMD64", CPUBits.Bits64);
		public static readonly Cpu intel = new Cpu("x86_x64", "Intel", CPUBits.Both);
		public static readonly Cpu arm = new Cpu("arm", "ARM legacy ABI", CPUBits.Bits32);
		public static readonly Cpu armeabi = new Cpu("arm_eabi", "ARM EABI", CPUBits.Bits32);
		public static readonly Cpu mipsel = new Cpu("mips_el", "MIPS Little-Endian", CPUBits.Bits32);
		public static readonly Cpu ppc32 = new Cpu("powerpc", "PowerPC", CPUBits.Bits32);
		public static readonly Cpu ppc64 = new Cpu("powerpc64", "PowerPC G5", CPUBits.Bits64);
		public static readonly Cpu sh4 = new Cpu("sh4", "SH-4", CPUBits.Bits32);
		public static readonly Cpu sparc32 = new Cpu("sparc", "SPARC", CPUBits.Bits32);
		public static readonly Cpu sparc64 = new Cpu("sparc64", "SPARC", CPUBits.Bits64);

		public static Cpu[] List
		{
			get
			{
				List<Cpu> o = new List<Cpu>();
				foreach (FieldInfo fi in typeof(CpuList).GetFields(BindingFlags.Static | BindingFlags.Public))
					if (fi.FieldType == typeof(Cpu))
						o.Add((Cpu)fi.GetValue(null));
				return o.ToArray();
			}
		}

		public static Cpu FindByName(string name)
		{
			foreach (Cpu c in List)
			{
				if (c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return c;
				}
			}

			throw new ApplicationException(name);
		}
	}
}

