using Microsoft.AspNetCore.Mvc;

namespace ActionResultDemo.Components
{
    public class MyComponentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Default", "This is content from ViewComponent");
        }
    }
}
