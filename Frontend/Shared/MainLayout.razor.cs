namespace Frontend.Shared
{
    public partial class MainLayout
    {
        //register
        public void register()
        {
            NavigationManager.NavigateTo("/signup");
        }

        //login
        public void login()
        {
            NavigationManager.NavigateTo("/login");
        }

        //logout
        public void logout()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
