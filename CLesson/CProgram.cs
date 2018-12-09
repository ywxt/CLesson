using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CLesson
{
    public class CProgram
    {
        public byte[] OldValue { get; set; }
        public byte[] NewValue { get; set; }
        public Encoding Encoding { get; set; }

        public void Replace(string path)
        {
            using (var file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                do
                {
                    var current = file.Position;
                    var ch = file.ReadByte();
                    if (ch != OldValue[0]) continue;
                    var flag = true;
                    for (int i = 1; i < OldValue.Length; i++)
                    {
                        if (OldValue[i] == file.ReadByte()) continue;
                        flag = false;
                        break; //如果不相等则跳出
                    }

                    if (!flag) continue;
                    file.Position = current;
                    file.Write(NewValue);
                    file.Position += NewValue.Length;
                } while (file.Position < file.Length);


            }
        }

        public void ReplaceAll(string path)
            {
                try
                {
                    var fileNames = Directory.GetFiles(path);
                    foreach (var file in fileNames)
                    {
                        this.Replace(file);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }