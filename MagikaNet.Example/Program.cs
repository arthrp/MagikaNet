namespace MagikaNet.Example;

class Program
{
    static void Main(string[] args)
    {
        using var m = new MagikaClient();

        var x = m.DetectPath("/tmp/example.jpg");
        
        Console.WriteLine(x);
    }
}