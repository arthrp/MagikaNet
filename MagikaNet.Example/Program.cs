using System.Text;
using System.Text.Unicode;

namespace MagikaNet.Example;

class Program
{
    static void Main(string[] args)
    {
        using var m = new MagikaClient();

        var str = "#!/bin/sh\necho hello\n";
        var y = m.DetectBytes(Encoding.UTF8.GetBytes(str));
        Console.WriteLine(y);
    }
}