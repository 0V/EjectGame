using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Runtime.InteropServices;
using System.IO;

namespace EjectGame.Models
{
    public class DriveService : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        [DllImport("winmm", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
            string command, StringBuilder buffer,
            int bufferSize, IntPtr hwndCallback);

        public static void Eject()
        {
            foreach (string drive in Environment.GetLogicalDrives())
            {
                var di = new DriveInfo(drive);
                if (di.DriveType == DriveType.CDRom && CanEject)
                {
                    mciSendString("open " + drive + " type cdaudio alias orator", null, 0, IntPtr.Zero);
                    mciSendString("set orator door open", null, 0, IntPtr.Zero);
                    mciSendString("set orator door closed", null, 0, IntPtr.Zero);
                    mciSendString("close orator", null, 0, IntPtr.Zero);
                    return;
                }
            }
        }

        public static bool CanEject 
        { 
            get
            {
//                return mciSendString("capability orator can eject", null, 0, IntPtr.Zero) == 0;
                return true;
            }
        }

    }
}
