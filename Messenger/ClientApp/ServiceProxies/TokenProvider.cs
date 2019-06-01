using ClientApp.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ServiceProxies
{
	internal static class STokenProvider
    {
        public static Guid Id { get; private set; }

        public static void OnAuthCompleted(Guid id)
        {
            Id = id;
            CViewModelLocator.Instance.ApplicationViewModel.RunUpdateLastActiveDateActivity();
        }
    }
}
