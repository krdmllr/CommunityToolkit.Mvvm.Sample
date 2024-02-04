using CommunityToolkit.Mvvm.Messaging;

namespace MauiCommunityToolkitMvvmSample.Notes;

public record NoteAddedMessage(Note AddedNote);
public record NoteUpdatedMessage(Note UpdatedNote);

public class NoteService
{
    private IMessenger messenger;

    public List<Note> Notes { get; private set; }

    public NoteService(IMessenger messenger)
    {
        this.messenger = messenger;
        Notes = GenerateSampleNotes();
    }

    public void AddOrUpdateNote(string title, string text, Guid? id = null)
    {
        var existingNote = Notes.FirstOrDefault(note => note.Id == id);

        if (existingNote != null)
        {
            existingNote = existingNote with { Text = text, Title = title };
            Console.WriteLine($"Note '{existingNote.Title}' updated successfully!");
            messenger.Send(new NoteUpdatedMessage(existingNote));
        }
        else
        {
            var note = new Note(Guid.NewGuid(), title, text);
            Console.WriteLine($"Note '{note.Title}' added successfully!");
            messenger.Send(new NoteAddedMessage(note));
            Notes.Add(note);
        }
    }

    private List<Note> GenerateSampleNotes() => [
            new(Guid.NewGuid(), "Preheat Oven", "Preheat your oven to 450°F."),
            new(Guid.NewGuid(), "Prepare Dough", "Make or buy pizza dough."),
            new(Guid.NewGuid(), "Roll Out Dough", "Roll out the dough on a floured surface."),
            new(Guid.NewGuid(), "Add Sauce", "Spread tomato sauce on the rolled-out dough."),
            new(Guid.NewGuid(), "Cheese", "Sprinkle grated cheese on top of the sauce."),
            new(Guid.NewGuid(), "Toppings", "Add your favorite toppings like pepperoni, mushrooms, and bell peppers."),
            new(Guid.NewGuid(), "Bake", "Place the pizza in the preheated oven and bake for 15-20 minutes."),
            new(Guid.NewGuid(), "Check Doneness", "Make sure the crust is golden brown and the cheese is melted."),
            new(Guid.NewGuid(), "Remove from Oven", "Carefully take the pizza out of the oven."),
            new(Guid.NewGuid(), "Let it Cool", "Allow the pizza to cool for a few minutes before slicing."),
            new(Guid.NewGuid(), "Enjoy", "Delicious homemade pizza is ready to be enjoyed!"),
            new(Guid.NewGuid(), "Clean Up", "Don't forget to clean up your kitchen mess."),
            new(Guid.NewGuid(), "Invite Friends", "Invite friends over to share the tasty pizza."),
            new(Guid.NewGuid(), "Leftovers", "Store any leftovers in the fridge for later."),
            new(Guid.NewGuid(), "Try New Recipes", "Experiment with different pizza recipes."),
            new(Guid.NewGuid(), "Pizza Party", "Host a pizza party for a fun gathering."),
            new(Guid.NewGuid(), "Delivery Night", "Take a break and order pizza for a lazy night."),
            new(Guid.NewGuid(), "Crust Options", "Explore different crust options like thin crust or stuffed crust."),
            new(Guid.NewGuid(), "Homemade Sauce", "Consider making your own pizza sauce for a personalized touch.")
        ];
}
