﻿using System;
using System.Windows.Input;

namespace PoEMonitor.Helpers
{
    /// <summary>
    /// A class used for comandbinding in MVVM
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region private fields
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;
        #endregion

        public event EventHandler CanExecuteChanged
        {
            // wire the CanExecutedChanged event only if the canExecute func
            // is defined (that improves perf when canExecute is not used)
            add
            {
                if (this._canExecute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (this._canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }



        /// <summary>
        /// Initializes a new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }



        /// <summary>
        /// Initializes a new instance of the RelayCommand class
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this._execute = execute;
            this._canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute();

        }

    }
}
