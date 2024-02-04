using MauiCommunityToolkitMvvmSample.Notes;

namespace MauiCommunityToolkitMvvmSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("Notes", typeof(NoteListPage));
            Routing.RegisterRoute("Notes/Detail", typeof(NoteDetailPage));

            MainPage = new AppShell();
        }
    }
}
