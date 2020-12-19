using System.Threading.Tasks;
using CreativeCoders.Core.Collections;
using CreativeCoders.SmartHal.Web.Api.Client.ControlCenter;
using CreativeCoders.SmartHal.Web.Api.Core.Models;

namespace CreativeCoders.SmartHal.Web.ControlCenter.Client.ViewModels
{
    public class ItemsViewModel
    {
        private readonly IItemsApi _itemsApi;

        public ItemsViewModel(IItemsApi itemsApi)
        {
            _itemsApi = itemsApi;
            ItemModels = new ExtendedObservableCollection<ItemModel>();
        }

        public async Task Refresh()
        {
            var items = await _itemsApi.GetItemsAsync();

            using (ItemModels.Update())
            {
                ItemModels.Clear();
                ItemModels.AddRange(items);
            }
        }

        public ExtendedObservableCollection<ItemModel> ItemModels { get; }
    }
}