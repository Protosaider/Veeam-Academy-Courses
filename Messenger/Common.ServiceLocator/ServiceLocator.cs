namespace Common.ServiceLocator
{
    public static class SServiceLocator
    {
        public static CContainer CreateContainer() => CContainer.Create();
    }
}