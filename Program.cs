using System;
using System.Windows.Forms;

namespace Spawnv1
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Main());
    }
  }
}
