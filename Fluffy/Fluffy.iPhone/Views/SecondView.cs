using System;

using UIKit;

namespace Fluffy.iPhone.Views
{
    using System.Collections.Generic;
    using Cells;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Binding.Touch.Views;
    using Cirrious.MvvmCross.Touch.Views;
    using Common.Entities;
    using Foundation;
    using ViewModels;

    public partial class SecondView : MvxViewController
    {
        public SecondView() : base("SecondView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var dataSource = new TableViewSource(this.TableView);

            var bindings = this.CreateBindingSet<SecondView, SecondViewModel>();
            bindings.Bind(dataSource).To(vm => vm.Users);
            bindings.Bind(dataSource).For(s => s.SelectionChangedCommand).To(vm => vm.UserItemClickCommand).Apply();
            bindings.Bind(this.InitVariables).To(vm => vm.UpdateString);
            bindings.Bind(this.ActivityIndicator).For("Visibility").To(vm => vm.IsLoading).WithConversion("Visibility");
            bindings.Apply();

            this.TableView.Source = dataSource;
            this.TableView.ReloadData();
        }

    }


    public class TableViewSource : MvxSimpleTableViewSource
    {
        public TableViewSource(UITableView tableView) : base(tableView, UserCell.Identifier, UserCell.Identifier)
        {
            tableView.RegisterNibForCellReuse(UINib.FromName("UserCell", NSBundle.MainBundle), UserCell.Identifier);
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var item = (User)this.GetItemAt(indexPath);

            if (item is User)
            {
                return 95.0f;
            }
            else
            {

            }

            //var item = GetItemAt(indexPath);
            //return UserCell.CellHeight(item);

            return base.GetHeightForRow(tableView, indexPath);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cellName = UserCell.Identifier;
            return tableView.DequeueReusableCell(cellName, indexPath);
        }
    }
}