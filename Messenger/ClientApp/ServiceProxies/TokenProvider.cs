using ClientApp.Other;
using System;

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
