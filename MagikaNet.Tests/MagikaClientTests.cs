using System.Text;

namespace MagikaNet.Tests;

public class MagikaClientTests
{
    [Test]
    public void JpegFile_DetectedSuccessfully()
    {
        using var m = new MagikaClient();

        var d = m.DetectPath("samples/forest.jpg");
        
        Assert.That(d, Contains.Substring("\"output\":\"jpeg\""));
    }

    [Test]
    public void ShellBytes_DetectedSuccessfully()
    {
        using var m = new MagikaClient();

        var str = "#!/bin/sh\necho hello\n";
        var d = m.DetectBytes(Encoding.UTF8.GetBytes(str));
        
        Assert.That(d, Contains.Substring("\"output\":\"shell\""));
    }

    [Test]
    public void EmptyBytes_ReturnsEmptyOutput()
    {
        using var m = new MagikaClient();
        byte[] bytes = { };

        var d = m.DetectBytes(bytes);
        Assert.That(d, Contains.Substring("\"output\":\"empty\""));
    }
}