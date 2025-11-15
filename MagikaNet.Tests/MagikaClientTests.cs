namespace MagikaNet.Tests;

public class MagikaClientTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CanDetectJpegFile()
    {
        using var m = new MagikaClient();

        var d = m.DetectPath("samples/forest.jpg");
        
        Assert.That(d, Contains.Substring("\"output\":\"jpeg\""));
    }
}