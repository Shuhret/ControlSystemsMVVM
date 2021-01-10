using ControlSystemsLibrary.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Security;
using System.Windows.Media;
using ControlSystemsLibrary.Controls;
using System.Threading;
using System.Threading.Tasks;
using ControlSystemsLibrary.Services;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;

namespace ControlSystemsLibrary.ViewModels
{
    class AuthorizationVM : ViewModelBase
    {
        public AuthorizationVM()
        {
            StartMethod();
        }

        


        private string currentUserName = "";
        public string CurrentUserName
        {
            get => currentUserName;
            set
            {
                currentUserName = value;
                
                OnPropertyChanged();
            }
        }






        private bool checkcreatedConnectionResult = false;
        public bool CheckcreatedConnectionResult
        {
            get => checkcreatedConnectionResult;
            set
            {
                checkcreatedConnectionResult = value;
                if(value == true)
                {
                    MessageText = "Соединение установлено!";
                    MessageTextColor = new SolidColorBrush((Color)Application.Current.FindResource("Green-002"));
                    SaveCreatedConnectionButtonEnabled = true;
                }
                else
                {
                    SaveCreatedConnectionButtonEnabled = false;
                    MessageTextColor = new SolidColorBrush((Color)Application.Current.FindResource("Red-001"));
                }
                
                LoaderUC = null;
                OnPropertyChanged();
            }
        }


        private string messageText = "";
        public string MessageText
        {
            get => messageText;
            set
            {
                messageText = value;
                OnPropertyChanged();
            }
        }


        private SolidColorBrush messageTextColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush MessageTextColor
        {
            get => messageTextColor;
            set
            {

                    messageTextColor = value;

                OnPropertyChanged();
            }
        }


        private SolidColorBrush currentConnectionTextColor = GetColor.Get("Dark-003");
        public SolidColorBrush CurrentConnectionTextColor
        {
            get => currentConnectionTextColor;
            set
            {

                currentConnectionTextColor = value;

                OnPropertyChanged();
            }
        }



        SqlConnectionStringBuilder ConnectionStringBuilder = new SqlConnectionStringBuilder();

        private string currentConnectionName = "Не создано!";
        public string CurrentConnectionName
        {
            get => currentConnectionName;
            set
            {
                currentConnectionName = value;
                if (value == "Не создано!")
                {
                    CurrentConnectionTextColor = GetColor.Get("Red-001");
                }
                else
                {

                }
                OnPropertyChanged();
            }
        }

