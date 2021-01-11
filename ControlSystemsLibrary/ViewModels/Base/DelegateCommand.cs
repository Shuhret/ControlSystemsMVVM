using System;
using System.Windows.Input;


namespace ControlSystemsLibrary.ViewModels.Base
{
    class DelegateCommand : ICommand
    {
        // Действие, которое нужно выполнить при выполнении этой команды
        private Action<object> executionAction;


        // Предикат, чтобы определить, является ли команда действительной для выполнения
        private Predicate<object> canExecutePredicate;

        // Инициализирует новый экземпляр класса DelegateCommand.
        // Команда всегда будет действительна для исполнения.
        // <param name="execute">Делегат для вызова на исполнение</param>
        public DelegateCommand(Action<object> execute) : this(execute, null)
        {

        }

        // Инициализирует новый экземпляр класса DelegateCommand.
        // <param name="execute">Делегат для вызова на исполнение</param>
        // <param name="canExecute">Предикат, чтобы определить, является ли команда действительной для выполнения</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.executionAction = execute;
            this.canExecutePredicate = canExecute;
        }

        // Возникает при изменении CanExecute
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Выполняет делегата, поддерживающего эту DelegateCommand
        // <param name="parameter">параметр для передачи в предикат</param>
        // <returns>True, если команда действительна для выполнения</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecutePredicate == null ? true : this.canExecutePredicate(parameter);
        }

        // Выполняет делегата, поддерживающего эту DelegateCommand
        // <param name="parameter"> параметр для передачи делегату </param>
        // <exception cref="InvalidOperationException">Брошенный, если CanExecute возвращает false</exception>
        public void Execute(object parameter)
        {
            if (!this.CanExecute(parameter))
            {
                throw new InvalidOperationException("Команда недопустима для выполнения, проверьте метод CanExecute, прежде чем пытаться выполнить.");
            }
            this.executionAction(parameter);
        }
    }
}
