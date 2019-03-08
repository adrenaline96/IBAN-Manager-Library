using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IBAN_Manager_API
{
    class Table
{
    public static void LoadTable(Dictionary<string, string> table)
    {
        try
        {
            var lines = File.ReadAllLines("table.ini");

            foreach (var line in lines)
            {
                string[] values = line.Split(',');

                table.Add(values[0], values[1]);

            }
        }
        catch { }
    }
}
}
