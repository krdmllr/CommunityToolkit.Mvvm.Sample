namespace MauiCommunityToolkitMvvmSample.Notes;

public partial class NoteListPage : ContentPage
{
	public NoteListPage(NoteListViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}