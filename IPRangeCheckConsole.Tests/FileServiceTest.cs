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
        [Theory]
        [InlineData(@"C:\Users\X\Document\Images\MzQ4NXR5NGJnNDUzIHY0dXIzbmhkLDAyLTM5aXIzN2g0dGdtdjM4NGh0NQ\ex.txt")]
        [InlineData(@"C:\Users\X\Document\Images\")]
        public async void ReadAsync_GetFilePath_ThrowInvalidDataException(string path)
        {
            
            await Assert.ThrowsAsync<InvalidDataException>(async () =>
            {
                await foreach (var item in fileService.ReadAsync(path)) ;
                
            });
        }

        [Theory]
        [InlineData(@"Users\X\Documents\Images\Example\ex.txt")]
        [InlineData(@"C:\Users\X\?Document\_Images\ex<.txt")]
        [InlineData(@"C:\Users\X\Document\Images\MzQ4NXR5NGJnNDUzIHY0dXIzbmhkLDAyLTM5aXIzN2g0dGdtdjM4NGh0NQ\ex.txt")]
        [InlineData(@"C:\Users\X\Document\Images\")]
        public async void  WriteAsync_GetFilePath_ThrowInvalidDataException(string path)
        {
            
            await Assert.ThrowsAsync<InvalidDataException>(async () =>
            {
                await fileService.WriteAsync(path, null!);

            });
        }
    }
}
