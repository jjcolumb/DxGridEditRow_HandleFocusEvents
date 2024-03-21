using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using DxGridEditRow_HandleFocusEvents.Module.BusinessObjects;
using System.ComponentModel;
using DevExpress.ExpressApp;
using System.Collections;

namespace DxGridEditRow_HandleFocusEvents.Blazor.Server.Editors.ProductListViewEditor
{
    [ListEditor(typeof(Product), true)]
    public class BlazorCustomListEditor : ListEditor, IComplexListEditor
    {
        XafApplication _application;
        CollectionSourceBase collectionSourceBase;
        public class ProductListViewHolder : IComponentContentHolder
        {
            private RenderFragment componentContent;
            public ProductListViewHolder(ProductsListViewModel componentModel)
            {
                ComponentModel =
                    componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            }
            private RenderFragment CreateComponent() =>
                ComponentModelObserver.Create(ComponentModel,
                                                ProductListViewRenderer.Create(ComponentModel));
            public ProductsListViewModel ComponentModel { get; }
            RenderFragment IComponentContentHolder.ComponentContent =>
                componentContent ??= CreateComponent();
        }
        public BlazorCustomListEditor(IModelListView model) : base(model) { }

        protected override object CreateControlsCore() =>
        new ProductListViewHolder(new ProductsListViewModel());

        protected override void AssignDataSourceToControl(object dataSource)
        {
            if (Control is ProductListViewHolder holder)
            {
                if (holder.ComponentModel.Data is IBindingList bindingList)
                {
                    bindingList.ListChanged -= BindingList_ListChanged;
                }
                holder.ComponentModel.Data =
                    (dataSource as System.Collections.IEnumerable)?.OfType<Product>().OrderBy(i => i.Name);
                holder.ComponentModel.ObjectSpace = collectionSourceBase.ObjectSpace;
                if (dataSource is IBindingList newBindingList)
                {
                    newBindingList.ListChanged += BindingList_ListChanged;
                }
            }
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            Refresh();
        }

        private Product[] selectedObjects = Array.Empty<Product>();
        protected override void OnControlsCreated()
        {
            if (Control is ProductListViewHolder holder)
            {
                holder.ComponentModel.ItemClick += ComponentModel_ItemClick;
            }
            base.OnControlsCreated();
        }
        // ...
        private void ComponentModel_ItemClick(object sender,
                                                ProductListViewModelItemClickEventArgs e)
        {
            selectedObjects = new Product[] { e.Item };
            OnSelectionChanged();
            OnProcessSelectedItem();
        }

        public override void BreakLinksToControls()
        {
            if (Control is ProductListViewHolder holder)
            {
                holder.ComponentModel.ItemClick -= ComponentModel_ItemClick;
            }
            AssignDataSourceToControl(null);
            base.BreakLinksToControls();
        }
        public override void Refresh()
        {
            if (Control is ProductListViewHolder holder)
            {
                holder.ComponentModel.Refresh();
            }
        }
        public override SelectionType SelectionType => SelectionType.Full;
        public override IList GetSelectedObjects() => selectedObjects;

        public void Setup(CollectionSourceBase collectionSource, XafApplication application)
        {
            this._application = application;
            this.collectionSourceBase = collectionSource;

        }
    }
}
