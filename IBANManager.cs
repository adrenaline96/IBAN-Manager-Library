using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace IBAN_Manager
{
    public class IBANManager
    {
        private List<string> ibanCharList = new List<string>(34);
        private string iban = String.Empty;

        private static Dictionary<string, string> tableValues = new Dictionary<string, string>();

        public IBANManager(string ibanCode)
        {
            ibanCode.Replace(" ", String.Empty);

            iban = ibanCode;

            ibanCharList.AddRange(ibanCode.Select(c => c.ToString()).ToList());


        }
        static IBANManager()
        {
            Table.LoadTable(tableValues);
        }
        public string GetIban()
        {
            return iban;
        }
        public string GenerateValidIban()
        {
            string validIban = String.Empty;

            List<string> intermediateIban = new List<string>();

            StringBuilder tableString = new StringBuilder();

            string verChars = String.Empty;

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
                    foreach (KeyValuePair<string, string> tableItem in tableValues)
                    {
                        if (item.Equals(tableItem.Key))
                        {
                            tableString.Append(tableItem.Value);
                        }

                    }
                }
                else
                {
                    tableString.Append(item);
                }
            }

            BigInteger tableStringInt = BigInteger.Parse(tableString.ToString());
            BigInteger remainder = tableStringInt % 97;

            verChars = Convert.ToString(98 - remainder);

            if (verChars.Length < 2)
            {
                verChars.Insert(0, "0");
            }

            List<string> verCharsList = new List<string>();

            verCharsList.AddRange(verChars.Select(c => c.ToString()).ToList());

            List<string> validIbanCharList = new List<string>();

            validIbanCharList.AddRange(ibanCharList);

            validIbanCharList[2] = verCharsList[0];
            validIbanCharList[3] = verCharsList[1];

            validIban = String.Join(String.Empty, validIbanCharList.ToArray());

            return validIban;
        }

        public string CheckIban()
        {
            string validIban = "Invalid";
            try
            {
                

                List<string> intermediateIban = new List<string>();

                StringBuilder tableString = new StringBuilder();

                string verChars = String.Empty;

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
                        foreach (KeyValuePair<string, string> tableItem in tableValues)
                        {
                            if (item.Equals(tableItem.Key))
                            {
                                tableString.Append(tableItem.Value);
                            }

                        }
                    }
                    else
                    {
                        tableString.Append(item);
                    }
                }

                BigInteger tableStringInt = BigInteger.Parse(tableString.ToString());
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
    }
}
