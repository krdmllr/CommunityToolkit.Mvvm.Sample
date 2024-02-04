namespace MauiCommunityToolkitMvvmSample.Notes;

public partial class NoteDetailPage : ContentPage
{
	public NoteDetailPage(NoteDetailViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}