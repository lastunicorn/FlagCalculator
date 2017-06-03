using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowse
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length >= 1)
            {
                string assemblyFileName = args[0];

                //Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
                //Assembly assembly = Assembly.LoadFrom(assemblyFileName);
                Assembly assembly = Assembly.GetEntryAssembly();

                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    DisplayType(type);
                }

                Console.ReadKey(true);
            }
        }

        private static void DisplayAssemblyTypeMembers()
        {
            Type t = typeof(Assembly);

            MemberInfo[] members = t.GetMembers();

            foreach (MemberInfo memberInfo in members)
            {
                MethodInfo methodInfo = memberInfo as MethodInfo;

                if (methodInfo != null)
                {
                    IEnumerable<string> parametersAsString = methodInfo.GetParameters()
                        .Select(x => x.ParameterType.Name + " " + x.Name);

                    string parametersString = string.Join(", ", parametersAsString);

                    Console.WriteLine(methodInfo.MemberType + ": " + methodInfo.Name + "(" + parametersString + ")");
                }
                else
                {
                    Console.WriteLine(memberInfo.MemberType + ": " + memberInfo.Name);
                }
            }
        }

        static void DisplayType(Type type)
        {
            Console.WriteLine();

            if (type.IsClass)
            {
                DisplayTitle("Class: " + type.Name);
            }
            else if (type.IsEnum)
            {
                DisplayTitle("Enum: " + type.Name);
            }
            else if (type.IsInterface)
            {
                DisplayTitle("Interface: " + type.Name);
            }

            DisplayKeyValue("Full Name", type.FullName);
            DisplayKeyValue("Member Type", type.MemberType);
            DisplayKeyValue("Is Array", type.IsArray);
            DisplayKeyValue("Is Delegate", type.IsSubclassOf(typeof(Delegate)));
            DisplayKeyValue("Is Generic", type.IsGenericType);

            if (type.IsGenericType)
            {
                DisplayKeyValue("Is Generic Type Definition", type.IsGenericTypeDefinition);
            }

            MemberInfo[] memberInfos = type.GetMembers();

            foreach (MemberInfo memberInfo in memberInfos)
            {
                DisplayMemberInfo(memberInfo);
            }
        }

        private static void DisplayMemberInfo(MemberInfo memberInfo)
        {
            MethodInfo methodInfo = memberInfo as MethodInfo;

            if (methodInfo != null)
            {
                IEnumerable<string> parametersAsString = methodInfo.GetParameters()
                    .Select(x => x.ParameterType.Name + " " + x.Name);

                string parametersString = string.Join(", ", parametersAsString);

                Console.WriteLine(methodInfo.MemberType + ": " + methodInfo.Name + "(" + parametersString + ")");
            }
            else
            {
                Console.WriteLine(memberInfo.MemberType + ": " + memberInfo.Name);
            }
        }

        static void DisplayTitle(string text)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ForegroundColor = old;
        }

        static void DisplayKeyValue(string key, object value)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- " + key + ": ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ForegroundColor = old;
        }

        static void DisplayKeyValue(string key, bool value)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- " + key + ": ");
            Console.ForegroundColor = value ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(value);
            Console.ForegroundColor = old;
        }
    }

    /// <summary>
    /// The delegate is a class derived from Delegate class.
    /// </summary>
    internal delegate void ExampleDelegate();

    internal class ExampleClass
    {
        private ExampleGenericClass<int> fieldFromGenericClass;
    }

    internal sealed class ExampleGenericClass<T>
    {
    }
}
