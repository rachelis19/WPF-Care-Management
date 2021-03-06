﻿using CareManagment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CareManagment.Commands
{
    class SaveChangesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DisplayDistributionsVM CurrentVM { get; set; }

        public SaveChangesCommand(DisplayDistributionsVM VM)
        {
            CurrentVM = VM;
        }

        public bool CanExecute(object parameter)
        {
            return CurrentVM.DistributionsToUpdate.Count != 0;
        }

        public void Execute(object parameter)
        {
            CurrentVM.SaveChanges();
        }
    }
}
