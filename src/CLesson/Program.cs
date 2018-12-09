using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("请输入替换的路径:");
            var path = Console.ReadLine();
            Console.WriteLine("请输入需要被替换的字符串:");
            var oldValue = Console.ReadLine();
            Console.WriteLine("请输入新字符串:");
            var newValue = Console.ReadLine();
            Console.WriteLine("请输入字符编码(回车默认Windows为GBK，Linux和macOS为UTF-8，一般不需要改):");
            var encoding = Console.ReadLine();
            try
            {
                var defaultValue = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("config/config.json"));
                var e = string.IsNullOrWhiteSpace(encoding) ? Encoding.GetEncoding(0) : Encoding.GetEncoding(encoding);
                oldValue = !string.IsNullOrWhiteSpace(oldValue) ? oldValue :defaultValue.old;
                newValue = !string.IsNullOrWhiteSpace(newValue) ? newValue : defaultValue.@new;
                var program = new CProgram
                {
                    Encoding = e,
                    OldValue = e.GetBytes(oldValue),
                    NewValue = e.GetBytes(newValue)
                };
                program.ReplaceAll(path);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("输入的编码有误");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
