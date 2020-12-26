using System;
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

        public async Task RefreshAsync()
        {
            var items = await _itemsApi.GetItemsAsync();

            using (ItemModels.Update())
            {
                ItemModels.Clear();
                ItemModels.AddRange(items);
            }
        }

        public async Task SendCommandAsync(ItemModel itemModel)
        {
            try
            {
                await _itemsApi.SendCommandAsync(new SendCommandModel { CommandValue = itemModel.Value, ItemName = itemModel.Name });
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to send command. ItemName = '{itemModel.Name}', CommandValue = '{itemModel.Value}'");
            }
        }

        public ExtendedObservableCollection<ItemModel> ItemModels { get; }
    }
}