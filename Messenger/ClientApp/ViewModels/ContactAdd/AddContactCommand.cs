using ClientApp.DataSuppliers;
using ClientApp.ServiceProxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels.ContactAdd
{
    public sealed class AddContactCommand : CBaseAsyncCommand
    {
        private readonly IContactsSupplier _contactsSupplier;

        public AddContactCommand()
        {
            _contactsSupplier = new CContactsSupplier();
        }

        protected override Boolean CanExecute<T>(T parameter)
        {
            return true;
        }

        protected sealed override async Task ExecuteAsync(Object parameter)
        {
            if (parameter == null)
                return;

            var contactId = (Guid)parameter;

            var result = await _contactsSupplier.AddContact(contactId, STokenProvider.Id);

            if (result == null)
                return;
        }
    }
}
