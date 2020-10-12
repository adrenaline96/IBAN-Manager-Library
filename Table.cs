using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IBAN_Manager
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
        catch 
            {
                throw new Exception("Failed to load IBAN table data. Maybe table.ini is missing? It needs to be placed in the application folder. It can be found here: https://github.com/adrenaline96/IBAN-Manager-Library/blob/master/table.ini");
            }
    }
}
}
