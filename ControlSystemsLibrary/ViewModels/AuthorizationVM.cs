using ControlSystemsLibrary.Controls;
using ControlSystemsLibrary.Models.Classes;
using ControlSystemsLibrary.Resources.Styles.WindowStyle;
using ControlSystemsLibrary.Services;
using ControlSystemsLibrary.ViewModels.Base;
using ControlSystemsLibrary.ViewModels.MainWindowViewModel;
using ControlSystemsLibrary.Views;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ControlSystemsLibrary.ViewModels
{
    class AuthorizationVM : ViewModelBase
    {
        LoaderCubes LC = new LoaderCubes();

        public AddSetUserInterface ASUI;

        public ObservableCollection<UserInterfacesClass> UserInterfacesCollection = new ObservableCollection<UserInterfacesClass>();

        public Authorization AuthorizationView;


        // КОНСТРУКТОР ------------------------------------------------------------------------------------------------------
        public AuthorizationVM()
        {
            StartMethod();
            ConnectionStringTrueVisibility = Visibility.Hidden;
            CreateConnectionVisibility = Visibility.Hidden;
            ConnectionListVisibility = Visibility.Hidden;
        }



    



















        // Команда для авторизации ------------------------------------------------------------------------------------------
        public ICommand ClickAuthorizationCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    UserAuthorization((obj as PasswordBox));
                });
            }
        }

        async void UserAuthorization(PasswordBox passwordBox)
        {
            AuthorizationEnabled = false;
            ShowMessage("Проверка логина и пароля...", "Blue-003", true);
            string message = "";
            if (await Task.Run(() => DataBaseRequest.CheckAuthorization(CurrentUserName, passwordBox.Password, ref message) == true))
            {
                string UserInterfaceName = DataBaseRequest.GetUserInterfaceName(CurrentUserName);
                string UserInterfaceFullName = GenerateUserInterfaceFullName(UserInterfaceName);
                if (CheckUserInterfacesCollection(UserInterfaceFullName) == false)
                {
                    UserControl UC = GetNewUserInterface(UserInterfaceName);
                    UserInterfacesClass UIC = new UserInterfacesClass();
                    UIC.FullUserInterfaceName = UserInterfaceFullName;
                    UIC.UserInterfaceControl = UC;
                    UserInterfacesCollection.Add(UIC);
                    ASUI(UIC.UserInterfaceControl);
                    ShowMessage(false);
                }
                else
                {
                    ASUI(GetUserInterfaceFromCollection(UserInterfaceFullName).UserInterfaceControl);
                    ShowMessage(false);
                }
            }
            else
            {
                ShowMessage(message, "Red-001", false);
            }
            AuthorizationEnabled = true;
        }

        string GenerateUserInterfaceFullName(string UserInterfaceControlName)
        {
            return  Strings.RemoveCharacters(CurrentUserName) + Strings.RemoveCharacters(UserInterfaceControlName) + Strings.RemoveCharacters(CurrentConnectionName);
        }

        UserControl GetNewUserInterface(string UserInterfaceName)
        {
            switch(UserInterfaceName)
            {
                case "Администратор": return new Administrator(ASUI, AuthorizationView, CurrentUserName, CurrentConnectionName, CurrentCryptConnectionString);
                case "Логист": return new Logist(ASUI, AuthorizationView);
                default: return new UserInterfaceSelection();
            }
        }

        UserInterfacesClass GetUserInterfaceFromCollection(string UserInterfaceFullName)
        {
            foreach (UserInterfacesClass UIC in UserInterfacesCollection)
            {
                if (UIC.FullUserInterfaceName == UserInterfaceFullName)
                {
                    return UIC;
                }
            }
            return null;
        }

        bool CheckUserInterfacesCollection(string UserInterfaceFullName)
        {
            foreach(UserInterfacesClass UIC in UserInterfacesCollection)
            {
                if(UIC.FullUserInterfaceName == UserInterfaceFullName)
                {
                    return true;
                }
            }
            return false;
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
                checkcreatedConnectionResult = value;

                SaveCreatedConnectionButtonEnabled = value; // "Сохранить"

                OnPropertyChanged();
            }
        }








        // Цвет текста "Название текущего подлючения" -----------------------------------------------------------------------
        private SolidColorBrush currentConnectionTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C566E"));
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
        private string currentConnectionName;
        public string CurrentConnectionName
        {
            get => currentConnectionName;
            set
            {
                currentConnectionName = value;
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
        private SolidColorBrush messageTextColor = new SolidColorBrush(Colors.Red);
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




        // Enabled элементов авторизации ------------------------------------------------------------------------------------
        private bool authorizationEnabled = false;
        public bool AuthorizationEnabled
        {
            get => authorizationEnabled;
            set
            {
                if (Equals(authorizationEnabled, value)) return;
                authorizationEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool connectionListEnabled = true;
        public bool ConnectionListEnabled
        {
            get => connectionListEnabled;
            set
            {
                if (Equals(connectionListEnabled, value)) return;
                connectionListEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool createconnectionEnabled = true;
        public bool CreateconnectionEnabled
        {
            get => createconnectionEnabled;
            set
            {
                if (Equals(createconnectionEnabled, value)) return;
                createconnectionEnabled = value;
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
                //if (Equals(connectionListVisibility, value)) return;
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
        public ICommand ClickShowConnectionListCommand
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
        public ICommand ClickShowAuthorizationCommand
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
        public ICommand ClickShowCreateConnectionCommand
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
        public ICommand ClickCheckCreatedConnectionCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CheckCreatedConnection();
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

        // Команда для проверки соединения с выбранным подключением ---------------------------------------------------------
        public ICommand ClickCheckSelectedConnectionCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CheckSelectedConnection();
                });
            }
        }




        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        async void CheckSelectedConnection()
        {
            ConnectionListEnabled = false;
            ShowMessage("Установка соединения...", "Blue-003", true);

            if (await Task.Run(() => OpenCloseConnection(Cryption.Decrypt(CurrentCryptConnectionString))) == true)
            {
                ShowMessage("Соединение установлено!", "Green-003", false);
                ConnectionListEnabled = true;
            }
            else
            {
                ShowMessage("Не удалось устновить соединение.", "Red-001", false);
                ConnectionListEnabled = true;
            }
        }

        #region События =====================================================================================================

        // Событие: При выборе покдлючения ----------------------------------------------------------------------------------
        async void SelectConnection(object sender, RoutedEventArgs e)
        {
            ShowMessage("Выбор подключения...", "Blue-003", true);

            CurrentConnectionName = (sender as ConnectionRB).Content.ToString();
            CurrentConnectionTextColor = GetColor.Get("Dark-003");


            await Task.Run(() => XmlClass.SetSelectConnection(CurrentConnectionName));

            CurrentCryptConnectionString = await Task.Run(XmlClass.GetSelectedConnectionString);


            ShowMessage("Выбрано подключение: " + '"' + CurrentConnectionName + '"', "Blue-003", false);
        }

        // Событие: При удалении покдлючения --------------------------------------------------------------------------------
        private void ConnectionRB_Deleted(object sender, EventArgs e)
        {
            string DelConnName = (sender as ConnectionRB).Content.ToString();

            ShowMessage("Удаление подключения: " + '"' + DelConnName + '"', "Blue-003", true);

            if (XmlClass.DeleteConnection(DelConnName) == true)
            {
                foreach (ConnectionRB CRB in Connections)
                {
                    if (CRB.Content.ToString() == DelConnName)
                    {
                        bool isCheck = (bool)CRB.IsChecked;
                        Connections.Remove(CRB);

                        ShowMessage("Подключение: " + '"' + DelConnName + '"' + " удалено!", "Blue-003", false);

                        if (isCheck && Connections.Count >= 1)// если удален выбранный и есть еще
                        {
                            Connections[Connections.Count - 1].IsChecked = true;
                        }
                        if (Connections.Count == 0)
                        {
                            CurrentConnectionName = null;
                            ShowMessage("Нет созданных подключений.\nСоздайте новое подключение.", "Red-001", false);
                            CurrentConnectionTextColor = GetColor.Get("Red-001");
                        }
                        break;
                    }
                }
            }
        }

        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::




        #region Методы ======================================================================================================

        // Метод: Первый вызываемый метод при загрузке ----------------------------------------------------------------------
        async void StartMethod()
        {
            AuthorizationEnabled = false;
            ShowMessage("Получение имени пользователя...", "Blue-003", true);
            CurrentUserName = await Task.Run(XmlClass.GetCurrentUserName);

            ShowMessage("Получение названия подключения...", "Blue-003", true);
            CurrentConnectionName = await Task.Run(XmlClass.GetSelectedConnectionName);

            ShowMessage("Загрузка зашифрованной строки подключения...", "Blue-003", true);
            CurrentCryptConnectionString = await Task.Run(XmlClass.GetSelectedConnectionString);

            if (CurrentCryptConnectionString != null)
            {
                ShowMessage("Установка соединения...", "Blue-003", true);
                if (await Task.Run(() => OpenCloseConnection(Cryption.Decrypt(CurrentCryptConnectionString))))
                {
                    ShowMessage("Соединение установлено!", "Green-003", false);
                    AuthorizationEnabled = true;
                }
                else
                {
                    ShowMessage("Не удалось установить соединение.", "Red-001", false);
                    AuthorizationEnabled = true;
                }
            }
            else
            {
                AuthorizationEnabled = true;
                CurrentConnectionTextColor = GetColor.Get("Red-001");
                ShowMessage("Создайте подключение.", "Red-001", false);
            }
        }

        // Метод: Загружает список подклчений -------------------------------------------------------------------------------
        async void LoadAllConnections()
        {
            ShowMessage("Загрузка списка подключений...", "Blue-003", true);

            Connections.Clear();
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
            }

            ShowMessage(false);
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
            CheckcreatedConnectionResult = false;

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
            CheckcreatedConnectionResult = false;
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
            ShowMessage("Проверка названия подключения", "Blue-003", true);
            if (await Task.Run(() => XmlClass.CheckConnectionName(createdConnectionStringName)) == false)
            {
                CreateconnectionEnabled = false;
                CheckCreatedConnectionButtonEnabled = false;

                ShowMessage("Установка соединения...", "Blue-003", true);

                CheckcreatedConnectionResult = false;
                if (ConnectionStringMode == true)
                {
                    try
                    {
                        ConnectionStringBuilder.ConnectionString = CreatedConnectionString;
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message, "Red-001", false);
                        CreateconnectionEnabled = true;
                        return;
                    }
                }
                else
                {
                    ConnectionStringBuilder.DataSource = CreatedConnectionStringServer;
                    ConnectionStringBuilder.InitialCatalog = CreatedConnectionStringDataBase;
                    ConnectionStringBuilder.UserID = CreatedConnectionStringUserID;
                    ConnectionStringBuilder.Password = CreatedConnectionStringPassword;
                }


                if (await Task.Run(() => OpenCloseConnection(ConnectionStringBuilder.ConnectionString)))
                {
                    ShowMessage("Соединение установлено!", "Green-003", false);
                    CheckcreatedConnectionResult = true;
                    CreateconnectionEnabled = true;
                }
                else
                {
                    ShowMessage("Не удалось установить соединение.", "Red-001", false);
                    CreateconnectionEnabled = true;
                }
            }
            else
            {
                ShowMessage("Подключение с названием " + '"' + createdConnectionStringName + '"' + " уже создано. Измените название.", "Red-001", false);
            }
        }

        // Метод: Пытается открыть и закрыть подключение с создаваемым подключением -----------------------------------------
        bool OpenCloseConnection(string ConnString)
        {
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                try
                {
                    conn.Open();
                    conn.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        // Метод: Вставляет в поле строку подключения из буфера обмена ------------------------------------------------------
        void PasteConnectionString()
        {
            CreatedConnectionString = Clipboard.GetText();
        }

        async void SaveConnection()
        {
            CreateconnectionEnabled = false;
            ShowMessage("Сохранение подключения...", "Blue-003", true);

            CurrentConnectionName = CreatedConnectionStringName;
            if (await Task.Run(() => XmlClass.CreateConnectionString(CurrentConnectionName, ConnectionStringBuilder.ConnectionString)) == true)
            {
                ClearCreatedValues();
                ShowMessage("Подключение " + '"' + CurrentConnectionName + '"' + " сохранено!", "Green-003", false);
                CurrentConnectionTextColor = GetColor.Get("Dark-003");
            }
            else
            {
                ShowMessage("Что-то пошло не так", "Green-003", false);
            }

            SaveCreatedConnectionButtonEnabled = false;
            CreateconnectionEnabled = true;
        }




        // Метод: Показывает текст сообщения и анимация загрузки (1-перегрузка) ---------------------------------------------
        void ShowMessage(string Text, string TextColor, bool ShowLoader)
        {
            MessageText = Text;
            MessageTextColor = GetColor.Get(TextColor);
            if (ShowLoader)
            {
                LoaderUC = LC;
            }
            else
            {
                LoaderUC = null;
            }
        }

        // Метод: Показывает текст сообщения и анимация загрузки (2-перегрузка) ---------------------------------------------
        void ShowMessage(bool ShowLoader)
        {
            MessageText = "";
            MessageTextColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C566E"));

            if (ShowLoader)
            {
                LoaderUC = LC;
            }
            else
            {
                LoaderUC = null;
            }
        }

        #endregion ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::   



        public ICommand TestCommand
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    MessageBox.Show(obj.ToString());
                });
            }
        }



    }
}
