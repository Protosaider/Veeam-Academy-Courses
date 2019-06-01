﻿using ClientApp.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels.ContactAdd
{
    public sealed class CCloseAddContactCommand : CBaseCommand
    {
        protected override Boolean CanExecute<T>(Object parameter)
        {
            return true;
        }

        protected override void Execute<T>(Object parameter)
        {
            CViewModelLocator.Instance.ApplicationViewModel.GoToControl(SideMenuContent.Contacts);
        }
    }
}
