using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace MilitaryElite
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Private> privatesMembers = new();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (cmdArgs[0] == "Private")
                {
                    Private privateMember = new(cmdArgs[1], cmdArgs[2], cmdArgs[3], decimal.Parse(cmdArgs[4]));
                    privatesMembers.Add(privateMember);
                    Console.WriteLine(privateMember);
                }
                else if (cmdArgs[0] == "Commando")
                {
                    try
                    {
                        List<(string, string)> missions = new List<(string, string)>();
                        for (int i = 6; i < cmdArgs.Length; i += 2)
                        {
                            string codeName = cmdArgs[i];
                            string state = cmdArgs[i + 1];
                            missions.Add((codeName, state));
                        }
                        Commando commando = new Commando(cmdArgs[1], cmdArgs[2], cmdArgs[3], decimal.Parse(cmdArgs[4]), cmdArgs[5], missions.ToArray());
                        Console.WriteLine(commando);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
                else if (cmdArgs[0] == "LieutenantGeneral")
                {
                    List<string> ids = new();
                    for (int i = 5; i < cmdArgs.Length; i++)
                    {
                        ids.Add(cmdArgs[i]);
                    }
                    LieutenantGeneral lieut = new(cmdArgs[1], cmdArgs[2], cmdArgs[3], decimal.Parse(cmdArgs[4]), ids.ToArray());
                    lieut.Privates = privatesMembers;
                    Console.WriteLine(lieut);
                }
                else if (cmdArgs[0] == "Engineer")
                {
                    try
                    {

                    List<(string, int)> repairs = new List<(string, int)>();
                    for (int i = 6; i < cmdArgs.Length; i += 2)
                    {
                        string repair = cmdArgs[i];
                        int hours = int.Parse(cmdArgs[i + 1]);
                        repairs.Add((repair, hours));

                    }
                    Engineer engineer = new(cmdArgs[1], cmdArgs[2], cmdArgs[3], decimal.Parse(cmdArgs[4]), cmdArgs[5], repairs.ToArray());
                    Console.WriteLine(engineer);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
                else if (cmdArgs[0] == "Spy")
                {
                    Spy spy = new(cmdArgs[1], cmdArgs[2], cmdArgs[3], decimal.Parse(cmdArgs[4]), cmdArgs[5], cmdArgs[6]);
                    Console.WriteLine(spy);
                }
            }
        }
    }
}