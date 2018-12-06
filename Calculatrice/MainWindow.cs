using System;
using Gtk;
using System.Linq.Expressions;
using Calculatrice;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using computer;
using System.Reflection.Emit;

public partial class MainWindow : Gtk.Window
{
    public Dictionary<string, string> compute = new Dictionary<string, string>();

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        string[] lines = System.IO.File.ReadAllLines(@"../../Config.txt");
        foreach (string elem in lines)
        {
            string[] dll = elem.Split(' ');
            compute[dll.First()] = dll.Last();
        }
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnComputeToggled(object sender, EventArgs e)
    {
        string text = this.Input.Text;
        string[] data = text.Split(' ');
        foreach (string elem in data)
        {
            if (compute.ContainsKey(elem))
            {
                Assembly dll = Assembly.LoadFile(compute[elem]);
                Type type = dll.GetExportedTypes()[0];

                Computer instance = (Computer)Activator.CreateInstance(type);
                double A = Convert.ToDouble(data.First());
                double B = Convert.ToDouble(data.Last());
                double result = instance.Compute(A, B);
                string output = "\n>  " + text + "\n   = " + result.ToString();
                this.Output.Text += output;
                this.Input.DeleteText(0, text.Length);
            }
        }
    }
}
