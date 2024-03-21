using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DxGridEditRow_HandleFocusEvents.Module.BusinessObjects;
using Microsoft.AspNetCore.Components;
using System.Transactions;

namespace DxGridEditRow_HandleFocusEvents.Blazor.Server.Components
{
    public partial class ProductsListComponent
    {
        [Parameter]
        public IEnumerable<Product> Data { get; set; }
        [Parameter]
        public EventCallback<Product> ItemClick { get; set; }
        [Parameter]
        public IObjectSpace ObjectSpace { get; set; }
        IGrid Grid { get; set; }

        async Task Grid_EditCanceling(GridEditCancelingEventArgs e)
        {
        }

        async Task Grid_CustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {
            if (e.IsNew)
            {
                e.EditModel = ObjectSpace.CreateObject<Product>();
            }
            else
            {
                e.EditModel = e.DataItem;
            }
        }

        async Task Grid_EditModelSaving(GridEditModelSavingEventArgs e)
        {
        }

        void EditProduct(GridColumnCellDisplayTemplateContext context)
        {
            Grid.StartEditRowAsync(context.VisibleIndex);
        }

        void AutoSave()
        {

        }

    }
}
