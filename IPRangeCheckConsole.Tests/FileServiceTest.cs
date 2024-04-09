using IPRangeCheckConsole.Services;

namespace IPRangeCheckConsole.Tests
{
    public class FileServiceTest
    {
        private FileService fileService;
        public FileServiceTest()
        {
            fileService = new FileService();
        }
        [Fact]
        public void ReadAsync_GetFilePath_ThrowInvalidDataException()
        {
            string path = @"TESC:\\Users\X\Win\Dir\Win\Dir\Dir\n\fDZ4bDG+tJzWY78yjYOnBg.txt";
            ;
            Assert.ThrowsAsync<InvalidDataException>(async () =>
            {
                await foreach (var item in fileService.ReadAsync(path)) ;
                
            });
        }
    }
}
