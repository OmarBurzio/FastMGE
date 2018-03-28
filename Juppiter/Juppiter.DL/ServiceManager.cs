using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juppiter.DL
{
    public static class DatabaseName
    {
        public static string BAM = "BAM";
    }

    public class ServiceManager
    {
        public ServiceManager()
        {

        }

        public ItemEvento Initialize()
        {
            ItemEvento result = new ItemEvento();

            try
            {
                Process proc = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = @Properties.Settings.Default.Mongod;
                startInfo.Arguments = @" --port " + Properties.Settings.Default.Port1 + @" --dbpath " + Properties.Settings.Default.Node1 + " --replSet " + Properties.Settings.Default.ReplicationName;
                proc.StartInfo = startInfo;
                // proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process proc1 = new Process();
                ProcessStartInfo startInfo1 = new ProcessStartInfo();

                startInfo1.FileName = @Properties.Settings.Default.Mongod;
                startInfo1.Arguments = @" --port " + Properties.Settings.Default.Port2 + @" --dbpath " + Properties.Settings.Default.Node2 + " --replSet " + Properties.Settings.Default.ReplicationName;
                proc1.StartInfo = startInfo1;
                // proc1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process proc2 = new Process();
                ProcessStartInfo startInfo2 = new ProcessStartInfo();

                startInfo2.FileName = @Properties.Settings.Default.Mongod;
                startInfo2.Arguments = @" --port " + Properties.Settings.Default.Port3 + @" --dbpath " + Properties.Settings.Default.Node3 + " --replSet " + Properties.Settings.Default.ReplicationName;
                proc2.StartInfo = startInfo2;
                // proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                proc.Start();
                proc1.Start();
                proc2.Start();
            }
            catch (Exception ex)
            {
                result.AddError(ex);
            }

            return result;
        }


        private CausaliManager causaliManagerInternal = null;
        public CausaliManager CausaliManager
        {
            get
            {
                if(causaliManagerInternal == null)
                {
                    causaliManagerInternal = new CausaliManager(this);
                }
                return causaliManagerInternal;
            }
        }
    }
}
