using System;
using System.Runtime.InteropServices;
using System.Linq;
using Microsoft.Windows.Sdk;
using Calculation;

namespace CsApp
{
    class Program
    {
        public struct MyStruct
        {
            public int a, b;
        };

        public const string CppFuncDll = @"..\..\..\..\..\CppApp\x64\Debug\CppApp.dll";

        [DllImport(CppFuncDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddNumbers(int a, int b);

        [DllImport(CppFuncDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SubtractNumbers(int a, int b);

        [DllImport(CppFuncDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddStruct(MyStruct vals);

        [DllImport(CppFuncDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SubtractStruct(MyStruct vals);

        static void Main(string[] args)
        {
            int input1, input2;
            Console.Write("Input number 1: ");
            if (!int.TryParse(Console.ReadLine(), out input1))
            {
                Console.WriteLine("Bad input. Setting input1 to 10");
                input1 = 10;
            }

            Console.Write("Input number 2: ");
            if (!int.TryParse(Console.ReadLine(), out input2))
            {
                Console.WriteLine("Bad input. Setting input2 to 10");
                input2 = 10;
            }

            //int sum = AddNumbers(input1, input2);
            //int diff = SubtractNumbers(input1, input2);

            MyStruct ms = new MyStruct();
            ms.a = input1;
            ms.b = input2;

            //int sum = AddStruct(ms);
            //int diff = SubtractStruct(ms);

            var cal = new Calculate();

            int sum = cal.Add(input1, input2);
            int diff = cal.Subtract(input1, input2);

            Console.WriteLine($"Sum is: {sum}");
            Console.WriteLine($"Difference is: {diff}");

            Console.ReadLine();

            using var handle = PInvoke.FindFirstFile("*.*", out var findData);
            if (handle.IsInvalid)
                return;
            bool result;
            do
            {
                Console.WriteLine(ConvertFileNameToString(findData.cFileName.AsSpan()));
                result = PInvoke.FindNextFile(handle, out findData);
            } while (result);
            string ConvertFileNameToString(Span<ushort> span)
            {
                return string.Join("", span.ToArray().TakeWhile(i => i != 0).Select(i => (char)i));
            }
        }
    }
}
