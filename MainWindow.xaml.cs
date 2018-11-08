using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Effects;

using Task3.BL;
using Task3.Classes;
using Task3.Classes.AppointmentData;

namespace Task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Holds an id number of the next order.
        /// </summary>
        private long _nextId;

        /// <summary>
        /// Contains information of current creating/editing order.
        /// </summary>
        private Appointment _appointment;

        /// <summary>
        /// Validator instance.
        /// </summary>
        private readonly Validator _validator;

        /// <summary>
        /// An object for storage connection.
        /// </summary>
        private readonly AppointmentsStorage _storage;

        /// <summary>
        /// Parameterless constructor of application's main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            EditOrderButton.IsEnabled = false;
            DeletOrderButton.IsEnabled = false;
            try
            {
                _storage = new AppointmentsStorage("storage.xml");
                _storage.CreateIfNotExists();
                
                var ordersList = _storage.RetrieveAllIds();
                _nextId = ordersList.Count != 0 ? ordersList.Keys.Last() + 1 : 0;
            }
            catch (NullReferenceException e)
            {
                Util.Error("Storage fatal error", e.Message);
                Application.Current.Shutdown();
            }

            /*_validator = new Validator(
                new List<TextBox>
                {
                    FirstName,
                    LastName,
                    Email,
                    PhoneNumber,
                    ClientAddressCity,
                    ClientAddressStreet,
                    ClientAddressBuildingNumber,
                    ShopName,
                    ShopAddressCity,
                    ShopAddressStreet,
                    ShopAddressBuildingNumber,
                    GoodsCode,
                    GoodsWeight
                },
                Email,
                PhoneNumber);*/
            ResetOrderInstance();
        }

       

        /// <summary>
        /// Fires when user opens pop-up window with a list of available oredrs.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void ExploreOrders(object sender, RoutedEventArgs e)
        {
            var orders = _storage.RetrieveAllIds();
            if (orders.Count > 0)
            {
                OrdersList.ItemsSource = orders;
                OrdersExplorer.IsOpen = true;
                //ResetOrderInstance();
                WindowMain.IsEnabled = false;
                EditOrderButton.IsEnabled = false;
                DeletOrderButton.IsEnabled = false;
                Opacity = 0.5;
                Effect = new BlurEffect();
            }
            else
            {
                Util.Info("Explore orders", "Orders database is empty!");
            }
        }

        /// <summary>
        /// Fires when user presses 'Edit' button on pop-up window.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void SetTargetEditingOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrdersList.SelectedItems.Count == 1)
                {
                    var selectedItem = (dynamic)OrdersList.SelectedItems[0];
                    _appointment = _storage.Retrieve(selectedItem.Key);
                    DataContext = _appointment;
                }
            }
            catch (Exception exc)
            {
                Util.Error("Can't set order for editing", exc.Message);
            }

            OrdersList.SelectedItem = null;
            OrdersExplorer.IsOpen = false;
            EditOrderButton.IsEnabled = false;
            DeletOrderButton.IsEnabled = false;
            WindowMain.IsEnabled = true;
            Opacity = 1;
            Effect = null;
        }

        /// <summary>
        /// Fires when user rejects pop-up window by pressing 'Cancel' button.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            OrdersExplorer.IsOpen = false;
            EditOrderButton.IsEnabled = false;
            DeletOrderButton.IsEnabled = false;
            WindowMain.IsEnabled = true;
            Opacity = 1;
            Effect = null;
        }

        /// <summary>
        /// Fires when user creates or updates an order.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void SaveOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                _validator.Validate();
                if (_appointment.Id == -1)
                {
                    _appointment.Id = _nextId++;
                    _storage.Add(_appointment);
                    ResetOrderInstance();
                }
                else
                {
                    _storage.Update(_appointment.Id, _appointment);
                }

                Util.Info("Cargo Delivery", "An order was saved successfully!");
            }
            catch (Exception exc)
            {
                Util.Error("Order error", exc.Message);
            }
        }

        /// <summary>
        /// Fires when user selects some input field.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void InputFocused(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
            {
                textBox.SelectAll();
            }
        }

        /// <summary>
        /// Fires when user presses on items in pop-up window's list view.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void ItemIsSelected(object sender, RoutedEventArgs e)
        {
            EditOrderButton.IsEnabled = true;
            DeletOrderButton.IsEnabled = true;
        }

        /// <summary>
        /// Fires when user resets input fields for creating new order by pressing 'New' button.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void NewOrder(object sender, RoutedEventArgs e)
        {
            ResetOrderInstance();
        }

        /// <summary>
        /// Fires when user deletes order by pressing 'Delete' button on pop-up window.
        /// </summary>
        /// <param name="sender">The button New that the action is for.</param>
        /// <param name="e">Arguments that the implementor of this event may find useful.</param>
        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrdersList.SelectedItems.Count != 1)
                {
                    return;
                }

                var selectedItem = (dynamic)OrdersList.SelectedItems[0];
                _storage.Remove(selectedItem.Key);
                OrdersList.SelectedItem = null;
                EditOrderButton.IsEnabled = false;
                DeletOrderButton.IsEnabled = false;
                var orders = _storage.RetrieveAllIds();
                if (orders.Count < 1)
                {
                    OrdersExplorer.IsOpen = false;
                    Opacity = 1;
                    Effect = null;
                    WindowMain.IsEnabled = true;
                }
                else
                {
                    OrdersList.ItemsSource = orders;
                }
            }
            catch (Exception exc)
            {
                Util.Error("Order deleting error", exc.Message);
            }
        }

        /// <summary>
        /// Resets _appointment field: set _appointment to new Order with id = -1.
        /// </summary>
        private void ResetOrderInstance()
        {
            _appointment = new  Appointment();
            _appointment.Id = -1;
            DataContext = _appointment;
        }
    }
}