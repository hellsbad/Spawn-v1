using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Spawnv1
{
  public class Main : Form
  {
    private IContainer components;
    private Button button1;

    public Main()
    {
      this.InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Console.Beep();
      Mem.WriteMemory<int>(Mem.ReadMemory<int>(Mem.ReadMemory<int>(Mem.ReadMemory<int>(Mem.ReadMemory<int>(Mem.ReadMemory<int>(Mem.BaseAddress + 29512092)) + 256) + 16) + 528) + 44, (object) 100000);
    }

    private void Main_Load(object sender, EventArgs e)
    {
      Process m_process = ((IEnumerable<Process>) Process.GetProcessesByName("ros")).FirstOrDefault<Process>();
      if (m_process == null)
      {
        int num = (int) MessageBox.Show("Error!", "Please open the game");
        Environment.Exit(-1);
      }
      Mem.Initialize(m_process, m_process.MainModule.BaseAddress);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 152);
            this.button1.TabIndex = 0;
            this.button1.Text = "ON";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 174);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

    }
  }
}
