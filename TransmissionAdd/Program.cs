using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransmissionAdd
{
    static class Program
    {
        private const int ERROR_SUCCESS = 0;
        private const int ERROR_INVALILD_COMMAND_LINE = 1;
        private const int ERROR_BAD_ARGUMENTS = 2;
        private const int ERROR_UNKNOWN_ERROR = 3;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 제목에 버전을 표시한다.
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine(String.Format("TransmissionAdd (v{0}.{1}.{2:#0}.{3:#0})\n\n", version.Major, version.Minor, version.Build, version.Revision));

            int len = args.Length;
            if (len == 0)
            {
                ShowUsage();
                MessageBox.Show("토렌트 주소를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ERROR_INVALILD_COMMAND_LINE;
            }

            if (args[0].Equals("/config", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var configFrm = new ConfigForm())
                {
                    configFrm.ShowDialog();
                }
                return ERROR_SUCCESS;
            }

            TransmissionRemote transmission = new TransmissionRemote();
            bool ret = transmission.Add(args[0].Trim());

            if (ret)
            {
                return ERROR_SUCCESS;
            }
            else
            {
                return ERROR_UNKNOWN_ERROR;
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine(@"
  사용법: TransmissionAdd.exe [토렌트 주소]
");
        }
    }
}
