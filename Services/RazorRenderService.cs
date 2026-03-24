using RazorLight;
using System.IO;
using System.Threading.Tasks;

namespace Game106.Backend.Services
{
    public interface IRazorRenderService
    {
        // Render một template cshtml ra chuỗi HTML dựa trên model truyền vào
        Task<string> RenderTemplateAsync<TModel>(string templateName, TModel model);
    }

    public class RazorRenderService : IRazorRenderService
    {
        private readonly IRazorLightEngine _engine;

        public RazorRenderService()
        {
            // Cấu hình Engine để đọc tệp từ thư mục "Templates" ở gốc dự án
            string templateFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
            
            _engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(templateFolderPath)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderTemplateAsync<TModel>(string templateName, TModel model)
        {
            // templateName ở đây là tên file, ví dụ: "WelcomeEmail.cshtml"
            return await _engine.CompileRenderAsync(templateName, model);
        }
    }
}
