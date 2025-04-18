using ModelHexa;
namespace TestProject2
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Board b=new Board(3);
            Assert.True(b.GetCell(0, 0).pawn.HasValue);
        }
    }
}
