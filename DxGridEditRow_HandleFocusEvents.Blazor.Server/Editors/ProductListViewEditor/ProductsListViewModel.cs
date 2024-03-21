using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DxGridEditRow_HandleFocusEvents.Module.BusinessObjects;

namespace DxGridEditRow_HandleFocusEvents.Blazor.Server.Editors.ProductListViewEditor
{
    public class ProductsListViewModel: ComponentModelBase
    {

        public IEnumerable<Product> Data
        {
            get => GetPropertyValue<IEnumerable<Product>>();
            set => SetPropertyValue(value);
        }

        public IObjectSpace ObjectSpace { get; set; }

        public void Refresh() => RaiseChanged();
        public void OnItemClick(Product item) =>
            ItemClick?.Invoke(this, new ProductListViewModelItemClickEventArgs(item));
        public event EventHandler<ProductListViewModelItemClickEventArgs> ItemClick;
    }
    public class ProductListViewModelItemClickEventArgs : EventArgs
    {
        public ProductListViewModelItemClickEventArgs(Product item)
        {
            Item = item;
        }
        public Product Item { get; }
    }
}
