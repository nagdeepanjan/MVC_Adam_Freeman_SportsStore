using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IStoreRepository _repository;

        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            repository = _repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
