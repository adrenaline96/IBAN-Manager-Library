using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace IBAN_Manager_API
{
    public class IBANManager
    {
        private List<String> ibanCharList = new List<String>(34);
        private String iban = String.Empty;

        private Dictionary<String, String> tableValues = new Dictionary<String, String>();

        public IBANManager(String ibanCode, Dictionary<String, String> ibanTableValues)
        {
            ibanCode.Replace(" ", String.Empty);

            iban = ibanCode;

            ibanCharList.AddRange(ibanCode.Select(c => c.ToString()).ToList());

            foreach (var kvp in ibanTableValues)
            {
                tableValues.Add(kvp.Key, kvp.Value);
            }

        }
        public String GetIban()
        {
            return iban;
        }
        public String GenerateValidIban()
        {
            String validIban = String.Empty;

            List<String> intermediateIban = new List<String>();

            String tableString = String.Empty;

            String verChars = String.Empty;

            intermediateIban.AddRange(ibanCharList);

            for (int i = 0; i <= 3; i++)
            {
                intermediateIban.Add(intermediateIban[i]);
            }

            intermediateIban.RemoveRange(0, 4);


            foreach (var item in intermediateIban)
            {
                if (!item.All(char.IsDigit))
                {
                    foreach (KeyValuePair<String, String> tableItem in tableValues)
                    {
                        if (item.Equals(tableItem.Key))
                        {
                            tableString = tableString + tableItem.Value;
                        }

                    }
                }
                else
                {
                    tableString = tableString + item;
                }
            }

            BigInteger tableStringInt = BigInteger.Parse(tableString);
            BigInteger remainder = tableStringInt % 97;

            verChars = Convert.ToString(98 - remainder);

            if (verChars.Length < 2)
            {
                verChars.Insert(0, "0");
            }

            List<String> verCharsList = new List<String>();

            verCharsList.AddRange(verChars.Select(c => c.ToString()).ToList());

            List<String> validIbanCharList = new List<String>();

            validIbanCharList.AddRange(ibanCharList);

            validIbanCharList[2] = verCharsList[0];
            validIbanCharList[3] = verCharsList[1];

            validIban = String.Join(String.Empty, validIbanCharList.ToArray());

            return validIban;
        }

        public String CheckIban()
        {
            String validIban = "Invalid";
            try
            {
                

                List<String> intermediateIban = new List<String>();

                String tableString = String.Empty;

                String verChars = String.Empty;

                intermediateIban.AddRange(ibanCharList);

                for (int i = 0; i <= 3; i++)
                {
                    intermediateIban.Add(intermediateIban[i]);
                }

                intermediateIban.RemoveRange(0, 4);


                foreach (var item in intermediateIban)
                {
                    if (!item.All(char.IsDigit))
                    {
                        foreach (KeyValuePair<String, String> tableItem in tableValues)
                        {
                            if (item.Equals(tableItem.Key))
                            {
                                tableString = tableString + tableItem.Value;
                            }

                        }
                    }
                    else
                    {
                        tableString = tableString + item;
                    }
                }

                BigInteger tableStringInt = BigInteger.Parse(tableString);
                BigInteger remainder = tableStringInt % 97;

                if (remainder == 1)
                {
                    validIban = "OK";
                }
                else
                {
                    validIban = "Invalid";
                }

               
            }

            catch
            {
                
            }
            return validIban;

        }
        public static void LoadTable(Dictionary<String, String> table)
        {

            var lines = File.ReadAllLines("table.ini");

            foreach (var line in lines)
            {
                String[] values = line.Split(',');

                table.Add(values[0], values[1]);

            }
        }
    }
}
