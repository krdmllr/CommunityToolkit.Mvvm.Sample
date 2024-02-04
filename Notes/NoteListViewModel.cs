using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace MauiCommunityToolkitMvvmSample.Notes;

public partial class NoteListViewModel : ObservableObject, IRecipient<NoteAddedMessage>, IRecipient<NoteUpdatedMessage>
{
    private readonly IMessenger messenger;
    private readonly NoteService noteService;

    public NoteListViewModel(IMessenger messenger, NoteService noteService)
    {
        this.messenger = messenger;
        this.noteService = noteService;

        LoadNotes();
        SetupMessenger();
    }

    public ObservableCollection<Note> Notes { get; private set; } = new ObservableCollection<Note>();

    [RelayCommand]
    public async Task SelectDetail(Note note)
    {
        await Shell.Current.GoToAsync($"//Notes/Detail", new Dictionary<string, object>
        {
            { "note", note }
        });
    }

    [RelayCommand]
    public async Task CreateNote()
    {
        await Shell.Current.GoToAsync($"//Notes/Detail");
    }

    private void LoadNotes()
    {
        foreach (var note in noteService.Notes)
        {
            Notes.Add(note);
        }
    }

    private void SetupMessenger()
    {
        messenger.RegisterAll(this);
    }

    public void Receive(NoteAddedMessage message)
    {
        Notes.Add(message.AddedNote);
    }

    public void Receive(NoteUpdatedMessage message)
    {
        var existingNote = Notes.FirstOrDefault(note => note.Id == message.UpdatedNote.Id);

        if (existingNote != null)
        {
            // Update the existing note in the collection
            int index = Notes.IndexOf(existingNote);
            Notes[index] = message.UpdatedNote;
        }
    }
}