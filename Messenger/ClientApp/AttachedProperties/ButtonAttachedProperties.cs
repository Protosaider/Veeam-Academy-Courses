using System;

namespace ClientApp.AttachedProperties
{
    /// <summary>
    /// The IsBusy attached property for a anything that wants to flag if the control is busy
    /// </summary>
    public sealed class IsBusyProperty : BaseAttachedProperty<IsBusyProperty, Boolean>
    {
    }
}
