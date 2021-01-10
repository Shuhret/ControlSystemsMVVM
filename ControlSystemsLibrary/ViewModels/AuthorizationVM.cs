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
        // КОНСТРУКТОР ------------------------------------------------------------------------------------------------------
        public AuthorizationVM()
        {
            StartMethod();
        }


        #region Свойства и поля =============================================================================================

        // Имя текущего пользователя ----------------------------------------------------------------------------------------
        private string currentUserName = "";
        public string CurrentUserName
        {
            get => currentUserName;
            set
            {
                if (Equals(currentUserName, value)) return;
                currentUserName = value;
                OnPropertyChanged();
            }
        }


        // Текущая строка подключения (зашифровано) -------------------------------------------------------------------------
        private string currentCryptConnectionString = "";
        public string CurrentCryptConnectionString
        {
            get => currentCryptConnectionString;
            set
            {
                if (Equals(currentCryptConnectionString, value)) return;
                currentCryptConnectionString = value;
                OnPropertyChanged();
            }
        }




        // SqlConnectionStringBuilder ---------------------------------------------------------------------------------------
        SqlConnectionStringBuilder ConnectionStringBuilder = new SqlConnectionStringBuilder();


        // Режим создания подключения (Строка подключения?) -----------------------------------------------------------------
        private bool? connectionStringMode = false;
        public bool? ConnectionStringMode
        {
            get => connectionStringMode;
            set
            {
                if (Equals(connectionStringMode, value)) return;
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


        // Название для создаваемого подлючения -----------------------------------------------------------------------------
        private string createdConnectionStringName = "";
        public string CreatedConnectionStringName
        {
            get => createdConnectionStringName;
            set
            {
                if (Equals(createdConnectionStringName, value)) return;
                createdConnectionStringName = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        // Название сервера создаваемого подлючения -------------------------------------------------------------------------
        private string createdConnectionStringServer = "";
        public string CreatedConnectionStringServer
        {
            get => createdConnectionStringServer;
            set
            {
                if (Equals(createdConnectionStringServer, value)) return;
                createdConnectionStringServer = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        // Название базы данных создаваемого подлючения ---------------------------------------------------------------------
        private string createdConnectionStringDataBase = "";
        public string CreatedConnectionStringDataBase
        {
            get => createdConnectionStringDataBase;
            set
            {
                if (Equals(createdConnectionStringDataBase, value)) return;
                createdConnectionStringDataBase = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        // Имя пользователя создаваемого подлючения -------------------------------------------------------------------------
        private string createdConnectionStringUserID = "";
        public string CreatedConnectionStringUserID
        {
            get => createdConnectionStringUserID;
            set
            {
                if (Equals(createdConnectionStringUserID, value)) return;
                createdConnectionStringUserID = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        // Пароль создаваемого подлючения -----------------------------------------------------------------------------------
        private string createdConnectionStringPassword = "";
        public string CreatedConnectionStringPassword
        {
            get => createdConnectionStringPassword;
            set
            {
                if (Equals(createdConnectionStringPassword, value)) return;
                createdConnectionStringPassword = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        // Строка подключения создаваемого подлючения -----------------------------------------------------------------------
        private string createdConnectionString = "";
        public string CreatedConnectionString
        {
            get => createdConnectionString;
            set
            {
                if (Equals(createdConnectionString, value)) return;
                createdConnectionString = value;
                CheckCreatedValues();
                OnPropertyChanged();
            }
        }


        // Удачность/Неудачность подключения --------------------------------------------------------------------------------
        private bool checkcreatedConnectionResult = false;
        public bool CheckcreatedConnectionResult
        {
            get => checkcreatedConnectionResult;
            set
            {
                if (Equals(checkcreatedConnectionResult, value)) return;

                checkcreatedConnectionResult = value;
                if (value == true)
                {
                    ShowMessage("Соединение установлено!", "Green-002", false);
                    SaveCreatedConnectionButtonEnabled = true;
                }
                else
                {
                    SaveCreatedConnectionButtonEnabled = false;
                    ShowMessage("", "Red-001", false);
                }

                LoaderUC = null;
                OnPropertyChanged();
            }
        }








        // Цвет текста "Название текущего подлючения" -----------------------------------------------------------------------
        private SolidColorBrush currentConnectionTextColor = GetColor.Get("Red-001");
        public SolidColorBrush CurrentConnectionTextColor
        {
            get => currentConnectionTextColor;
            set
            {
                if (Equals(currentConnectionTextColor, value)) return;
                currentConnectionTextColor = value;
                OnPropertyChanged();
            }
        }


        // Название текущего подключения ------------------------------------------------------------------------------------
        private string currentConnectionName = "Не создано!";
        public string CurrentConnectionName
        {
            get => currentConnectionName;
            set
            {
                if (Equals(currentConnectionName, value)) return;
                currentConnectionName = value;
                if (value == "Не создано!")
                {
                    CurrentConnectionTextColor = GetColor.Get("Red-001");
                }
                OnPropertyChanged();
            }
        }








        // Текст сообщения для вывода ---------------------------------------------------------------------------------------
        private string messageText = "";
        public string MessageText
        {
            get => messageText;
            set
            {
                if (Equals(messageText, value)) return;

                messageText = value;
                OnPropertyChanged();
            }
        }


        // Цвет текста сообщения --------------------------------------------------------------------------------------------
        private SolidColorBrush messageTextColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush MessageTextColor
        {
            get => messageTextColor;
            set
            {
                if (Equals(messageTextColor, value)) return;
                messageTextColor = value;
                OnPropertyChanged();
            }
        }







        // Enabled кнопки "Проверить соединение" создаваемого подключения ---------------------------------------------------
        private bool checkCreatedConnectionButtonEnabled = false;
        public bool CheckCreatedConnectionButtonEnabled
        {
            get => checkCreatedConnectionButtonEnabled;
            set
            {
                if (Equals(checkCreatedConnectionButtonEnabled, value)) return;
                checkCreatedConnectionButtonEnabled = value;
                OnPropertyChanged();
            }
        }


        // Enabled кнопки "Сохранить" создаваемого подключения --------------------------------------------------------------
        private bool saveCreatedConnectionButtonEnabled = false;
        public bool SaveCreatedConnectionButtonEnabled
        {
            get => saveCreatedConnectionButtonEnabled;
            set
            {
                if (Equals(saveCreatedConnectionButtonEnabled, value)) return;
                saveCreatedConnectionButtonEnabled = value;
                OnPropertyChanged();
            }
        }


        // UserControl для анимации загрузки --------------------------------------------------------------------------------
        private UserControl loader = null;
        public UserControl LoaderUC
        {
            get => loader;
            set
            {
                if (Equals(loader, value)) return;
                loader = value;
                OnPropertyChanged();
            }
        }


        // Visibility окна авторизации --------------------------------------------------------------------------------------
        private Visibility authorizationVisibility;
        public Visibility AuthorizationVisibility
        {
            get => authorizationVisibility;
            set
            {
                if (Equals(authorizationVisibility, value)) return;
                authorizationVisibility = value;
                OnPropertyChanged();
            }
        }


        // Visibility окна списка подключений -------------------------------------------------------------------------------
        private Visibility connectionListVisibility;
        public Visibility ConnectionListVisibility
        {
            get => connectionListVisibility;
            set
            {
                if (Equals(connectionListVisibility, value)) return;
                connectionListVisibility = value;
                if (value == Visibility.Visible)
                {
                    LoadAllConnections();
                }
                OnPropertyChanged();
            }
        }


        // Visibility окна создани подключения ------------------------------------------------------------------------------
        private Visibility createConnectionVisibility;
        public Visibility CreateConnectionVisibility
        {
            get => createConnectionVisibility;
            set
            {
                if (Equals(createConnectionVisibility, value)) return;
                createConnectionVisibility = value;
                OnPropertyChanged();
            }
        }


        // Visibility элементов для создания с режимом "ConnectionString" ---------------------------------------------------
        private Visibility connectionStringTrueVisibility;
        public Visibility ConnectionStringTrueVisibility
        {
            get => connectionStringTrueVisibility;
            set
            {
                if (Equals(connectionStringTrueVisibility, value)) return;

                connectionStringTrueVisibility = value;
                OnPropertyChanged();
            }
        }


        // Visibility элементов для создания не с режимом "ConnectionString" ------------------------------------------------
        private Visibility connectionStringFalseVisibility;
        public Visibility ConnectionStringFalseVisibility
        {
            get => connectionStringFalseVisibility;
            set
            {
                if (Equals(connectionStringFalseVisibility, value)) return;

                connectionStringFalseVisibility = value;
                OnPropertyChanged();
            }
        }


        // Коллекция RadioButton-ов подключений -----------------------------------------------------------------------------
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

        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::




        #region Команды =====================================================================================================

        // Команда для перехода в окно списка подключений -------------------------------------------------------------------
        public ICommand ClickShowConnectionList
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ShowMessage(false);
                    ShowConnectionList();
                });
            }
        }

        // Команда для перехода в окно авторизации --------------------------------------------------------------------------
        public ICommand ClickShowAuthorization
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ShowAuthorization();
                    ShowMessage(false);
                });
            }
        }

        // Команда для перехода в окно создания подключения -----------------------------------------------------------------
        public ICommand ClickShowCreateConnection
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ShowCreateConnection();
                    ShowMessage(false);
                });
            }
        }

        // Команда для проверки соединения с создаваемым подключением -------------------------------------------------------
        public ICommand ClickCheckCreatedConnection
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CheckCreatedConnection();
                    ShowMessage(false);

                });
            }
        }

        // Команда для вставки строки подключения из буфера -----------------------------------------------------------------
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

        // Команда для сохранения созданного подключения --------------------------------------------------------------------
        public ICommand SaveConnectionCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    SaveConnection();
                });
            }
        }

        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::




        #region События =====================================================================================================

        // Событие: При выборе покдлючения ----------------------------------------------------------------------------------
        async void SelectConnection(object sender, RoutedEventArgs e)
        {
            ShowMessage("Выбор подключения...", "Blue-003", true);

            CurrentConnectionName = (sender as ConnectionRB).Content.ToString();

            await Task.Run(() => XmlClass.SetSelectConnection(CurrentConnectionName));

            LoadCurrentConnectionString();

            ShowMessage("Выбрано подключение: " + '"' + CurrentConnectionName + '"', "Blue-003", false);
        }

        // Событие: При удалении покдлючения --------------------------------------------------------------------------------
        private void ConnectionRB_Deleted(object sender, EventArgs e)
        {
            string DelConnName = (sender as ConnectionRB).Content.ToString();
            ShowMessage("Удаление подключения: " + '"' + DelConnName + '"', "Blue-003", true);


            foreach (ConnectionRB CRB in Connections)
            {
                if (CRB.Content.ToString() == DelConnName)
                {
                    bool isCheck = (bool)CRB.IsChecked;
                    DeleteConnectionFromXmlFile(CRB.Content.ToString());
                    Connections.Remove(CRB);

                    ShowMessage("Подключение: " + '"' + DelConnName + '"' + " удалено!", "Blue-003", false);

                    if (isCheck && Connections.Count >= 1)// если удален выбранный и есть еще
                    {
                        Connections[Connections.Count - 1].IsChecked = true;
                    }
                    if (Connections.Count == 0)
                    {
                        CurrentConnectionName = "Не создано!";

                        ShowMessage("Нет созданных подключений.\nСоздайте новое подключение.", "Red-001", false);

                        CurrentConnectionTextColor = GetColor.Get("Red-001");
                    }
                    break;
                }
            }
        }

        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::




        #region Методы ======================================================================================================

        // Метод: Первый вызываемый метод при загрузке ----------------------------------------------------------------------
        private void StartMethod()
        {

            LoadCurrentConnectionName();
            if (CurrentConnectionName != "Не создано!")
            {
                LoadCurrentUserName();
                LoadCurrentConnectionString();
            }
        }


        // Метод: Загружает зашифрованную строку подключения из XML-файла ---------------------------------------------------
        async void LoadCurrentConnectionString()
        {
            CurrentCryptConnectionString = await Task.Run(XmlClass.GetSelectedConnectionString);
        }

        // Метод: Загружает название последнего выбранного подключения ------------------------------------------------------
        async void LoadCurrentConnectionName()
        {
            ShowMessage("Загрузка текущего подключения...", "Blue-003", true);

            CurrentConnectionName = await Task.Run(XmlClass.GetSelectedConnectionName);
            

            if (CurrentConnectionName == "")
            {
                CurrentConnectionName = "Не создано!";
                ShowMessage("Нет созданных подключений.\nСоздайте новое подключение.", "Red-001", false);
            }
            else
            {
                CurrentConnectionTextColor = GetColor.Get("Dark-003");
                ShowMessage("Выбрано подключение: " + '"' + CurrentConnectionName + '"', "Blue-003", false);
            }
        }

        // Метод: Загружает список подклчений -------------------------------------------------------------------------------
        async void LoadAllConnections()
        {
            ShowMessage("Загрузка списка подключений...", "Blue-003", true);

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

            ShowMessage(false);
        }
        
        // Метод: Удаляет удаленное подключение из XML-файла ----------------------------------------------------------------
        void DeleteConnectionFromXmlFile(string DeletedConnectionName)
        {
            XmlClass.DeleteConnection(DeletedConnectionName);
        }

        // Метод: Показывает окно авторизации (остальные окна скрывает) -----------------------------------------------------
        void ShowAuthorization()
        {
            AuthorizationVisibility = Visibility.Visible;
            ConnectionListVisibility = Visibility.Collapsed;
            CreateConnectionVisibility = Visibility.Collapsed;
        }

        // Метод: Показывает окно списка подключений (остальные окна скрывает) ----------------------------------------------
        void ShowConnectionList()
        {
            AuthorizationVisibility = Visibility.Collapsed;
            ConnectionListVisibility = Visibility.Visible;
            CreateConnectionVisibility = Visibility.Collapsed;
        }
        
        // Метод: Показывает окно создания подключения (остальные окна скрывает) --------------------------------------------
        void ShowCreateConnection()
        {
            AuthorizationVisibility = Visibility.Collapsed;
            ConnectionListVisibility = Visibility.Collapsed;
            CreateConnectionVisibility = Visibility.Visible;
            ConnectionStringMode = false;
        }

        // Метод: Очищает все значения создаваемого подключения -------------------------------------------------------------
        void ClearCreatedValues()
        {
            CreatedConnectionStringName = "";
            CreatedConnectionStringServer = "";
            CreatedConnectionStringDataBase = "";
            CreatedConnectionStringUserID = "";
            CreatedConnectionStringPassword = "";
            CreatedConnectionString = "";

            CheckCreatedValues();
        }

        // Метод: Проверяет заполненость значений для проверки соединения ---------------------------------------------------
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

        // Метод: Проверяет соединение создаваемым подключением -------------------------------------------------------------
        async void CheckCreatedConnection()
        {
            ShowMessage("Установка соединения...", "Blue-003", true);

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
                ShowMessage(ex.Message, "Red-001", false);
            }
        }

        // Метод: Пытается открыть и закрыть подключение с создаваемым подключением -----------------------------------------
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
                    ShowMessage(ex.Message, "Red-001", false);
                    ok = false;
                }
            }
            return ok;
        }

        // Метод: Загружает имя последнего авторизованного пользователя -----------------------------------------------------
        async void LoadCurrentUserName()
        {
            CurrentUserName = await Task.Run(XmlClass.GetCurrentUserName);
        }

        // Метод: Вставляет в поле строку подключения из буфера обмена ------------------------------------------------------
        void PasteConnectionString()
        {
            CreatedConnectionString = Clipboard.GetText();
        }


        async void SaveConnection()
        {
            ShowMessage("Сохранение подключения...", "Blue-003", true);

            CurrentConnectionName = CreatedConnectionStringName;
            await Task.Run(() => XmlClass.AddConnectSetting(CurrentConnectionName, ConnectionStringBuilder.ConnectionString));

            SaveCreatedConnectionButtonEnabled = false;
            ClearCreatedValues();

            ShowMessage("Подключение " + '"' + CurrentConnectionName + '"' + " сохранено!", "Green-002", false);

        }


        void ShowMessage(string Text, string TextColor, bool ShowLoader)
        {
            MessageText = Text;
            MessageTextColor = GetColor.Get(TextColor);
            if(ShowLoader)
            {
                LoaderUC = new Loader();
            }
            else
            {
                LoaderUC = null;
            }
        }

        void ShowMessage(string Text, bool ShowLoader)
        {
            MessageText = Text;
            MessageTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C566E"));

            if (ShowLoader)
            {
                LoaderUC = new Loader();
            }
            else
            {
                LoaderUC = null;
            }
        }

        void ShowMessage(bool ShowLoader)
        {
            MessageText = "";
            MessageTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C566E"));

            if (ShowLoader)
            {
                LoaderUC = new Loader();
            }
            else
            {
                LoaderUC = null;
            }
        }

        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::    
    }
}