        private string createdConnectionStringName = "";
        public string CreatedConnectionStringName
        {
            get => createdConnectionStringName;
            set
            {
                createdConnectionStringName = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        private string createdConnectionStringServer = "";
        public string CreatedConnectionStringServer
        {
            get => createdConnectionStringServer;
            set
            {
                createdConnectionStringServer = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        private string createdConnectionStringDataBase = "";
        public string CreatedConnectionStringDataBase
        {
            get => createdConnectionStringDataBase;
            set
            {
                createdConnectionStringDataBase = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        private string createdConnectionStringUserID = "";
        public string CreatedConnectionStringUserID
        {
            get => createdConnectionStringUserID;
            set
            {
                createdConnectionStringUserID = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        private string createdConnectionStringPassword = "";
        public string CreatedConnectionStringPassword
        {
            get => createdConnectionStringPassword;
            set
            {
                createdConnectionStringPassword = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        private string createdConnectionString = "";
        public string CreatedConnectionString
        {
            get => createdConnectionString;
            set
            {
                createdConnectionString = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        private bool? connectionStringMode = false;
        public bool? ConnectionStringMode
        {
            get => connectionStringMode;
            set
            {
                connectionStringMode = value;

                if (value == true)
                {
                    ConnectionStringFalseVisibility = Visibility.Collapsed;
                    ConnectionStringTrueVisibility = Visibility.Visible;

                }
                else
                {
                    ConnectionStringFalseVisibility = Visibility.Visible;
                    ConnectionStringTrueVisibility = Visibility.Collapsed;
                }
                CheckcreatedConnectionResult = false;
                ClearCreatedValues();
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }



        public ICommand ClickShowConnectionList
        {
            get
            {
                return new DelegateCommand((obj) => 
                { 
                    ShowConnectionList();
                    MessageText = "";
                });
            }
        }
        public ICommand ClickShowAuthorization
        {
            get
            {
                return new DelegateCommand((obj) => 
                { 
                    ShowAuthorization();
                    MessageText = "";
                });
            }
        }
        public ICommand ClickShowCreateConnection
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    ShowCreateConnection();
                    MessageText = "";
                });
            }
        }

        public ICommand ClickCheckCreatedConnection
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    CheckCreatedConnection();
                    MessageText = "";

                });
            }
        }

        public ICommand PasteCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    PasteConnectionString();
                });
            }
        }







        private bool checkCreatedConnectionButtonEnabled = false;
        public bool CheckCreatedConnectionButtonEnabled
        {
            get => checkCreatedConnectionButtonEnabled;
            set
            {
                checkCreatedConnectionButtonEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool saveCreatedConnectionButtonEnabled = false;
        public bool SaveCreatedConnectionButtonEnabled
        {
            get => saveCreatedConnectionButtonEnabled;
            set
            {
                saveCreatedConnectionButtonEnabled = value;
                OnPropertyChanged();
            }
        }


        private UserControl loader = null;
        public UserControl LoaderUC
        {
            get => loader;
            set
            {
                loader = value;
                OnPropertyChanged();
            }
        }

        private Visibility authorizationVisibility = Visibility.Visible;
        public Visibility AuthorizationVisibility
        {
            get => authorizationVisibility;
            set
            {
                authorizationVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility connectionListVisibility = Visibility.Collapsed;
        public Visibility ConnectionListVisibility
        {
            get => connectionListVisibility;
            set
            {
                connectionListVisibility = value;
                if(value == Visibility.Visible)
                {
                    LoadAllConnections();
                }
                OnPropertyChanged();
            }
        }

        private Visibility createConnectionVisibility = Visibility.Collapsed;
        public Visibility CreateConnectionVisibility
        {
            get => createConnectionVisibility;
            set
            {
                createConnectionVisibility = value;
                OnPropertyChanged();
            }
        }


        private Visibility connectionStringTrueVisibility = Visibility.Collapsed;
        public Visibility ConnectionStringTrueVisibility
        {
            get => connectionStringTrueVisibility;
            set
            {
                connectionStringTrueVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility connectionStringFalseVisibility = Visibility.Visible;
        public Visibility ConnectionStringFalseVisibility
        {
            get => connectionStringFalseVisibility;
            set
            {
                connectionStringFalseVisibility = value;
                OnPropertyChanged();
            }
        }


        //---
        private ObservableCollection<ConnectionRB> connections;
        
        public ObservableCollection<ConnectionRB> Connections
        {
            get
            {
                if (connections == null)
                    connections = new ObservableCollection<ConnectionRB>();
                return connections;
            }
        }


        #region МЕТОДЫ ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        private void StartMethod()
        {
            LoadCurrentUserName();
            LoadCurrentConnectionName();
        }

        async void LoadCurrentConnectionName()
        {
            CurrentConnectionName = await Task.Run(XmlClass.GetSelectedConnectionName);
            if (CurrentConnectionName == "")
            {
                CurrentConnectionName = "Не создано!";
            }
        }

        async void LoadAllConnections()
        {
            LoaderUC = new Loader();
            Connections.Clear();
            LoadCurrentConnectionName();
            ArrayList array = await Task.Run(XmlClass.ReadAllConnectionsName);
            if (array.Count > 0)
            {
                foreach (string str in array)
                {
                    ConnectionRB connectionRB = new ConnectionRB();
                    connectionRB.GroupName = "ConnGroup";
                    connectionRB.Content = str;
                    connectionRB.Deleted += ConnectionRB_Deleted;
                    connectionRB.Checked += SelectConnection;
                    if (str == CurrentConnectionName)
                    {
                        connectionRB.IsChecked = true;
                    }
                    Connections.Add(connectionRB);
                }
            }
            else
            {
                ShowCreateConnection();
                CurrentConnectionName = "Не создано!";
            }
            LoaderUC = null;
        }

        async void SelectConnection(object sender, RoutedEventArgs e)
        {
            CurrentConnectionName =  (sender as ConnectionRB).Content.ToString();
            await Task.Run(() => XmlClass.SetSelectConnection(CurrentConnectionName));
        }

        private void ConnectionRB_Deleted(object sender, EventArgs e)
        {
            foreach(ConnectionRB CRB in Connections)
            {
                if(CRB.Content == (sender as ConnectionRB).Content)
                {
                    bool isCheck = (bool)CRB.IsChecked;
                    DeleteConnectionFromXmlFile(CRB.Content.ToString());
                    Connections.Remove(CRB);
                    if (isCheck && Connections.Count >= 1)// если удален выбранный и есть еще
                    {
                        XmlClass.DeSelectAllConnections();
                        Connections[Connections.Count - 1].IsChecked = true;
                    }
                    if(Connections.Count == 0)
                    {
                        CurrentConnectionName = "Не создано!";
                        CurrentConnectionTextColor = GetColor.Get("Red-001");
                    }
                    break;
                }
            }
        }

        void DeleteConnectionFromXmlFile(string DeletedConnectionName)
        {
            XmlClass.DeleteConnection(DeletedConnectionName);
        }

        void ShowAuthorization()
        {
            AuthorizationVisibility = Visibility.Visible;
            ConnectionListVisibility = Visibility.Collapsed;
            CreateConnectionVisibility = Visibility.Collapsed;
        }

        void ShowConnectionList()
        {
            AuthorizationVisibility = Visibility.Collapsed;
            ConnectionListVisibility = Visibility.Visible;
            CreateConnectionVisibility = Visibility.Collapsed;
        }

        void ShowCreateConnection()
        {
            AuthorizationVisibility = Visibility.Collapsed;
            ConnectionListVisibility = Visibility.Collapsed;
            CreateConnectionVisibility = Visibility.Visible;
            ConnectionStringMode = false;
        }

        void ClearCreatedValues()
        {
            if (ConnectionStringMode == true)
            {
                CreatedConnectionStringServer = "";
                CreatedConnectionStringDataBase = "";
                CreatedConnectionStringUserID = "";
                CreatedConnectionStringPassword = "";
            }
            else
            {
                CreatedConnectionString = "";
            }
            CheckCreatedValues();
        }

        void CheckCreatedValues()
        {
            if (ConnectionStringMode == true)
            {
                if (CreatedConnectionStringName != "" && CreatedConnectionString != "")
                {
                    CheckCreatedConnectionButtonEnabled = true;
                }
                else
                {
                    CheckCreatedConnectionButtonEnabled = false;
                }
            }
            else
            {
                if (CreatedConnectionStringName != "" && CreatedConnectionStringServer != "" && CreatedConnectionStringDataBase != "" && CreatedConnectionStringUserID != "" && CreatedConnectionStringPassword != "")
                {
                    CheckCreatedConnectionButtonEnabled = true;
                }
                else
                {
                    CheckCreatedConnectionButtonEnabled = false;
                }
            }
            MessageText = "";
        }

        async void CheckCreatedConnection()
        {
            LoaderUC = new Loader();
            MessageText = "Установка соединения...";
            MessageTextColor = new SolidColorBrush((Color)Application.Current.FindResource("Blue-003"));
            CheckCreatedConnectionButtonEnabled = false;
            try
            {
                if (ConnectionStringMode == true)
                {
                    ConnectionStringBuilder.ConnectionString = CreatedConnectionString;
                }
                else
                {
                    ConnectionStringBuilder.DataSource = CreatedConnectionStringServer;
                    ConnectionStringBuilder.InitialCatalog = CreatedConnectionStringDataBase;
                    ConnectionStringBuilder.UserID = CreatedConnectionStringUserID;
                    ConnectionStringBuilder.Password = CreatedConnectionStringPassword;
                }
                CheckcreatedConnectionResult = await Task.Run(OpenCloseConnection); 
            }
            catch (Exception ex)
            {
                LoaderUC = null;
                MessageText = ex.Message;
                MessageTextColor = new SolidColorBrush((Color)Application.Current.FindResource("Red-001"));
            }
        }

        bool OpenCloseConnection()
        {
            bool ok = false;

            using (SqlConnection conn = new SqlConnection(ConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    conn.Open();
                    ok = true;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageText = ex.Message;
                    ok = false;
                }
            }
            return ok;
        }


        async void LoadCurrentUserName()
        {
            CurrentUserName = await Task.Run(XmlClass.GetCurrentUserName);
        }


        void PasteConnectionString()
        {
            CreatedConnectionString = Clipboard.GetText();
        }
        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    }
}
