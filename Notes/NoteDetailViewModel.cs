using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics.CodeAnalysis;

namespace MauiCommunityToolkitMvvmSample.Notes;

public partial class NoteDetailViewModel(NoteService noteService) : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string? title;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string? text;

    private Note? note;

    [RelayCommand]
    public void GenerateText()
    {
        var random = new Random();
        var pizzaFacts = new List<string>
        {
            "Did you know that the world's largest pizza was 122 feet in diameter?",
            "The world record for the longest pizza delivery is 6,700 miles.",
            "The most expensive pizza in the world costs $12,000 and is topped with caviar and lobster.",
            "Pizza is consumed by 13% of the U.S. population every day.",
            "The first recorded pizzeria in the world was opened in Naples, Italy, in 1830."
        };

        Text = pizzaFacts[random.Next(0, pizzaFacts.Count)];
    }

    [MemberNotNullWhen(true, nameof(Title), nameof(Text))] 
    private bool CanSave => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Text);

    [RelayCommand(CanExecute = nameof(CanSave))]
    public async Task Save()
    {
        if (!CanSave)
        {
            return;
        }

        noteService.AddOrUpdateNote(Title, Text, note?.Id);
        await Shell.Current.GoToAsync($"//Notes");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Title = null;
        Text = null;
        note = null;

        if (query.TryGetValue("note", out var value) && value is Note selectedNote)
        {
            note = selectedNote;
            Title = note.Title;
            Text = note.Text;
        }
    }
}
