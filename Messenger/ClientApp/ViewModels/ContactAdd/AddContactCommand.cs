using ClientApp.DataSuppliers;
using ClientApp.ServiceProxies;
using System;
using System.Threading.Tasks;
using ClientApp.ViewModels.Base;

namespace ClientApp.ViewModels.ContactAdd
{
	internal sealed class CAddContactCommand : CBaseAsyncCommand
    {
        private readonly IContactsSupplier _contactsSupplier;

        public CAddContactCommand()
        {
            _contactsSupplier = CContactsSupplier.Create();
        }

        protected override Boolean CanExecute<T>(T parameter) => true;

		protected override async Task ExecuteAsync(Object parameter)
        {
            if (parameter == null)
                return;

            var contactId = (Guid)parameter;

            await _contactsSupplier.AddContact(contactId, STokenProvider.Id);

            //var result = await _contactsSupplier.AddContact(contactId, STokenProvider.Id);

            //if (result == null)
            //    return;
        }
    }
}
