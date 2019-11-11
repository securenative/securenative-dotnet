using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using static SecureNative.SDK.PlayHooky;

namespace SecureNative.SDK
{
    public class Agent
    {

        [ClassInitialize]
        public void Setup()
        {
           
        }
        public static string GetDependencies()
        {
            var indent = 0;
            var dependecies = new List<string>();
            var dependecies1 = new List<string>();
            var dependecies2 = new List<string>();
            var dependecies3 = new List<string>();
            var dependecies4 = new List<string>();


            Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            
            foreach (AssemblyName an in a.GetReferencedAssemblies())
            {
                dependecies.Add(an.FullName);
            }
            foreach (Assembly b in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Module m in b.GetModules(true))
                {
                    dependecies1.Add(m.Name);
                }
                foreach (Type t in b.GetExportedTypes())
                {
                    dependecies2.Add(t.FullName);
                    foreach (MemberInfo mi in t.GetMembers())
                    {
                        if (mi.MemberType == MemberTypes.Method)
                        {
                            foreach (ParameterInfo pi in ((MethodInfo)mi).GetParameters())
                            {
                                dependecies3.Add(pi.Name);
                            }
                        }
                        if (mi.MemberType == MemberTypes.Property)
                        {
                            foreach (MethodInfo am in ((PropertyInfo)mi).GetAccessors())
                            {
                                dependecies4.Add(am.Name);
                            }
                        }
                    }
                }
            }
            return "";
        }
   
        public static void ChangeMethod(object cls, object cls1, string methodToReplace, string newMethod)

            {
                try
                {
                    HookManager manager = new HookManager();
                    manager.Hook(cls.GetType().GetMethod(methodToReplace), cls1.GetType().GetMethod(newMethod));
                }
            
                catch (Exception e)
                {
                    Console.Error.WriteLine("Unable to hook method, : " + e);
                }
            }
        }
    }

