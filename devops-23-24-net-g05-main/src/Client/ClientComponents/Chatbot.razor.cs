using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Client.ClientComponents;

public partial class Chatbot
{
    private bool IsChatbotOpen = false;
    private bool IsEmojiPickerOpen = false;
    private List<string> Messages = new List<string>();
    private string UserInput;
    private List<string> EmojiList = new List<string> { "😀", "😂", "👍", "😍", "😢", "🤔" };

    private void ToggleChatbot()
    {
        IsChatbotOpen = !IsChatbotOpen;
    }

    private void ToggleEmojiPicker()
    {
        IsEmojiPickerOpen = !IsEmojiPickerOpen;
    }

    private void InsertEmoji(string emoji)
    {
        UserInput += emoji;
        IsEmojiPickerOpen = false;
    }

    private bool IsTyping = false;

    private async Task SendMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            // User sends a message
            Messages.Add($"User: {message}");
            UserInput = string.Empty;
            IsTyping = true;

            // Simulate bot typing
            await Task.Delay(2000); // Wait for 2 seconds

            // Bot sends a message
            IsTyping = false;
            Messages.Add("Deze functie is nog niet beschikbaar!");
        }
    }
    private string GetFormattedDateTime()
    {
        var now = DateTime.Now;
        var datePart = now.Date == DateTime.Today ? "Today" : now.ToString("MMMM dd, yyyy");
        return $"{datePart} at {now:HH:mm}";
    }
}
